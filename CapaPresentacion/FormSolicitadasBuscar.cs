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
    public partial class FormSolicitadasBuscar : Form
    {
        public string IdRacionSolicitada { get; private set; }
        public string FechaSolicitada { get; private set; }
        public string Convenio { get; private set; }
        public string Detalles { get; private set; }
        public string FechaCarga { get; private set; }

        public FormSolicitadasBuscar()
        {
            InitializeComponent();
        }

        private void FormSolicitadasBuscar_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);

            this.CargarDataSolicitadas();
        }

        private void dtgRacionesSolicitadas_KeyDown(object sender, KeyEventArgs e)
        {
            //AL PRESIONAR ENTER MOSTRAR
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (dtgRacionesSolicitadas.SelectedRows.Count > 0)
                {
                    IdRacionSolicitada = Convert.ToString(this.dtgRacionesSolicitadas.CurrentRow.Cells["Id"].Value);
                    Convenio = Convert.ToString(this.dtgRacionesSolicitadas.CurrentRow.Cells["Convenio"].Value);
                    FechaSolicitada = Convert.ToString(this.dtgRacionesSolicitadas.CurrentRow.Cells["Fecha_solicitada"].Value);
                    Detalles = Convert.ToString(this.dtgRacionesSolicitadas.CurrentRow.Cells["Detalles"].Value);
                    FechaCarga = Convert.ToString(this.dtgRacionesSolicitadas.CurrentRow.Cells["Fecha_carga"].Value);

                    this.DialogResult = DialogResult.OK; // Para indicar que cerró bien
                    this.Close();

                }//fin if
                else
                {
                    MessageBox.Show("Debe seleccionar un registro.");
                }

            }
        }

        //METODO PARA OBTENER LA LISTA DE USUARIOS
        private void CargarDataSolicitadas()
        {
            var nRacionSolicitada = new NRacionSolicitada();

            var (listaRacionSolicitada, error) = nRacionSolicitada.ListarTodos();



            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var datosfiltrados = listaRacionSolicitada
                .Select(c => new
                {
                    Id = c.id_racion_solicitada,
                    Convenio = c.convenio,
                    Fecha_solicitada = c.fecha_solicitada,
                    Detalles = c.detalles,
                    Fecha_carga = c.fecha_carga

                })
                .ToList();

            dtgRacionesSolicitadas.DataSource = datosfiltrados;

            if (listaRacionSolicitada.Count == 0)
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


    }
}
