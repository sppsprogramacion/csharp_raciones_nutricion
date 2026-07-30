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
    public partial class FormAnexosBuscar : Form
    {
        public string IdAnexo { get; private set; }
        public string FechaInicio { get; private set; }
        public string Descripcion { get; private set; }
        public string FechaCarga { get; private set; }


        public FormAnexosBuscar()
        {
            InitializeComponent();
        }

        private void FormAnexosBuscar_Load(object sender, EventArgs e)
        {
            FormularioAyudas.AjustarFormulario(this);

            this.CargarDataAnexos();
        }


        private void dtgAnexos_KeyDown(object sender, KeyEventArgs e)
        {
            //AL PRESIONAR ENTER MOSTRAR
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (dtgAnexos.SelectedRows.Count > 0)
                {
                    IdAnexo = Convert.ToString(this.dtgAnexos.CurrentRow.Cells["Id"].Value);
                    FechaInicio = Convert.ToString(this.dtgAnexos.CurrentRow.Cells["FechaInicio"].Value);
                    Descripcion = Convert.ToString(this.dtgAnexos.CurrentRow.Cells["Descripcion"].Value);
                    FechaCarga = Convert.ToString(this.dtgAnexos.CurrentRow.Cells["FechaCarga"].Value);

                    this.DialogResult = DialogResult.OK; // Para indicar que cerró bien
                    this.Close();

                }//fin if
                else
                {
                    MessageBox.Show("Debe seleccionar un registro.");
                }

            }
        }

        private void dtgAnexos_DoubleClick(object sender, EventArgs e)
        {
            if (dtgAnexos.SelectedRows.Count > 0)
            {
                IdAnexo = Convert.ToString(this.dtgAnexos.CurrentRow.Cells["Id"].Value);
                FechaInicio = Convert.ToString(this.dtgAnexos.CurrentRow.Cells["FechaInicio"].Value);
                Descripcion = Convert.ToString(this.dtgAnexos.CurrentRow.Cells["Descripcion"].Value);
                FechaCarga = Convert.ToString(this.dtgAnexos.CurrentRow.Cells["FechaCarga"].Value);

                this.DialogResult = DialogResult.OK; // Para indicar que cerró bien
                this.Close();

            }//fin if
            else
            {
                MessageBox.Show("Debe seleccionar un registro.");
            }
        }


        //METODO PARA OBTENER LA LISTA DE ANEXOS
        private void CargarDataAnexos()
        {
            var nAnexos = new NAnexo();

            var (listaAnexosResponse, error) = nAnexos.ListarTodos();



            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var datosfiltrados = listaAnexosResponse
                .Select(c => new
                {
                    Id = c.id_anexo,
                    FechaInicio = c.fecha_inicio,
                    Descripcion = c.descripcion,
                    FechaCarga = c.fecha_carga

                })
                .ToList();

            dtgAnexos.DataSource = datosfiltrados;

            if (listaAnexosResponse.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                dtgAnexos.Columns[2].Width = 180;
            }
        }        
        //FIN METODO PARA OBTENER LA LISTA DE ANEXOS..............................................
    }
}
