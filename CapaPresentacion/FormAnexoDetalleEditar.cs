using CapaDatos;
using CapaNegocio;
using CapaPresentacion.FuncionesGenerales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormAnexoDetalleEditar : Form
    {

        int IdAnexoDetalleGlobal;

        public FormAnexoDetalleEditar( int idAnexoDetalle)
        {
            InitializeComponent();

            IdAnexoDetalleGlobal = idAnexoDetalle;
        }

        private void FormAnexoDetalleEditar_Load(object sender, EventArgs e)
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


            //Buscar AnexoDetalle
            NAnexoDetalles nAnexoDetalles = new NAnexoDetalles();

            (DAnexoDetalle anexoDetalle, string errorResponseDetalle) = nAnexoDetalles.BuscarXIdDetalle(IdAnexoDetalleGlobal);

            if (errorResponseDetalle != null)
            {
                MessageBox.Show(errorResponseDetalle, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtIdAnexoDetalle.Text = anexoDetalle.id_anexo_detalle.ToString();
            cmbMenus.SelectedValue = anexoDetalle.anexo_menu_id;
            txtDetalle.Text = anexoDetalle.detalle;
            txtCantidad.Text = anexoDetalle.cantidad.ToString();
            txtFactor.Text = anexoDetalle.factor.ToString();
        }

        private void btnGuardarCantidad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdAnexoDetalle.Text))
            {
                MessageBox.Show("Debe seleccionar un registro", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                
                var nAnexoDetalles = new NAnexoDetalles();

                var detalle = new DAnexoDetalle
                {
                    id_anexo_detalle = Convert.ToInt32(txtIdAnexoDetalle.Text),
                    anexo_menu_id = Convert.ToInt32(cmbMenus.SelectedValue.ToString()),
                    detalle = txtDetalle.Text,
                    cantidad = Convert.ToInt32(txtCantidad.Text),
                    factor = Convert.ToDecimal(txtFactor.Text),
                    usuario_id = 1
                };

                nAnexoDetalles.EditarDetalle(detalle);
                MessageBox.Show("Editado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // Para indicar que cerró bien
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelarGuardarCantidad_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
