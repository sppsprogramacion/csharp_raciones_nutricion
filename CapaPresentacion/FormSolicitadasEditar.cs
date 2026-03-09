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
                dtgRacionesCargadas.Columns[2].Width = 150;
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
    }
}
