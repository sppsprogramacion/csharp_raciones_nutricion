using CapaDatos;
using CapaNegocio;
using CapaPresentacion.FuncionesGenerales;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormSolicitadasEditar : Form
    {
        //VARIABLES GLOBALES
        int idRacionSolicitadaGlobal = 0;
        int idUnidadGlobal = 0;

        public FormSolicitadasEditar(int idRacionSolicitada, int idUnidad, string unidad, string fechaSolicitada)
        {
            InitializeComponent();
            idRacionSolicitadaGlobal = idRacionSolicitada;
            idUnidadGlobal = idUnidad;

            txtIdRacionSolicitada.Text = idRacionSolicitadaGlobal.ToString();
            txtFechaElaborada.Text = fechaSolicitada;
            txtUnidad.Text = unidad;
        }

        private void FormSolicitadasEditar_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);

            this.ActualizarUnidadesCantidades();

            //Carga datagrid SapMenus
            NSapMenu nSapMenu = new NSapMenu();
            int id_unidad = Convert.ToInt32(this.idUnidadGlobal);

            //Ccargar Sap y Menus de la unidad
            (List<DSapMenu> listaSapMenu, string errorResponse) = nSapMenu.ListarxUnidad(id_unidad);

            if (errorResponse != null)
            {
                MessageBox.Show(errorResponse, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaSapMenu = listaSapMenu
                .OrderBy(s => s.tipo_menu.orden)
                .ToList();

            var datosfiltrados = listaSapMenu
                .Select(c => new
                {
                    IdSap = c.sap_id,
                    Sap = c.sap.sap,
                    IdTipoMenu = c.tipo_menu_id,
                    TipoMenu = c.tipo_menu.tipo_menu,
                })
                .ToList();

            dtgTiposMenus.DataSource = null;
            dtgTiposMenus.Columns.Clear();
            dtgTiposMenus.Rows.Clear();
            dtgTiposMenus.DataSource = datosfiltrados;
            dtgTiposMenus.Columns["IdSap"].Visible = false;
            dtgTiposMenus.Columns["IdTipoMenu"].Visible = false;

            if (listaSapMenu.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                dtgTiposMenus.Columns[1].Width = 180;
                dtgTiposMenus.Columns[3].Width = 130;
            }
        }

        //ACTUALIZAR UNIADES CANTIDADES
        private void ActualizarUnidadesCantidades()
        {
            //Listar Detalles elaboradas
            NRacionSolicitadaDetalles nSolicitadasDetalles = new NRacionSolicitadaDetalles();

            (List<DRacionesSolicitadasDetalles> listaDetalles, string errorResponseDetalles) = nSolicitadasDetalles.ListarXIdRacionSolicitadaXUnidad(idRacionSolicitadaGlobal, idUnidadGlobal);

            if (errorResponseDetalles != null)
            {
                MessageBox.Show(errorResponseDetalles, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaDetalles = listaDetalles
                .OrderBy(s => s.tipo_menu.orden)
                .ToList();
            //fin Listar Detalles elaboradas

            var datosfiltrados = listaDetalles
                .Select(c => new
                {
                    Identificador = c.id_racion_solicitada_detalle,
                    Unidad = c.unidad.unidad,
                    TipoMenu = c.tipo_menu.tipo_menu,
                    Almuerzo = c.almuerzo,
                    Cena = c.cena

                })
                .ToList();

            dtgRacionesCargadas.DataSource = datosfiltrados;

            if (listaDetalles.Count == 0)
            {
                MessageBox.Show("No se encontraron raciones cargadas para esta unidad", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                dtgRacionesCargadas.Columns[0].Width = 80;
                dtgRacionesCargadas.Columns[1].Width = 150;
                dtgRacionesCargadas.Columns[2].Width = 130;
                dtgRacionesCargadas.Columns[3].Width = 50;
                dtgRacionesCargadas.Columns[4].Width = 50;
            }
        }//FIN ACTUALIZAR UNIADES CANTIDADES..................................................

        private void dtgRacionesCargadas_KeyDown(object sender, KeyEventArgs e)
        {
            //AL PRESIONAR ENTER MOSTRAR EL TRAMITE
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (dtgRacionesCargadas.SelectedRows.Count > 0)
                {
                    int idSolictadaDetalle;
                    idSolictadaDetalle = Convert.ToInt32(dtgRacionesCargadas.CurrentRow.Cells["Identificador"].Value.ToString());

                    if (idSolictadaDetalle > 0)
                    {
                        //cargar datos de datagrid a controles
                        txtIdSolicitadaDetalle.Text = idSolictadaDetalle.ToString();
                        txtMenu.Text = dtgRacionesCargadas.CurrentRow.Cells["TipoMenu"].Value.ToString();
                        txtAlmuerzo.Text = dtgRacionesCargadas.CurrentRow.Cells["Almuerzo"].Value.ToString();
                        txtCena.Text = dtgRacionesCargadas.CurrentRow.Cells["Cena"].Value.ToString();

                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un registro.", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdSolicitadaDetalle.Text))
            {
                MessageBox.Show("Debe seleccionar un registro", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var nRacionSolicitadaDetalles = new NRacionSolicitadaDetalles();

                var detalle = new DRacionesSolicitadasDetalles
                {
                    id_racion_solicitada_detalle = Convert.ToInt32(txtIdSolicitadaDetalle.Text),
                    almuerzo = Convert.ToInt32(txtAlmuerzo.Text),
                    cena = Convert.ToInt32(txtCena.Text)
                };

                nRacionSolicitadaDetalles.EditarDetalle(detalle);
                MessageBox.Show("Editado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtIdSolicitadaDetalle.Text = string.Empty;
                txtMenu.Text = string.Empty;
                txtAlmuerzo.Text = string.Empty;
                txtCena.Text = string.Empty;

                this.ActualizarUnidadesCantidades();

                //this.LimpiarControles();
                //this.HabilitarControles(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtIdSolicitadaDetalle.Text = string.Empty;
            txtMenu.Text = string.Empty;
            txtAlmuerzo.Text = string.Empty;
            txtCena.Text = string.Empty;
        }

        private void gboxCargarUna_Enter(object sender, EventArgs e)
        {

        }

        private void dtgTiposMenus_KeyDown(object sender, KeyEventArgs e)
        {
            //AL PRESIONAR ENTER MOSTRAR EL TRAMITE
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (dtgRacionesCargadas.SelectedRows.Count > 0)
                {                    
                    //cargar datos de datagrid a controles
                    txtIdMenuCargarUna.Text = txtMenuCargarUna.Text = dtgTiposMenus.CurrentRow.Cells["IdTipoMenu"].Value.ToString();
                    txtMenuCargarUna.Text = dtgTiposMenus.CurrentRow.Cells["TipoMenu"].Value.ToString();
                    txtIdSapCargarUna.Text = dtgTiposMenus.CurrentRow.Cells["IdSap"].Value.ToString();
                    
                }
            }
        }

        private void btnGuardarCargaUna_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdRacionSolicitada.Text))
            {
                MessageBox.Show("No hay una elaborada cargada", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtIdMenuCargarUna.Text))
            {
                MessageBox.Show("Debe seleccionar un registro", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var entidad = new DRacionesSolicitadasDetalles
            {
                racion_solicitada_id = Convert.ToInt32(txtIdRacionSolicitada.Text),
                sap_id = Convert.ToInt32(txtIdSapCargarUna.Text),
                unidad_id = Convert.ToInt32(this.idUnidadGlobal),
                tipo_menu_id = Convert.ToInt32(txtIdMenuCargarUna.Text),
                almuerzo = Convert.ToInt32(txtAlmuerzoCargaUna.Text),
                cena = Convert.ToInt32(txtCenaCargaUna.Text),
                usuario_id = 1
                // fecha_carga NO se asigna
            };

            try
            {
                var dao = new NRacionSolicitadaDetalles();
                dao.InsertarUnDetalle(entidad);

                MessageBox.Show("Datos guardados correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ActualizarUnidadesCantidades();

                txtIdMenuCargarUna.Text = string.Empty;
                txtMenuCargarUna.Text = string.Empty;
                txtIdSapCargarUna.Text = string.Empty;
                txtAlmuerzoCargaUna.Text = string.Empty;
                txtCenaCargaUna.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancelarCargaUna_Click(object sender, EventArgs e)
        {
            txtIdMenuCargarUna.Text = string.Empty;
            txtMenuCargarUna.Text = string.Empty;
            txtIdSapCargarUna.Text = string.Empty;
            txtAlmuerzoCargaUna.Text = string.Empty;
            txtCenaCargaUna.Text = string.Empty;
        }
    }
}
