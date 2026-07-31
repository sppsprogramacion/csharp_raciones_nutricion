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
    public partial class FormAnexoObservaciones : Form
    {
        private ErrorProvider errorProvider = new ErrorProvider();

        string accion_global = "";
        int id_anexo_global = 0;

        public FormAnexoObservaciones(int idAnexo)
        {
            InitializeComponent();
            this.id_anexo_global = idAnexo;
        }

        private void FormAnexoObservaciones_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);

            this.CargarDataObservaciones();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.accion_global = "nuevo";
            this.LimpiarControles();
            this.HabilitarControles(true);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdObservacion.Text))
            {
                MessageBox.Show("Debe seleccionar una observacion para editar", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.accion_global = "editar";
            this.HabilitarControles(true);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.accion_global = "";
            this.LimpiarControles();
            this.HabilitarControles(false);
        }

        private void dtgObservaciones_KeyDown(object sender, KeyEventArgs e)
        {
            //AL PRESIONAR ENTER MOSTRAR
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (dtgObservaciones.SelectedRows.Count > 0)
                {
                    txtIdObservacion.Text = Convert.ToString(dtgObservaciones.CurrentRow.Cells["Id"].Value);
                    txtObservacion.Text = Convert.ToString(dtgObservaciones.CurrentRow.Cells["Observacion"].Value);
                    chkVigente.Checked = Convert.ToBoolean(dtgObservaciones.CurrentRow.Cells["Vigente"].Value.ToString());



                }//fin if
                else
                {
                    MessageBox.Show("Debe seleccionar un regisrto.");
                }
            }
        }

        private void dtgObservaciones_DoubleClick(object sender, EventArgs e)
        {
            if (dtgObservaciones.SelectedRows.Count > 0)
            {
                txtIdObservacion.Text = Convert.ToString(dtgObservaciones.CurrentRow.Cells["Id"].Value);
                txtObservacion.Text = Convert.ToString(dtgObservaciones.CurrentRow.Cells["Observacion"].Value);
                chkVigente.Checked = Convert.ToBoolean(dtgObservaciones.CurrentRow.Cells["Vigente"].Value.ToString());



            }//fin if
            else
            {
                MessageBox.Show("Debe seleccionar un regisrto.");
            }
        }


        //METODO PARA OBTENER LA LISTA OBSERVACIONES
        private void CargarDataObservaciones()
        {
            var nObservaciones = new NAnexoObservacion();

            var (listaObservaciones, error) = nObservaciones.ListarTodosXIdAnexo(this.id_anexo_global);



            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var datosfiltrados = listaObservaciones
                .Select(c => new
                {
                    Id = c.id_anexo_observacion,
                    Observacion = c.observacion,
                    Vigente = c.vigente

                })
                .ToList();

            dtgObservaciones.DataSource = datosfiltrados;

            if (listaObservaciones.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                dtgObservaciones.Columns[0].Width = 80;
                dtgObservaciones.Columns[1].Width = 600;
                dtgObservaciones.Columns[2].Width = 50;
            }
        }
        //FIN METODO PARA OBTENER LA LISTA OBSERVACIONES ELABORADA..............................................


        //HABILITAR CONTROLES
        private void HabilitarControles(bool valor)
        {
            txtObservacion.Enabled = valor;
            dtgObservaciones.Enabled = !valor;
            if (accion_global == "nuevo")
            {
                chkVigente.Enabled = !valor;
            }
            else
            {
                chkVigente.Enabled = valor;
            }

            btnNuevo.Enabled = !valor;
            btnEditar.Enabled = !valor;
            btnGuardar.Enabled = valor;
            btnCancelar.Enabled = valor;
        }
        //FIN HABILITAR CONTROLES.......................................

        //LIMPIAR CONTROLES
        private void LimpiarControles()
        {
            txtIdObservacion.Text = string.Empty;
            txtObservacion.Text = string.Empty;
            chkVigente.Checked = false;

        }//FIN LIMPIAR CONTROLES..........................................

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //NUEVO
            if (this.accion_global == "nuevo")
            {
                //limpiar errores de provider
                errorProvider.Clear();
                bool tieneErrores = false;
                

                if (string.IsNullOrEmpty(txtObservacion.Text))
                {
                    errorProvider.SetError(txtObservacion, "Debe completar el campo OBSERVACION");
                    tieneErrores = true;
                    
                }

                if (txtIdObservacion.Text.Length > 500)
                {
                    errorProvider.SetError(txtObservacion, "Debe tener maximo 500 caracteres.");
                    tieneErrores = true;
                }


                if (tieneErrores)
                {
                    MessageBox.Show("Complete correctamente los campos marcados", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                try
                {
                    var nObservaciones = new NAnexoObservacion();

                    var observacion = new DAnexoObservacion
                    {
                        observacion = txtObservacion.Text,
                        anexo_id = this.id_anexo_global
                    };

                    nObservaciones.CrearObservacion(observacion);
                    MessageBox.Show("Creado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.CargarDataObservaciones();
                    
                    this.LimpiarControles();
                    this.accion_global = "";
                    this.HabilitarControles(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //FIN NUEVO

            //EDITAR
            if (this.accion_global == "editar")
            {
                //limpiar errores de provider
                errorProvider.Clear();
                bool tieneErrores = false;


                if (string.IsNullOrEmpty(txtObservacion.Text))
                {
                    errorProvider.SetError(txtObservacion, "Debe completar el campo OBSERVACION");
                    tieneErrores = true;

                }

                if (txtIdObservacion.Text.Length > 500)
                {
                    errorProvider.SetError(txtObservacion, "Debe tener maximo 500 caracteres.");
                    tieneErrores = true;
                }


                if (tieneErrores)
                {
                    MessageBox.Show("Complete correctamente los campos marcados", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var nAnexoObservaciones = new NAnexoObservacion();

                    
                    var anexoObservacion = new DAnexoObservacion
                    {
                        id_anexo_observacion = Convert.ToInt32(txtIdObservacion.Text),
                        observacion = txtObservacion.Text,
                        vigente = chkVigente.Checked

                    };

                    nAnexoObservaciones.EditarObservacion(anexoObservacion);
                    MessageBox.Show("Editado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.CargarDataObservaciones();
                    

                    

                    this.LimpiarControles();
                    this.accion_global = "";
                    this.HabilitarControles(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //FIN EDITAR
        }
    }
}
