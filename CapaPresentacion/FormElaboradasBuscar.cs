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
    public partial class FormElaboradasBuscar : Form
    {
        public string IdRacionElaborada { get; private set; }
        public string FechaElaborada { get; private set; }
        public string Convenio { get; private set; }
        public string Detalles { get; private set; }
        public string FechaCarga { get; private set; }

        public FormElaboradasBuscar()
        {
            InitializeComponent();
        }

        private void FormElaboradasBuscar_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);

            this.CargarDataElaboradas();
        }

        //METODO PARA OBTENER LA LISTA DE ELABORADAS
        private void CargarDataElaboradas()
        {
            var nRacionElaborada = new NRacionElaborada();

            var (listaRacionElaborada, error) = nRacionElaborada.ListarTodos();


            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var datosfiltrados = listaRacionElaborada
                .Select(c => new
                {
                    Id = c.id_racion_elaborada,
                    Convenio = c.convenio,
                    FechaElaborada = c.fecha_elaborada,
                    Detalles = c.detalles,
                    FechaCarga = c.fecha_carga

                })
                .ToList();

            dtgRacionesElaboradas.DataSource = datosfiltrados;

            if (listaRacionElaborada.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                dtgRacionesElaboradas.Columns[3].Width = 180;
            }
        }

        private void dtgRacionesElaboradas_KeyDown(object sender, KeyEventArgs e)
        {
            //AL PRESIONAR ENTER MOSTRAR
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (dtgRacionesElaboradas.SelectedRows.Count > 0)
                {
                    IdRacionElaborada = Convert.ToString(this.dtgRacionesElaboradas.CurrentRow.Cells["Id"].Value);
                    Convenio = Convert.ToString(this.dtgRacionesElaboradas.CurrentRow.Cells["Convenio"].Value);
                    FechaElaborada = Convert.ToString(this.dtgRacionesElaboradas.CurrentRow.Cells["FechaElaborada"].Value);
                    Detalles = Convert.ToString(this.dtgRacionesElaboradas.CurrentRow.Cells["Detalles"].Value);
                    FechaCarga = Convert.ToString(this.dtgRacionesElaboradas.CurrentRow.Cells["FechaCarga"].Value);

                    this.DialogResult = DialogResult.OK; // Para indicar que cerró bien
                    this.Close();

                }//fin if
                else
                {
                    MessageBox.Show("Debe seleccionar un registro.");
                }
            }
        }
        //FIN METODO PARA OBTENER LA LISTA DE ELABORADAS..............................................
    }
}
