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
    public partial class FormElaboradasNuevo : Form
    {
        public string IdRacionElaborada { get; private set; }
        public string FechaElaborada { get; private set; }
        public string Convenio { get; private set; }
        public string Detalles { get; private set; }
        public string FechaCarga { get; private set; }

        public FormElaboradasNuevo()
        {
            InitializeComponent();
        }

        private void FormElaboradasNuevo_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);


            this.CargarDataElaboradas();
        }


        //METODO PARA OBTENER LA LISTA DE USUARIOS
        private void CargarDataElaboradas()
        {
            var nRacionElaborada = new NRacionElaborada();

            var (listaRacionElaboradas, error) = nRacionElaborada.ListarTodos();



            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var datosfiltrados = listaRacionElaboradas
                .Select(c => new
                {
                    Id = c.id_racion_elaborada,
                    Convenio = c.convenio,
                    FechaElaborada = c.fecha_elaborada,
                    Detalles = c.detalles,
                    FechaCarga = c.fecha_carga

                })
                .ToList();

            dtgRacionesSolicitadas.DataSource = datosfiltrados;

            if (listaRacionElaboradas.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                dtgRacionesSolicitadas.Columns[3].Width = 180;
            }
        }

        //FIN METODO PARA OBTENER LA LISTA DE USUARIOS..............................................

        //LIMPIAR CONTROLES
        private void LimpiarControles()
        {
            txtConvenio.Text = string.Empty;
            dtpFechaSolicitada.Text = string.Empty;
            txtDetalles.Text = string.Empty;

        }
        //FIN LIMPIAR CONTROLES

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var nRacionElaborada = new NRacionElaborada();

                var racionElaborada = new DRacionElaborada
                {
                    fecha_elaborada = dtpFechaSolicitada.Value,
                    convenio = txtConvenio.Text,
                    detalles = txtDetalles.Text,
                };

                nRacionElaborada.CrearRacionSolicitada(racionElaborada);
                MessageBox.Show("Creado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.CargarDataElaboradas();

                this.LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dtgRacionesSolicitadas_KeyDown(object sender, KeyEventArgs e)
        {
            //AL PRESIONAR ENTER MOSTRAR
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (dtgRacionesSolicitadas.SelectedRows.Count > 0)
                {
                    IdRacionElaborada = Convert.ToString(this.dtgRacionesSolicitadas.CurrentRow.Cells["Id"].Value);
                    Convenio = Convert.ToString(this.dtgRacionesSolicitadas.CurrentRow.Cells["Convenio"].Value);
                    FechaElaborada = Convert.ToString(this.dtgRacionesSolicitadas.CurrentRow.Cells["FechaElaborada"].Value);
                    Detalles = Convert.ToString(this.dtgRacionesSolicitadas.CurrentRow.Cells["Detalles"].Value);
                    FechaCarga = Convert.ToString(this.dtgRacionesSolicitadas.CurrentRow.Cells["FechaCarga"].Value);

                    this.DialogResult = DialogResult.OK; // Para indicar que cerró bien
                    this.Close();

                }//fin if
                else
                {
                    MessageBox.Show("Debe seleccionar un interno.");
                }

            }
        }
    }
}
