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
    public partial class FormCargaSolicitadasCantidadesSap : Form
    {
        //VARIABLES GLOBALES
        int idRacionSolicitadaGlobal = 0;

        public FormCargaSolicitadasCantidadesSap(int idRacionSolicitada)
        {
            InitializeComponent();
            idRacionSolicitadaGlobal = idRacionSolicitada;

        }

        private void FormCargaSolicitadasCantidadesSap_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);

            
            this.ActualizarSapCantidades();
        }

        //ACTUALIZAR SAP CANTIDADES
        private void ActualizarSapCantidades()
        {
            if (string.IsNullOrEmpty(idRacionSolicitadaGlobal.ToString()) || idRacionSolicitadaGlobal == 0)
            {
                MessageBox.Show("No hay una Racion Solicitad selecionada", "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idRacionSolicitada = Convert.ToInt32(this.idRacionSolicitadaGlobal.ToString());


            //Listar Sap
            NSap nSap = new NSap();
            (List<DSap> listaSap, string errorResponseSap) = nSap.ListarTodos();

            if (errorResponseSap != null)
            {
                MessageBox.Show(errorResponseSap, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaSap = listaSap
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar Sap

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

            //Listar Detalles solicitadas
            NRacionSolicitadaDetalles nSolicitadasDetalles = new NRacionSolicitadaDetalles();

            (List<DRacionesSolicitadasDetalles> listaSolicitadasDetalles, string errorResponseSolicitadasDEtalles) = nSolicitadasDetalles.ListarXIdRacionSolicitada(idRacionSolicitada);

            if (errorResponseSolicitadasDEtalles != null)
            {
                MessageBox.Show(errorResponseSolicitadasDEtalles, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaSolicitadasDetalles = listaSolicitadasDetalles
                .OrderBy(s => s.tipo_menu.orden)
                .ToList();
            //fin Listar Detalles solicitadas

            //contar valores de menus en cada sap
            List<DSapMenuCantidades> listaSapCantidades = new List<DSapMenuCantidades>();

            List<DRacionesSolicitadasDetalles> listaFiltroDetallesXSap = new List<DRacionesSolicitadasDetalles>();
            foreach (DSap sap in listaSap)
            {
                // 🔴 NUEVA instancia en cada vuelta
                var sapCantidades = new DSapMenuCantidades();
                sapCantidades.sap = sap.sap;

                foreach (DTipoMenu tipoMenu in listaTipoMenu)
                {
                    listaFiltroDetallesXSap = listaSolicitadasDetalles.Where(x => x.sap_id == sap.id_sap && x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();
                    int desayuno = 0;
                    int almuerzo = 0;
                    int merienda = 0;
                    int cena = 0;

                    foreach (DRacionesSolicitadasDetalles detalle in listaFiltroDetallesXSap)
                    {
                        almuerzo = almuerzo + detalle.almuerzo;
                        cena = cena + detalle.cena;
                    }

                    if (tipoMenu.id_tipo_menu == 1)
                    {
                        sapCantidades.P12_A = almuerzo;
                        sapCantidades.P12_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 2)
                    {
                        sapCantidades.P24_A = almuerzo;
                        sapCantidades.P24_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        sapCantidades.IntN_A = almuerzo;
                        sapCantidades.IntN_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 4)
                    {
                        sapCantidades.Astr_A = almuerzo;
                        sapCantidades.Astr_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 5)
                    {
                        sapCantidades.Celi_A = almuerzo;
                        sapCantidades.Celi_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 6)
                    {
                        sapCantidades.AFib_A = almuerzo;
                        sapCantidades.AFib_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 7)
                    {
                        sapCantidades.Hep_A = almuerzo;
                        sapCantidades.Hep_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 8)
                    {
                        sapCantidades.SSal_A = almuerzo;
                        sapCantidades.SSal_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 9)
                    {
                        sapCantidades.HivTbc_A = almuerzo;
                        sapCantidades.HivTbc_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 10)
                    {
                        sapCantidades.Men_A = almuerzo;
                        sapCantidades.Men_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 11)
                    {
                        sapCantidades.SobreAl_A = almuerzo;
                        sapCantidades.SobreAl_C = cena;
                    }

                }

                listaSapCantidades.Add(sapCantidades);
            }

            //fin contar valores de menus en cada sap

            AgregarFilaTotalesSap(listaSapCantidades);

            dtgSapCantidades.DataSource = null;
            dtgSapCantidades.DataSource = listaSapCantidades;

            //formato columnas
            if (listaSapCantidades.Count > 0)
            {
                dtgSapCantidades.Columns[0].Width = 150;
                dtgSapCantidades.Columns[1].Width = 50;
                dtgSapCantidades.Columns[2].Width = 50;
                dtgSapCantidades.Columns[3].Width = 50;
                dtgSapCantidades.Columns[4].Width = 50;
                dtgSapCantidades.Columns[5].Width = 50;
                dtgSapCantidades.Columns[6].Width = 50;
                dtgSapCantidades.Columns[7].Width = 50;
                dtgSapCantidades.Columns[8].Width = 50;
                dtgSapCantidades.Columns[9].Width = 50;
                dtgSapCantidades.Columns[10].Width = 50;
                dtgSapCantidades.Columns[11].Width = 50;
                dtgSapCantidades.Columns[12].Width = 50;
                dtgSapCantidades.Columns[13].Width = 50;
                dtgSapCantidades.Columns[14].Width = 50;
                dtgSapCantidades.Columns[15].Width = 50;
                dtgSapCantidades.Columns[16].Width = 50;
                dtgSapCantidades.Columns[17].Width = 50;
                dtgSapCantidades.Columns[18].Width = 50;
                dtgSapCantidades.Columns[19].Width = 50;
                dtgSapCantidades.Columns[20].Width = 50;
                dtgSapCantidades.Columns[21].Width = 50;
                dtgSapCantidades.Columns[22].Width = 50;

                dtgSapCantidades.Columns[1].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgSapCantidades.Columns[2].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgSapCantidades.Columns[5].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgSapCantidades.Columns[6].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgSapCantidades.Columns[9].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgSapCantidades.Columns[10].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgSapCantidades.Columns[13].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgSapCantidades.Columns[14].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgSapCantidades.Columns[17].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgSapCantidades.Columns[18].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgSapCantidades.Columns[21].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgSapCantidades.Columns[22].DefaultCellStyle.BackColor = Color.SandyBrown;

            }

            //formato fila Totales
            foreach (DataGridViewRow row in dtgSapCantidades.Rows)
            {
                if (row.Cells["sap"].Value?.ToString() == "Totales")
                {
                    row.DefaultCellStyle.Font =
                        new Font(dtgSapCantidades.Font, FontStyle.Bold);

                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.ForeColor = Color.Black;

                    break; // solo una fila
                }
            }
        }
        //FIN ACTUALIZAR SAP CANTIDADES......................................

        //AGREGAR FILA TOTALES SAP
        private void AgregarFilaTotalesSap(List<DSapMenuCantidades> lista)
        {
            // Evitar duplicar la fila Totales
            lista.RemoveAll(x => x.sap == "Totales");

            var totales = new DSapMenuCantidades
            {
                sap = "Totales",

                P12_A = lista.Sum(x => x.P12_A),
                P12_C = lista.Sum(x => x.P12_C),

                P24_A = lista.Sum(x => x.P24_A),
                P24_C = lista.Sum(x => x.P24_C),

                IntN_A = lista.Sum(x => x.IntN_A),
                IntN_C = lista.Sum(x => x.IntN_C),

                Astr_A = lista.Sum(x => x.Astr_A),
                Astr_C = lista.Sum(x => x.Astr_C),

                Celi_A = lista.Sum(x => x.Celi_A),
                Celi_C = lista.Sum(x => x.Celi_C),

                AFib_A = lista.Sum(x => x.AFib_A),
                AFib_C = lista.Sum(x => x.AFib_C),

                Hep_A = lista.Sum(x => x.Hep_A),
                Hep_C = lista.Sum(x => x.Hep_C),

                SSal_A = lista.Sum(x => x.SSal_A),
                SSal_C = lista.Sum(x => x.SSal_C),

                HivTbc_A = lista.Sum(x => x.HivTbc_A),
                HivTbc_C = lista.Sum(x => x.HivTbc_C),

                Men_A = lista.Sum(x => x.Men_A),
                Men_C = lista.Sum(x => x.Men_C),

                SobreAl_A = lista.Sum(x => x.SobreAl_A),
                SobreAl_C = lista.Sum(x => x.SobreAl_C),
            };

            lista.Add(totales);

        } // FIN AGREGAR FILA TOTALES SAP............................................................


    }
}
