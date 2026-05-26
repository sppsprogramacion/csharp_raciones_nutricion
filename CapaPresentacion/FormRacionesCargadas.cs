using CapaDatos;
using CapaNegocio;
using CapaPresentacion.FuncionesGenerales;
using CapaPresentacion.Reportes;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormRacionesCargadas : Form
    {
        List<DPlanillaLiquidacion> listaPlanillaLiquidacionGlobal = new List<DPlanillaLiquidacion>();

        public FormRacionesCargadas()
        {
            InitializeComponent();
        }

        private void FormRacionesCargadas_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            this.CargarDataElaboradas();
        }

        //METODO PARA OBTENER ELABORADAS CARGADAS
        private void CargarDataElaboradas()
        {
            decimal total = 0;

            var nRacionElaborada = new NRacionElaborada();

            var (listaRacionElaboradas, error) = nRacionElaborada.ListaXFecha(dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listaRacionElaboradas.Count() <= 0)
            {
                MessageBox.Show("No se encontraron cargas en este rango de fechas", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            listaRacionElaboradas = listaRacionElaboradas
                .OrderBy(s => s.fecha_elaborada)
                .ToList();            

            //Listar TipoMenu
            NTipoMenu nTipoMenu = new NTipoMenu();
            (List<DTipoMenu> listaTipoMenu, string errorResponseTipoMenu) = nTipoMenu.ListarTodos();

            if (errorResponseTipoMenu != null)
            {
                MessageBox.Show(errorResponseTipoMenu, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaTipoMenu = listaTipoMenu
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar TipoMenu           

            //contar valores de menus en cada sap
            List<DPlanillaLiquidacion> listaPlanillaLiquidacion = new List<DPlanillaLiquidacion>();

            List<DFactores> listaFactores = new List<DFactores>();

            foreach (DRacionElaborada racionElaborada in listaRacionElaboradas)
            {
                // 🔴 NUEVA instancia en cada vuelta
                var planillaLiquidacion = new DPlanillaLiquidacion();
                planillaLiquidacion.fecha = racionElaborada.fecha_elaborada.ToString("dd-MM");


                List<DRacionElaboradaDetalles> listaDetalles = racionElaborada.raciones_elaboradas_detalles.ToList();

                List<DRacionElaboradaDetalles> listaFiltroDetallesXTipoMenu = new List<DRacionElaboradaDetalles>();
                foreach (DTipoMenu tipoMenu in listaTipoMenu)
                {
                    

                    listaFiltroDetallesXTipoMenu = listaDetalles.Where(x => x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();
                    int desayuno = 0;
                    int almuerzo = 0;
                    int merienda = 0;
                    int cena = 0;

                    foreach (DRacionElaboradaDetalles detalle in listaFiltroDetallesXTipoMenu)
                    {
                        desayuno = desayuno + detalle.almuerzo;
                        almuerzo = almuerzo + detalle.almuerzo;
                        merienda = merienda + detalle.cena;
                        cena = cena + detalle.cena;

                        
                    }

                    if (tipoMenu.id_tipo_menu == 1)
                    {
                        planillaLiquidacion.Pers_12hs_D = desayuno;
                        planillaLiquidacion.Pers_12hs_A = almuerzo;
                        planillaLiquidacion.Pers_12hs_M = merienda;
                        planillaLiquidacion.Pers_12hs_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "Pers_12hs_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "Pers_12hs_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "Pers_12hs_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "Pers_12hs_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);

                    }
                    if (tipoMenu.id_tipo_menu == 2)
                    {
                        planillaLiquidacion.Pers_24hs_D = desayuno;
                        planillaLiquidacion.Pers_24hs_A = almuerzo;
                        planillaLiquidacion.Pers_24hs_M = merienda;
                        planillaLiquidacion.Pers_24hs_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores5 = new DFactores();
                        dFactores5.tipo_menu = "Pers_24hs_D";
                        dFactores5.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores5);

                        // 🔴 NUEVA instancia
                        var dFactores6 = new DFactores();
                        dFactores6.tipo_menu = "Pers_24hs_A";
                        dFactores6.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores6);

                        // 🔴 NUEVA instanciaa
                        var dFactores7 = new DFactores();
                        dFactores7.tipo_menu = "Pers_24hs_M";
                        dFactores7.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores7);

                        // 🔴 NUEVA instancia
                        var dFactores8 = new DFactores();
                        dFactores8.tipo_menu = "Pers_24hs_C";
                        dFactores8.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores8);
                    }
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        planillaLiquidacion.Int_NORM_D = desayuno;
                        planillaLiquidacion.Int_NORM_A = almuerzo;
                        planillaLiquidacion.Int_NORM_M = merienda;
                        planillaLiquidacion.Int_NORM_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "Int_NORM_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "Int_NORM_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "Int_NORM_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "Int_NORM_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 4)
                    {
                        planillaLiquidacion.Int_ASTR_D = desayuno;
                        planillaLiquidacion.Int_ASTR_A = almuerzo;
                        planillaLiquidacion.Int_ASTR_M = merienda;
                        planillaLiquidacion.Int_ASTR_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "Int_ASTR_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "Int_ASTR_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "Int_ASTR_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "Int_ASTR_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 5)
                    {
                        planillaLiquidacion.Int_CELI_D = desayuno;
                        planillaLiquidacion.Int_CELI_A = almuerzo;
                        planillaLiquidacion.Int_CELI_M = merienda;
                        planillaLiquidacion.Int_CELI_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "Int_CELI_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "Int_CELI_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "Int_CELI_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "Int_CELI_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 6)
                    {
                        planillaLiquidacion.Int_A_Fib_D = desayuno;
                        planillaLiquidacion.Int_A_Fib_A = almuerzo;
                        planillaLiquidacion.Int_A_Fib_M = merienda;
                        planillaLiquidacion.Int_A_Fib_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "Int_A_Fib_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "Int_A_Fib_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "Int_A_Fib_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "Int_A_Fib_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 7)
                    {
                        planillaLiquidacion.Int_Hepat_D = desayuno;
                        planillaLiquidacion.Int_Hepat_A = almuerzo;
                        planillaLiquidacion.Int_Hepat_M = merienda;
                        planillaLiquidacion.Int_Hepat_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "Int_Hepat_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "Int_Hepat_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "Int_Hepat_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "Int_Hepat_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 8)
                    {
                        planillaLiquidacion.Int_SSAL_D = desayuno;
                        planillaLiquidacion.Int_SSAL_A = almuerzo;
                        planillaLiquidacion.Int_SSAL_M = merienda;
                        planillaLiquidacion.Int_SSAL_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "Int_SSAL_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "Int_SSAL_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "Int_SSAL_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "Int_SSAL_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 9)
                    {
                        planillaLiquidacion.Int_HIV_D = desayuno;
                        planillaLiquidacion.Int_HIV_A = almuerzo;
                        planillaLiquidacion.Int_HIV_M = merienda;
                        planillaLiquidacion.Int_HIV_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "Int_HIV_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "Int_HIV_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "Int_HIV_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "Int_HIV_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 10)
                    {
                        //planillaLiquidacion.Men_D = desayuno;
                        //planillaLiquidacion.Men_A = almuerzo;
                        //planillaLiquidacion.Men_M = merienda;
                        //planillaLiquidacion.Men_C = cena;

                        //los hijos menores son contados como racion de internos normal
                        planillaLiquidacion.Int_NORM_D = planillaLiquidacion.Int_NORM_D + desayuno;
                        planillaLiquidacion.Int_NORM_A = planillaLiquidacion.Int_NORM_A + almuerzo;
                        planillaLiquidacion.Int_NORM_M = planillaLiquidacion.Int_NORM_M + merienda;
                        planillaLiquidacion.Int_NORM_C = planillaLiquidacion.Int_NORM_C + cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "Men_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "Men_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "Men_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "Men_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }

                }

                listaPlanillaLiquidacion.Add(planillaLiquidacion);
            }
            //fin contar valores de menus en cada sap

            //cargar lista planilla global
            this.listaPlanillaLiquidacionGlobal = listaPlanillaLiquidacion;

            //Desvincular y limpiar
            dtgRacionesCargadas.DataSource = null;
            dtgRacionesCargadas.Columns.Clear();
            dtgRacionesCargadas.Rows.Clear();
            dtgRacionesCargadas.AutoGenerateColumns = false;

            // Columna fija: Concepto
            dtgRacionesCargadas.Columns.Add("Menus", "Menus");

            // Columnas dinámicas por FECHA
            foreach (var item in listaPlanillaLiquidacion)
            {
                dtgRacionesCargadas.Columns.Add(
                    item.fecha,
                    item.fecha
                );
            }

            // Columnas finales
            dtgRacionesCargadas.Columns.Add("Subtotal", "Subtotal");
            dtgRacionesCargadas.Columns.Add("Factor", "Factor");
            dtgRacionesCargadas.Columns.Add("Total", "Total");

            dtgRacionesCargadas.Columns["Subtotal"].ReadOnly = true;
            dtgRacionesCargadas.Columns["Factor"].ReadOnly = true;
            dtgRacionesCargadas.Columns["Total"].ReadOnly = true;

            // Propiedades del modelo (excepto fecha)
            var propiedades = typeof(DPlanillaLiquidacion)
                .GetProperties()
                .Where(p => p.Name != "fecha")
                .ToList();

            //cargar filas
            foreach (var prop in propiedades)
            {
                int fila = dtgRacionesCargadas.Rows.Add();
                string menu = prop.Name;

                dtgRacionesCargadas.Rows[fila].Cells["Menus"].Value = menu;

                int subtotal = 0;

                // Valores por fecha
                for (int i = 0; i < listaPlanillaLiquidacion.Count; i++)
                {
                    int valor = Convert.ToInt32(prop.GetValue(listaPlanillaLiquidacion[i]));
                    dtgRacionesCargadas.Rows[fila].Cells[i + 1].Value = valor;
                    subtotal += valor;
                }

                var factorAux = listaFactores.FirstOrDefault(x => x.tipo_menu == prop.Name);
                // Factor desde código / BD
                decimal factor = factorAux.factor;
                decimal totalRegistro = factorAux.factor;

                // Totales
                dtgRacionesCargadas.Rows[fila].Cells["Subtotal"].Value = subtotal;
                dtgRacionesCargadas.Rows[fila].Cells["Factor"].Value = factor;
                //dtgRacionesCargadas.Rows[fila].Cells["Total"].Value = subtotal * factor;
                totalRegistro = subtotal * factor;
                dtgRacionesCargadas.Rows[fila].Cells["Total"].Value = totalRegistro;
                total = total + totalRegistro;
            }

            txtTotal.Text = total.ToString();

            // Ajustes visuales
            dtgRacionesCargadas.Columns[0].Frozen = true;
            dtgRacionesCargadas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //dtgRacionesCargadas.DataSource = listaPlanillaLiquidacion;

            if (listaPlanillaLiquidacion.Count > 0)
            {
                //dtgRacionesCargadas.Columns[1].Width = 50;
                //dtgRacionesCargadas.Columns[2].Width = 50;
                //dtgRacionesCargadas.Columns[3].Width = 50;
                //dtgRacionesCargadas.Columns[4].Width = 50;
                //dtgRacionesCargadas.Columns[5].Width = 50;
                //dtgRacionesCargadas.Columns[6].Width = 50;
                //dtgRacionesCargadas.Columns[7].Width = 50;
                //dtgRacionesCargadas.Columns[8].Width = 50;
                //dtgRacionesCargadas.Columns[9].Width = 50;
                //dtgRacionesCargadas.Columns[10].Width = 50;
                //dtgRacionesCargadas.Columns[11].Width = 50;
                //dtgRacionesCargadas.Columns[12].Width = 50;
                //dtgRacionesCargadas.Columns[13].Width = 50;
                //dtgRacionesCargadas.Columns[14].Width = 50;
                //dtgRacionesCargadas.Columns[15].Width = 50;
                //dtgRacionesCargadas.Columns[16].Width = 50;
                //dtgRacionesCargadas.Columns[17].Width = 50;
                //dtgRacionesCargadas.Columns[18].Width = 50;
                //dtgRacionesCargadas.Columns[19].Width = 50;
                //dtgRacionesCargadas.Columns[20].Width = 50;
                //dtgRacionesCargadas.Columns[21].Width = 50;
                //dtgRacionesCargadas.Columns[22].Width = 50;
                //dtgRacionesCargadas.Columns[23].Width = 50;
                //dtgRacionesCargadas.Columns[24].Width = 50;
                //dtgRacionesCargadas.Columns[25].Width = 50;
                //dtgRacionesCargadas.Columns[26].Width = 50;
                //dtgRacionesCargadas.Columns[27].Width = 50;
                //dtgRacionesCargadas.Columns[28].Width = 50;
                //dtgRacionesCargadas.Columns[29].Width = 50;
                //dtgRacionesCargadas.Columns[30].Width = 50;
                //dtgRacionesCargadas.Columns[31].Width = 50;
                //dtgRacionesCargadas.Columns[32].Width = 50;
                //dtgRacionesCargadas.Columns[33].Width = 60;
                //dtgRacionesCargadas.Columns[34].Width = 60;
                //dtgRacionesCargadas.Columns[35].Width = 60;
                //dtgRacionesCargadas.Columns[36].Width = 60;
                //dtgRacionesCargadas.Columns[37].Width = 50;
                //dtgRacionesCargadas.Columns[38].Width = 50;
                //dtgRacionesCargadas.Columns[39].Width = 50;
                //dtgRacionesCargadas.Columns[40].Width = 50;

                //dtgRacionesCargadas.Columns[1].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[2].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[3].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[4].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[9].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[10].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[11].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[12].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[17].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[18].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[19].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[20].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[25].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[26].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[27].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[28].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[33].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[34].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[35].DefaultCellStyle.BackColor = Color.SandyBrown;
                //dtgRacionesCargadas.Columns[36].DefaultCellStyle.BackColor = Color.SandyBrown;

            }

        }//FIN METODO PARA OBTENER ELABORADAS CARGADAS..............................................

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (this.listaPlanillaLiquidacionGlobal.Count == 0)
            {
                MessageBox.Show("No hay registros cargados en este periodo", "Nutriocion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //obtener encabezados
            List<string> encabezados = new List<string>();
            foreach (DataGridViewColumn columna in dtgRacionesCargadas.Columns)
            {
                encabezados.Add(columna.HeaderText);
            }
            //obtener filas
            List<string[]> filas = new List<string[]>();
            foreach (DataGridViewRow fila in dtgRacionesCargadas.Rows)
            {
                if (!fila.IsNewRow)
                {
                    string[] datosFila = new string[dtgRacionesCargadas.Columns.Count];

                    for (int i = 0; i < dtgRacionesCargadas.Columns.Count; i++)
                    {
                        datosFila[i] = fila.Cells[i].Value?.ToString();
                    }

                    filas.Add(datosFila);
                }
            }

            // Generar PDF en memoria
            MemoryStream msOriginal = ReportesElaboradasPDF.RepPdfPlanillaLiquidacion(encabezados, filas, txtTotal.Text);

            // Clonar el stream para que PdfiumViewer pueda cerrarlo sin afectar el original
            MemoryStream ms = new MemoryStream(msOriginal.ToArray());

            PdfDocument pdfDocument = null;

            try
            {
                pdfDocument = PdfDocument.Load(ms);

                Form formVisor = new Form
                {
                    Text = "Vista previa PDF",
                    Width = 800,
                    Height = 600
                };

                PdfViewer pdfViewer = new PdfViewer
                {
                    Dock = DockStyle.Fill,
                    Document = pdfDocument
                };

                formVisor.Controls.Add(pdfViewer);

                formVisor.FormClosed += (s, args) =>
                {
                    // Liberar recursos al cerrar el visor
                    pdfViewer.Document.Dispose();
                    pdfViewer.Dispose();
                    formVisor.Dispose();
                    ms.Dispose();
                    pdfDocument = null;
                };

                formVisor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar PDF: " + ex.Message);
                ms.Dispose();
                pdfDocument?.Dispose();
            }
        }
    }
}
