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

        }



        //METODO PARA OBTENER LA LISTA OBSERVACIONES GENERAL
        private void CargarDataObservaciones()
        {
            var nObservaciones = new ();

            var (listaObservacionesElaborada, error) = nObservacionesElaborada.ListarTodosXIdElaborada(this.id_observacion_global);



            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var datosfiltrados = listaObservacionesElaborada
                .Select(c => new
                {
                    Id = c.id_observacion_elaborada,
                    Observacion = c.observacion,
                    Vigente = c.vigente

                })
                .ToList();

            dtgObservaciones.DataSource = datosfiltrados;

            if (listaObservacionesElaborada.Count == 0)
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

       
    }
}
