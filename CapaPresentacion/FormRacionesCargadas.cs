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

                        if ((tipoMenu.id_tipo_menu == 1 || tipoMenu.id_tipo_menu == 2) && (detalle.unidad_id == 14 || detalle.unidad_id == 13))
                        {
                            planillaLiquidacion.Int_NORM_D = planillaLiquidacion.Int_NORM_D + detalle.almuerzo;
                            planillaLiquidacion.Int_NORM_A = planillaLiquidacion.Int_NORM_A + detalle.almuerzo;
                            planillaLiquidacion.Int_NORM_M = planillaLiquidacion.Int_NORM_M + detalle.cena;
                            planillaLiquidacion.Int_NORM_C = planillaLiquidacion.Int_NORM_C + detalle.cena;
                        }
                        else
                        {
                            desayuno = desayuno + detalle.almuerzo;
                            almuerzo = almuerzo + detalle.almuerzo;
                            merienda = merienda + detalle.cena;
                            cena = cena + detalle.cena;
                        }

                        //desayuno = desayuno + detalle.almuerzo;
                        //almuerzo = almuerzo + detalle.almuerzo;
                        //merienda = merienda + detalle.cena;
                        //cena = cena + detalle.cena;



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
                        //planillaLiquidacion.Int_NORM_D = desayuno;
                        //planillaLiquidacion.Int_NORM_A = almuerzo;
                        //planillaLiquidacion.Int_NORM_M = merienda;
                        //planillaLiquidacion.Int_NORM_C = cena;

                        //los hijos menores son contados como racion de internos normal
                        planillaLiquidacion.Int_NORM_D = planillaLiquidacion.Int_NORM_D + desayuno;
                        planillaLiquidacion.Int_NORM_A = planillaLiquidacion.Int_NORM_A + almuerzo;
                        planillaLiquidacion.Int_NORM_M = planillaLiquidacion.Int_NORM_M + merienda;
                        planillaLiquidacion.Int_NORM_C = planillaLiquidacion.Int_NORM_C + cena;

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

            //--Creacion de datagrid ---
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

            // Agrega Columnas finales
            dtgRacionesCargadas.Columns.Add("Subtotal", "Subtotal");
            dtgRacionesCargadas.Columns.Add("Factor", "Factor");
            dtgRacionesCargadas.Columns.Add("Total", "RACION");

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
                

            }

        }//FIN METODO PARA OBTENER ELABORADAS CARGADAS..............................................

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            int numero_rendicion;

            if (!int.TryParse(txtNumeroRendicion.Text, out numero_rendicion))
            {
                MessageBox.Show("Debe ingresar un número entero válido en Nº RENDICION.", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNumeroRendicion.Focus();
                return;
            }

            if (this.listaPlanillaLiquidacionGlobal.Count == 0)
            {
                MessageBox.Show("No hay registros cargados en este periodo", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //obtener encabezados
            List<string> encabezados = new List<string>();
            foreach (DataGridViewColumn columna in dtgRacionesCargadas.Columns)
            {
                encabezados.Add(columna.HeaderText);
            }
            //obtener filas para enviar
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
            //obtener filas para enviar

            //Obtener filas-lista de 2da planilla

            var nRacionElaboradaDetalles = new NRacionElaboradaDetalles();

            var (listaRacionElaboradasDetalles, error) = nRacionElaboradaDetalles.ListaXFechaElaborada(dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listaRacionElaboradasDetalles.Count() <= 0)
            {
                MessageBox.Show("No se encontraron cargas en este rango de fechas", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            listaRacionElaboradasDetalles = listaRacionElaboradasDetalles
                .OrderBy(s => s.racion_elaborada.fecha_elaborada)
                .ToList();

            //Listar Menus
            NMenu nMenu = new NMenu();
            (List<DMenu> listaMenus, string errorResponseMenu) = nMenu.ListarTodos();

            if (errorResponseMenu != null)
            {
                MessageBox.Show(errorResponseMenu, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaMenus = listaMenus
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar Menus           

            //contar valores de menus en cada menu
            List<DPlanillaLiquidacion2da> listaPlanillaLiquidacion2da = new List<DPlanillaLiquidacion2da>();

            List<DRacionElaboradaDetalles> listaFiltroDetallesXMenu = new List<DRacionElaboradaDetalles>();

            List<DMenu> listaMenusFiltro = listaMenus.Where(x => x.id_menu != 7).ToList();

            foreach (DMenu menu in listaMenusFiltro)
            {
                if(menu.id_menu == 1 || menu.id_menu == 2){
                    listaFiltroDetallesXMenu = listaRacionElaboradasDetalles.Where(x => x.tipo_menu.menu_id == menu.id_menu && x.tipo_menu.menu_id != 7 && x.unidad_id != 13 && x.unidad_id != 14).ToList();

                }

                if (menu.id_menu == 6)
                {
                    listaFiltroDetallesXMenu = listaRacionElaboradasDetalles.Where(x => (x.tipo_menu.menu_id == menu.id_menu && x.tipo_menu.menu_id != 7) || (x.unidad_id == 13 || x.unidad_id == 14)).ToList();

                }
                if (menu.id_menu != 1 && menu.id_menu != 2 && menu.id_menu != 6)
                {
                    listaFiltroDetallesXMenu = listaRacionElaboradasDetalles.Where(x => x.tipo_menu.menu_id == menu.id_menu && x.tipo_menu.menu_id != 7).ToList();
                }

                int desayuno = 0;
                int almuerzo = 0;
                int merienda = 0;
                int cena = 0;

                foreach (DRacionElaboradaDetalles detalle in listaFiltroDetallesXMenu)
                {
                    desayuno = desayuno + detalle.almuerzo;
                    almuerzo = almuerzo + detalle.almuerzo;
                    merienda = merienda + detalle.cena;
                    cena = cena + detalle.cena;
                }

                // 🔴 NUEVA instancia en cada vuelta
                var planillaLiquidacion_1 = new DPlanillaLiquidacion2da();
                planillaLiquidacion_1.menu = menu.menu_descripcion + " Desayuno";
                planillaLiquidacion_1.subtotal = desayuno;
                planillaLiquidacion_1.factor = menu.factor_desayuno;
                planillaLiquidacion_1.racion = desayuno * menu.factor_desayuno;

                listaPlanillaLiquidacion2da.Add(planillaLiquidacion_1);

                var planillaLiquidacion_2 = new DPlanillaLiquidacion2da();
                planillaLiquidacion_2.menu = menu.menu_descripcion + " Almuerzo";
                planillaLiquidacion_2.subtotal = almuerzo;
                planillaLiquidacion_2.factor = menu.factor_almuerzo;
                planillaLiquidacion_2.racion = almuerzo * menu.factor_almuerzo;
                listaPlanillaLiquidacion2da.Add(planillaLiquidacion_2);

                var planillaLiquidacion_3 = new DPlanillaLiquidacion2da();
                planillaLiquidacion_3.menu = menu.menu_descripcion + " Merienda";
                planillaLiquidacion_3.subtotal = merienda;
                planillaLiquidacion_3.factor = menu.factor_merienda;
                planillaLiquidacion_3.racion = merienda * menu.factor_merienda;
                listaPlanillaLiquidacion2da.Add(planillaLiquidacion_3);

                var planillaLiquidacion_4 = new DPlanillaLiquidacion2da();
                planillaLiquidacion_4.menu = menu.menu_descripcion + " Cena";
                planillaLiquidacion_4.subtotal = cena;
                planillaLiquidacion_4.factor = menu.factor_cena;
                planillaLiquidacion_4.racion = cena * menu.factor_cena;
                listaPlanillaLiquidacion2da.Add(planillaLiquidacion_4);
            }
            //fin Obtener filas-lista de 2da planilla

            //
            // Generar PDF en memoria
            MemoryStream msOriginal=null;

            if (encabezados.Count <= 20)
            {
                msOriginal = ReportesElaboradasPDF.RepPdfPlanillaLiquidacionQuincenal(encabezados, filas, txtTotal.Text, listaPlanillaLiquidacion2da, numero_rendicion, dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"), dtpFechaRendicion.Value.ToString("yyyy-MM-dd"));
            }
            if (encabezados.Count > 20)
            {
                msOriginal = ReportesElaboradasPDF.RepPdfPlanillaLiquidacionMensual(encabezados, filas, txtTotal.Text, listaPlanillaLiquidacion2da, numero_rendicion, dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"), dtpFechaRendicion.Value.ToString("yyyy-MM-dd"));
            }
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
