using CapaDatos;
using CapaNegocio;
using CapaPresentacion.FuncionesGenerales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormRacionesCargadas : Form
    {
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
                planillaLiquidacion.fecha = racionElaborada.fecha_elaborada.ToShortDateString();


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
                        planillaLiquidacion.P12_D = desayuno;
                        planillaLiquidacion.P12_A = almuerzo;
                        planillaLiquidacion.P12_M = merienda;
                        planillaLiquidacion.P12_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "P12_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "P12_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "P12_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "P12_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);

                    }
                    if (tipoMenu.id_tipo_menu == 2)
                    {
                        planillaLiquidacion.P24_D = desayuno;
                        planillaLiquidacion.P24_A = almuerzo;
                        planillaLiquidacion.P24_M = merienda;
                        planillaLiquidacion.P24_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores5 = new DFactores();
                        dFactores5.tipo_menu = "P24_D";
                        dFactores5.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores5);

                        // 🔴 NUEVA instancia
                        var dFactores6 = new DFactores();
                        dFactores6.tipo_menu = "P24_A";
                        dFactores6.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores6);

                        // 🔴 NUEVA instanciaa
                        var dFactores7 = new DFactores();
                        dFactores7.tipo_menu = "P24_M";
                        dFactores7.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores7);

                        // 🔴 NUEVA instancia
                        var dFactores8 = new DFactores();
                        dFactores8.tipo_menu = "P24_C";
                        dFactores8.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores8);
                    }
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        planillaLiquidacion.IntN_D = desayuno;
                        planillaLiquidacion.IntN_A = almuerzo;
                        planillaLiquidacion.IntN_M = merienda;
                        planillaLiquidacion.IntN_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "IntN_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "IntN_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "IntN_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "IntN_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 4)
                    {
                        planillaLiquidacion.Astr_D = desayuno;
                        planillaLiquidacion.Astr_A = almuerzo;
                        planillaLiquidacion.Astr_M = merienda;
                        planillaLiquidacion.Astr_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "Astr_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "Astr_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "Astr_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "Astr_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 5)
                    {
                        planillaLiquidacion.Celi_D = desayuno;
                        planillaLiquidacion.Celi_A = almuerzo;
                        planillaLiquidacion.Celi_M = merienda;
                        planillaLiquidacion.Celi_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "Celi_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "Celi_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "Celi_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "Celi_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 6)
                    {
                        planillaLiquidacion.AFib_D = desayuno;
                        planillaLiquidacion.AFib_A = almuerzo;
                        planillaLiquidacion.AFib_M = merienda;
                        planillaLiquidacion.AFib_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "AFib_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "AFib_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "AFib_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "AFib_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 7)
                    {
                        planillaLiquidacion.Hep_D = desayuno;
                        planillaLiquidacion.Hep_A = almuerzo;
                        planillaLiquidacion.Hep_M = merienda;
                        planillaLiquidacion.Hep_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "Hep_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "Hep_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "Hep_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "Hep_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 8)
                    {
                        planillaLiquidacion.SSal_D = desayuno;
                        planillaLiquidacion.SSal_A = almuerzo;
                        planillaLiquidacion.SSal_M = merienda;
                        planillaLiquidacion.SSal_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "SSal_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "SSal_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "SSal_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "SSal_C";
                        dFactores4.factor = tipoMenu.menu.factor_cena;
                        listaFactores.Add(dFactores4);
                    }
                    if (tipoMenu.id_tipo_menu == 9)
                    {
                        planillaLiquidacion.HivTbc_D = desayuno;
                        planillaLiquidacion.HivTbc_A = almuerzo;
                        planillaLiquidacion.HivTbc_M = merienda;
                        planillaLiquidacion.HivTbc_C = cena;

                        // 🔴 NUEVA instanciaa
                        var dFactores1 = new DFactores();
                        dFactores1.tipo_menu = "HivTbc_D";
                        dFactores1.factor = tipoMenu.menu.factor_desayuno;
                        listaFactores.Add(dFactores1);

                        // 🔴 NUEVA instancia
                        var dFactores2 = new DFactores();
                        dFactores2.tipo_menu = "HivTbc_A";
                        dFactores2.factor = tipoMenu.menu.factor_almuerzo;
                        listaFactores.Add(dFactores2);

                        // 🔴 NUEVA instanciaa
                        var dFactores3 = new DFactores();
                        dFactores3.tipo_menu = "HivTbc_M";
                        dFactores3.factor = tipoMenu.menu.factor_merienda;
                        listaFactores.Add(dFactores3);

                        // 🔴 NUEVA instancia
                        var dFactores4 = new DFactores();
                        dFactores4.tipo_menu = "HivTbc_C";
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
                        planillaLiquidacion.IntN_D = planillaLiquidacion.IntN_D + desayuno;
                        planillaLiquidacion.IntN_A = planillaLiquidacion.IntN_A + almuerzo;
                        planillaLiquidacion.IntN_M = planillaLiquidacion.IntN_M + merienda;
                        planillaLiquidacion.IntN_C = planillaLiquidacion.IntN_C + cena;

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

        
    }
}
