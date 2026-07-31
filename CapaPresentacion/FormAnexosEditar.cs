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
    public partial class FormAnexosEditar : Form
    {
        private ErrorProvider errorProvider = new ErrorProvider();

        public int IdAnexoGlobal { get; private set; }
        public DateTime FechaInicioGlobal { get; private set; }
        public string DescripcionGlobal { get; private set; }

        public FormAnexosEditar(int idAnexo, DateTime fechaInicio, string descripcion)
        {
            InitializeComponent();
            IdAnexoGlobal = idAnexo;
            txtIdAnexo.Text = IdAnexoGlobal.ToString();
            dtpFechaInicio.Value = fechaInicio;
            txtDescripcion.Text = descripcion;
        }


        private void FormAnexosEditar_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //limpiar errores de provider
            errorProvider.Clear();
            bool tieneErrores = false;
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {

                errorProvider.SetError(txtDescripcion, "Debe completar el campo DESCRIPCION");
                tieneErrores = true;
            }

            if (txtDescripcion.Text.Length > 1000)
            {
                errorProvider.SetError(txtDescripcion, "Debe tener maximo 1000 caracteres.");
                tieneErrores = true;
            }

            if (tieneErrores)
            {
                MessageBox.Show("Complete correctamente los campos marcados", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var nAnexo = new NAnexo();


                var anexo = new DAnexo()
                {
                    id_anexo = Convert.ToInt32(txtIdAnexo.Text),
                    fecha_inicio = dtpFechaInicio.Value.Date,
                    descripcion = txtDescripcion.Text

                };

                nAnexo.Editar(anexo);
                MessageBox.Show("Editado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FechaInicioGlobal = dtpFechaInicio.Value.Date;
                DescripcionGlobal = txtDescripcion.Text;

                this.DialogResult = DialogResult.OK; // Para indicar que cerró bien
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
