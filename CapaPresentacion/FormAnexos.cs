using CapaDatos;
using CapaNegocio;
using CapaPresentacion.FuncionesGenerales;
using CapaPresentacion.Validaciones.Anexos.Datos;
using CapaPresentacion.Validaciones.Anexos.Validaciones;
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
    public partial class FormAnexos : Form
    {
        private ErrorProvider errorProvider = new ErrorProvider();

        public FormAnexos()
        {
            InitializeComponent();
        }

        private void FormAnexos_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);

            //cargar menus
            var nMenusAnexo = new NAnexoMenu(); 
            cmbMenus.ValueMember = "id_anexo_menu";
            cmbMenus.DisplayMember = "menu";

            var (listaMenus, error) = nMenusAnexo.ListarTodos();

            if (error != null)
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmbMenus.DataSource = listaMenus;
            //fin cargar menus
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (FormAnexosNuevo formulario = new FormAnexosNuevo())
            {
                // Aquí se abre el FormularioB
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    // Recién después de cerrar FormularioB, puedo leer el dato
                    txtIdAnexo.Text = formulario.IdAnexo;
                    dtpFechaInicio.Text = formulario.FechaInicio;
                    txtDescripcion.Text = formulario.Descripcion;
                    txtFechaCarga.Text = formulario.FechaCarga;
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (FormAnexosBuscar formulario = new FormAnexosBuscar())
            {
                // Aquí se abre el FormularioB
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    // Recién después de cerrar FormularioB, puedo leer el dato
                    txtIdAnexo.Text = formulario.IdAnexo;
                    dtpFechaInicio.Text = formulario.FechaInicio;
                    txtDescripcion.Text = formulario.Descripcion;
                    txtFechaCarga.Text = formulario.FechaCarga;
                }
            }
        }

        private void btnGuardarCantidad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdAnexo.Text))
            {
                MessageBox.Show("No hay un anexo seleccionado", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var nAnexoDetalles = new NAnexoDetalles();
                //limpiar errores de provider
                errorProvider.Clear();

                //validacion de formulario
                var datosFormulario = new AnexoDetalleDatos
                {
                    cmbMenus = cmbMenus.SelectedValue?.ToString() ?? string.Empty,
                    txtDetalle = txtDetalle.Text,
                    txtCantidad = txtCantidad.Text,
                    txtFactor = txtFactor.Text
                };

                var validator = new CrearAnexoDetalleValidation();
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
                //fin validar formulario

                var anexoDetalle = new DAnexoDetalle
                {
                    anexo_id = Convert.ToInt32(txtIdAnexo.Text),
                    anexo_menu_id = Convert.ToInt32(cmbMenus.SelectedValue.ToString()),
                    detalle = txtDetalle.Text,
                    cantidad = Convert.ToInt32(txtCantidad.Text),
                    factor = Convert.ToDecimal(txtFactor.Text),
                    usuario_id = 1
                };

                nAnexoDetalles.InsertarUnDetalle(anexoDetalle);
                MessageBox.Show("Creado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.CargarDataAnexosDetalles();

                //this.LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelarGuardarCantidad_Click(object sender, EventArgs e)
        {
            this.LimpiarControles();
        }


        private void btnActualizarAnexo_Click(object sender, EventArgs e)
        {
            this.CargarDataAnexosDetalles();
        }



        //METODO PARA OBTENER LA LISTA DE ANEXOS
        private void CargarDataAnexosDetalles()
        {
            if (string.IsNullOrEmpty(txtIdAnexo.Text))
            {
                MessageBox.Show("No hay un anexo seleccionado", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nAnexosDetalles = new NAnexoDetalles();

            var (listaDetallesResponse, error) = nAnexosDetalles.ListarXIdAnexo(Convert.ToInt32(txtIdAnexo.Text));


            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var datosfiltrados = listaDetallesResponse
                .Select(c => new
                {
                    Id = c.id_anexo_detalle,
                    Menu = c.anexo_menu.menu,
                    Detalle = c.detalle,
                    Cantidad = c.cantidad,
                    Factor = c.factor,
                    Racion = c.cantidad * c.factor

                })
                .ToList();

            dtgAnexoDetalles.DataSource = datosfiltrados;

            if (listaDetallesResponse.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                dtgAnexoDetalles.Columns[1].Width = 180;
                dtgAnexoDetalles.Columns[2].Width = 150;
            }
        }

        
        //FIN METODO PARA OBTENER LA LISTA DE ANEXOS..............................................

        //LIMPIAR CONTROLES
        private void LimpiarControles()
        {
            this.errorProvider.Clear();
            txtDetalle.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtFactor.Text = string.Empty;


        }

        private void btnEliminarRegistrosCargados_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdAnexo.Text))
            {
                MessageBox.Show("Debe seleccionar un Anexo", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //advertencia al usuario
            DialogResult resultado = MessageBox.Show(
                "⚠️ Esta acción eliminará todos los resgistros cargados para este anexo.\n\n¿Desea continuar?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (resultado == DialogResult.No)
                return;


            try
            {
                var nAnexoDetalles = new NAnexoDetalles();

                nAnexoDetalles.EliminarDetalles(Convert.ToInt32(txtIdAnexo.Text));
                MessageBox.Show("Eliminado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.CargarDataAnexosDetalles();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //FIN LIMPIAR CONTROLES...................................................................

    }
}
