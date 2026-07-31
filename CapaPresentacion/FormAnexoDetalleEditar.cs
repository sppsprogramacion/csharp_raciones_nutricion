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




        }
    }
}
