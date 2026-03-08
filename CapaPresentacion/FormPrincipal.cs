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
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FormUsuarios formUsuarios = new FormUsuarios(); 
            formUsuarios.ShowDialog();
        }

        private void btnCargarSolicitadas_Click(object sender, EventArgs e)
        {
            FormCargaSolicitadas formSolicitadas = new FormCargaSolicitadas();
            formSolicitadas.ShowDialog();
        }
               

        private void btnCargarElaboradas_Click(object sender, EventArgs e)
        {
            FormCargarElaboradas form = new FormCargarElaboradas();
            form.ShowDialog();
        }

        private void btnVerRacionesElaboradasCargadas_Click(object sender, EventArgs e)
        {

            FormRacionesCargadas form = new FormRacionesCargadas();
            form.ShowDialog();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);
        }

        private void btnVerRacionesSolicitadasCargadas_Click(object sender, EventArgs e)
        {
            FormRacionesSolicitadas form = new FormRacionesSolicitadas();
            form.ShowDialog();
        }
    }
}
