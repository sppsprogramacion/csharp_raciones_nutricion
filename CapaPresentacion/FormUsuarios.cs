using CapaDatos;
using CapaNegocio;
using CapaPresentacion.FuncionesGenerales;
using CapaPresentacion.Validaciones.Usuarios.Datos;
using CapaPresentacion.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion.Validaciones.Usuarios.ValidacionesUsuarios;

namespace CapaPresentacion
{
    public partial class FormUsuarios : Form
    {
        private ErrorProvider errorProvider = new ErrorProvider();
        private string accion = "ninguna";
        public FormUsuarios()
        {
            InitializeComponent();
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);

            //cargar roles
            var nRol = new NRol();
            cmbRol.ValueMember = "id_rol";
            cmbRol.DisplayMember = "rol";

            var (listaRoles, error) = nRol.ListarTodos();

            if (error != null)
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmbRol.DataSource = listaRoles;
            //fin cargar roles

            this.CargarDataGridUsuarios();

            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.LimpiarControles();
            this.HabilitarControles(true);

            this.accion = "nuevo";
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtIdUsuario.Text == string.Empty || txtIdUsuario.Text == null)
            {
                MessageBox.Show("Debe seleccionar un usuario", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.HabilitarControles(true);
            txtPassword.Enabled = false;
            this.accion = "editarDatos";
        }

        private void btnEditarContrasenia_Click(object sender, EventArgs e)
        {
            if (txtIdUsuario.Text == string.Empty || txtIdUsuario.Text == null)
            {
                MessageBox.Show("Debe seleccionar un usuario", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.accion = "editarContrasenia";
            txtPassword.Enabled = true;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnEditarContrasenia.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void btnDeshabilitarUsuario_Click(object sender, EventArgs e)
        {
            if(txtIdUsuario.Text == string.Empty || txtIdUsuario.Text == null)
            {
                MessageBox.Show("Debe seleccionar un usuario", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //limpiar errores de provider
            errorProvider.Clear();

            //NUEVO USUARIO
            if (this.accion == "nuevo")
            {
                var datosFormulario = new UsuarioDatos
                {
                    txtApellido = txtApellido.Text,
                    txtNombre = txtNombre.Text,
                    txtDNI = txtDNI.Text,
                    txtLegajo = txtLegajo.Text,
                    cmbRol = cmbRol.SelectedValue?.ToString() ?? string.Empty,
                    txtPassword = txtPassword.Text,
                };

                var validator = new UsuarioNuevoValidator();
                var result = validator.Validate(datosFormulario);

                if (!result.IsValid)
                {
                    MessageBox.Show("Complete correctamente los campos del formulario", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    foreach (var failure in result.Errors)
                    {

                        Control control = Controls.Find(failure.PropertyName, true)[0];
                        errorProvider.SetError(control, failure.ErrorMessage);
                    }
                    return;
                }

                try
                {
                    var nUsuario = new NUsuario();

                    var usuario = new DUsuario
                    {
                        apellido = txtApellido.Text,
                        nombre = txtNombre.Text,
                        dni = Convert.ToInt32(txtDNI.Text),
                        legajo = Convert.ToInt32(txtLegajo.Text),
                        rol_id = cmbRol.SelectedValue?.ToString() ?? string.Empty,
                        is_activo = chkEstado.Checked,
                        contrasenia = txtPassword.Text,
                    };

                    nUsuario.CrearUsuario(usuario);
                    MessageBox.Show("Creado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.CargarDataGridUsuarios();

                    this.LimpiarControles();
                    this.HabilitarControles(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            //FIN NUEVO USUARIO

            //EDITAR USUARIO
            if (this.accion == "editarDatos")
            {
                var datosFormulario = new UsuarioDatos
                {
                    
                    txtApellido = txtApellido.Text,
                    txtNombre = txtNombre.Text,
                    txtDNI = txtDNI.Text,
                    txtLegajo = txtLegajo.Text,
                    cmbRol = cmbRol.SelectedValue?.ToString() ?? string.Empty,
                };

                var validator = new UsuarioEditarValidator();
                var result = validator.Validate(datosFormulario);

                if (!result.IsValid)
                {
                    MessageBox.Show("Complete correctamente los campos del formulario", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    foreach (var failure in result.Errors)
                    {

                        Control control = Controls.Find(failure.PropertyName, true)[0];
                        errorProvider.SetError(control, failure.ErrorMessage);
                    }
                    return;
                }

                try
                {
                    var nUsuario = new NUsuario();

                    var usuario = new DUsuario
                    {
                        id_usuario = Convert.ToInt32(txtIdUsuario.Text),
                        apellido = txtApellido.Text,
                        nombre = txtNombre.Text,
                        dni = Convert.ToInt32(txtDNI.Text),
                        legajo = Convert.ToInt32(txtLegajo.Text),
                        rol_id = cmbRol.SelectedValue?.ToString() ?? string.Empty,
                        is_activo = chkEstado.Checked,
                    };

                    nUsuario.EditarUsuario(usuario);
                    MessageBox.Show("Editado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.CargarDataGridUsuarios();

                    this.LimpiarControles();
                    this.HabilitarControles(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //FIN EDITAR USUARIO

            //EDITAR CONTRASEÑA
            if (this.accion == "editarContrasenia")
            {
                var datosFormulario = new UsuarioDatos
                {

                    txtPassword = txtPassword.Text
                };

                var validator = new UsuarioEditarContraseniaValidator();
                var result = validator.Validate(datosFormulario);

                if (!result.IsValid)
                {
                    MessageBox.Show("Complete correctamente los campos del formulario", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    foreach (var failure in result.Errors)
                    {

                        Control control = Controls.Find(failure.PropertyName, true)[0];
                        errorProvider.SetError(control, failure.ErrorMessage);
                    }
                    return;
                }

                try
                {
                    var nUsuario = new NUsuario();

                    var usuario = new DUsuario
                    {
                        id_usuario = Convert.ToInt32(txtIdUsuario.Text),
                        contrasenia = txtPassword.Text,
                        
                    };

                    nUsuario.EditarContrasenia(usuario);
                    MessageBox.Show("Editado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.CargarDataGridUsuarios();

                    this.LimpiarControles();
                    this.HabilitarControles(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //FIN EDITAR CONTRASEÑA
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //limpiar errores de provider
            errorProvider.Clear();

            this.LimpiarControles();
            this.HabilitarControles(false);
        }

        private void dtgUsuarios_KeyDown(object sender, KeyEventArgs e)
        {
            //AL PRESIONAR ENTER MOSTRAR
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;


                if (dtgUsuarios.SelectedRows.Count > 0)
                {
                    int idUsuario;
                    idUsuario = Convert.ToInt32(dtgUsuarios.CurrentRow.Cells["Id"].Value.ToString());

                    if (idUsuario > 0)
                    {
                        txtIdUsuario.Text = idUsuario.ToString();
                        txtApellido.Text = dtgUsuarios.CurrentRow.Cells["Apellido"].Value.ToString();
                        txtNombre.Text = dtgUsuarios.CurrentRow.Cells["Nombre"].Value.ToString();
                        txtDNI.Text = dtgUsuarios.CurrentRow.Cells["Dni"].Value.ToString();
                        txtLegajo.Text = dtgUsuarios.CurrentRow.Cells["Legajo"].Value.ToString();
                        cmbRol.SelectedValue = dtgUsuarios.CurrentRow.Cells["Rol"].Value.ToString();
                        chkEstado.Checked = Convert.ToBoolean(dtgUsuarios.CurrentRow.Cells["Activo"].Value.ToString());


                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un usuario.", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        //HABILITAR CONTROLES
        private void HabilitarControles(bool valor)
        {
            txtApellido.Enabled = valor;
            txtNombre.Enabled = valor;
            txtDNI.Enabled = valor;
            txtLegajo.Enabled = valor;
            txtPassword.Enabled = valor;
            cmbRol.Enabled = valor;
            chkEstado.Enabled = valor;
            dtgUsuarios.Enabled = !valor; 

            btnNuevo.Enabled = !valor;
            btnEditar.Enabled = !valor;
            btnEditarContrasenia.Enabled = !valor;
            btnGuardar.Enabled = valor;
            btnCancelar.Enabled = valor;
        }
        //FIN HABILITAR CONTROLES

        //LIMPIAR CONTROLES
        private void LimpiarControles()
        {
            txtIdUsuario.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDNI.Text = string.Empty;
            txtLegajo.Text = string.Empty;
            txtPassword.Text = string.Empty;
            chkEstado.Checked = false;

        }
        //FIN LIMPIAR CONTROLES

        //METODO PARA OBTENER LA LISTA DE USUARIOS
        private void CargarDataGridUsuarios()
        {
            var nUsuario = new NUsuario();
            
            var (listaUsuarios, error) = nUsuario.ListarTodos();

            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }           
           
            var datosfiltrados = listaUsuarios
                .Select(c => new
                {
                    Id = c.id_usuario,
                    Apellido = c.apellido,
                    Nombre = c.nombre,
                    Dni = c.dni,
                    Legajo = c.legajo,
                    Rol = c.rol.rol,
                    Activo = c.is_activo,
                    
                })
                .ToList();

            dtgUsuarios.DataSource = datosfiltrados;

            if (listaUsuarios.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }                        
        }
                
        //FIN METODO PARA OBTENER LA LISTA DE USUARIOS..............................................

    }
}
