using CapaDatos;
using CapaNegocio;
using CapaPresentacion.FuncionesGenerales;
using CapaPresentacion.Validaciones.Usuarios.Datos;
using CapaPresentacion.Validaciones.Usuarios.ValidacionesUsuarios;
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
    public partial class FormSolicitadasNuevo : Form
    {
        public string IdRacionSolicitada { get; private set; }
        public string FechaSolicitada { get; private set; }
        public string Convenio { get; private set; }
        public string Detalles { get; private set; }
        public string FechaCarga { get; private set; }

        public FormSolicitadasNuevo()
        {
            InitializeComponent();
        }


        private void FormSolicitadasNuevo_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);
                        

            this.CargarDataSolicitadas();
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ////limpiar errores de provider
            //errorProvider.Clear();

            ////NUEVO USUARIO
            //if (this.accion == "nuevo")
            //{
            //    var datosFormulario = new UsuarioDatos
            //    {
            //        txtApellido = txtApellido.Text,
            //        txtNombre = txtNombre.Text,
            //        txtDNI = txtDNI.Text,
            //        txtLegajo = txtLegajo.Text,
            //        cmbRol = cmbRol.SelectedValue?.ToString() ?? string.Empty,
            //        txtPassword = txtPassword.Text,
            //    };

            //    var validator = new UsuarioNuevoValidator();
            //    var result = validator.Validate(datosFormulario);

            //    if (!result.IsValid)
            //    {
            //        MessageBox.Show("Complete correctamente los campos del formulario", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        foreach (var failure in result.Errors)
            //        {

            //            Control control = Controls.Find(failure.PropertyName, true)[0];
            //            errorProvider.SetError(control, failure.ErrorMessage);
            //        }
            //        return;
            //    }

            try
            {
                var nRacionSolicitada = new NRacionSolicitada();

                var racionSolicitada = new DRacionSolicitada
                {
                    fecha_solicitada = dtpFechaSolicitada.Value,
                    convenio = txtConvenio.Text,
                    detalles = txtDetalles.Text,
                };

                nRacionSolicitada.CrearRacionSolicitada(racionSolicitada);
                MessageBox.Show("Creado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.CargarDataSolicitadas();

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

        //LIMPIAR CONTROLES
        private void LimpiarControles()
        {
            txtConvenio.Text = string.Empty;
            dtpFechaSolicitada.Text = string.Empty;
            txtDetalles.Text = string.Empty;

        }
        //FIN LIMPIAR CONTROLES

        //METODO PARA OBTENER LA LISTA
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
        //FIN METODO PARA OBTENER LA LISTA..............................................


    }
}
