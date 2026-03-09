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
    public partial class FormElaboradasEditar : Form
    {

        //VARIABLES GLOBALES
        int idRacionElaboradaGlobal = 0;
        int idUnidadGlobal = 0;

        public FormElaboradasEditar(int idRacionElaborada, int idUnidad, string unidad, string fechaElaborada)
        {
            InitializeComponent();
            idRacionElaboradaGlobal = idRacionElaborada;
            idUnidadGlobal = idUnidad;

            txtIdRacionElaborada.Text = idRacionElaboradaGlobal.ToString();
            txtFechaElaborada.Text = fechaElaborada;
            txtUnidad.Text = unidad;

        }

        private void FormElaboradasEditar_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);

            this.ActualizarUnidadesCantidades();
        }
        
        private void dtgRacionesCargadas_KeyDown(object sender, KeyEventArgs e)
        {
            //AL PRESIONAR ENTER MOSTRAR EL TRAMITE
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (dtgRacionesCargadas.SelectedRows.Count > 0)
                {
                    int idElaboradaDetalle;
                    idElaboradaDetalle = Convert.ToInt32(dtgRacionesCargadas.CurrentRow.Cells["Identificador"].Value.ToString());

                    if (idElaboradaDetalle > 0)
                    {
                        //cargar datos de datagrid a controles
                        txtIdElaboradaDetalle.Text = idElaboradaDetalle.ToString();
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
            if (string.IsNullOrEmpty(txtIdElaboradaDetalle.Text))
            {
                MessageBox.Show("Debe seleccionar un registro", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var nRacionElaboradaDetalle = new NRacionElaboradaDetalles();

                var detalle = new DRacionElaboradaDetalles
                {
                    id_racion_elaboradas_detalle = Convert.ToInt32(txtIdElaboradaDetalle.Text),
                    almuerzo = Convert.ToInt32(txtAlmuerzo.Text),
                    cena = Convert.ToInt32(txtCena.Text)
                };

                nRacionElaboradaDetalle.EditarDetalle(detalle);
                MessageBox.Show("Editado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txtIdElaboradaDetalle.Text = string.Empty;
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
            txtIdElaboradaDetalle.Text = string.Empty;
            txtMenu.Text = string.Empty;
            txtAlmuerzo.Text = string.Empty;   
            txtCena.Text = string.Empty;
        }

        //ACTUALIZAR UNIADES CANTIDADES
        private void ActualizarUnidadesCantidades()
        {
            //Listar Detalles elaboradas
            NRacionElaboradaDetalles nElaboradasDetalles = new NRacionElaboradaDetalles();

            (List<DRacionElaboradaDetalles> listaDetalles, string errorResponseDetalles) = nElaboradasDetalles.ListarXIdRacionElaboradaXUnidad(idRacionElaboradaGlobal, idUnidadGlobal);

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
                    Identificador = c.id_racion_elaboradas_detalle,
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
        }//FIN ACTUALIZAR UNIADES CANTIDADES...........................................................

    }
}
