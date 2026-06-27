using CapaDatos;
using CapaNegocio;
using CapaPresentacion.FuncionesGenerales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormRacionesSolicitadas : Form
    {
        public FormRacionesSolicitadas()
        {
            InitializeComponent();
        }

        private void FormRacionesSolicitadas_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            this.CargarDataSolicitadas();
        }

        //METODO PARA OBTENER ELABORADAS CARGADAS
        private void CargarDataSolicitadas()
        {
            decimal total = 0;

            var nRacionSolicitada = new NRacionSolicitada();

            var (listaRacionSolicitadas, error) = nRacionSolicitada.ListaXFecha(dtpFechaInicio.Value.ToString("yyyy-MM-dd"), dtpFechaFin.Value.ToString("yyyy-MM-dd"));

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listaRacionSolicitadas.Count() <= 0)
            {
                MessageBox.Show("No se encontraron cargas en este rango de fechas", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            listaRacionSolicitadas = listaRacionSolicitadas
                .OrderBy(s => s.fecha_solicitada)
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

            foreach (DRacionSolicitada racionSolicitada in listaRacionSolicitadas)
            {
                // 🔴 NUEVA instancia en cada vuelta
                var planillaLiquidacion = new DPlanillaLiquidacion();
                planillaLiquidacion.fecha = racionSolicitada.fecha_solicitada.ToString("dd-MM");


                List<DRacionesSolicitadasDetalles> listaDetalles = racionSolicitada.raciones_solicitadas_detalles.ToList();

                List<DRacionesSolicitadasDetalles> listaFiltroDetallesXTipoMenu = new List<DRacionesSolicitadasDetalles>();
                foreach (DTipoMenu tipoMenu in listaTipoMenu)
                {

                    listaFiltroDetallesXTipoMenu = listaDetalles.Where(x => x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();
                    int desayuno = 0;
                    int almuerzo = 0;
                    int merienda = 0;
                    int cena = 0;

                    foreach (DRacionesSolicitadasDetalles detalle in listaFiltroDetallesXTipoMenu)
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

            }

        }//FIN METODO PARA OBTENER ELABORADAS CARGADAS..............................................

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
    }
}
