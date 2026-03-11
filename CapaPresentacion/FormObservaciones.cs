using CapaDatos;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CapaPresentacion
{
    public partial class FormObservaciones : Form
    {
        string accion_global = "";
        string tipo_observacion_global = "";
        int id_observacion_global = 0;
        public FormObservaciones(string tipo_observacion, int id_observacion)
        {
            InitializeComponent();
            this.tipo_observacion_global =tipo_observacion;
            this.id_observacion_global=id_observacion;
        }

        private void FormObservaciones_Load(object sender, EventArgs e)
        {
            if (this.tipo_observacion_global == "observacion_solicitada")
            {
                lblDatos.Text = "OBSERVACIONES DE RACION SOLICITADA";
                this.CargarDataObservacionesSolicitadas();
                    
            }
            if (this.tipo_observacion_global == "observacion_elaborada")
            {
                lblDatos.Text = "OBSERVACIONES DE RACION ELABORADA";
                this.CargarDataObservacionesElaborada();

            }
            if (this.tipo_observacion_global == "observacion_general")
            {
                lblDatos.Text = "OBSERVACIONES GENERALES";
                this.CargarDataObservacionesGeneral();

            }
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //NUEVO
            if (this.accion_global == "nuevo")
            {
                if (string.IsNullOrEmpty(txtObservacion.Text))
                {
                    MessageBox.Show("Debe detallar la observacion", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtIdObservacion.Text.Length > 500)
                {
                    MessageBox.Show("La observacion no puede tener mas de 500 caracteres", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var nObservaciones = new NObservaciones();

                    

                    if (tipo_observacion_global == "observacion_solicitada")
                    {
                        var observacionSolicitada = new DObservacionSolicitada
                        {
                            observacion = txtObservacion.Text,
                            racion_solicitada_id = this.id_observacion_global
                        };

                        nObservaciones.CrearObservacionSolicitada(observacionSolicitada);
                        MessageBox.Show("Creado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.CargarDataObservacionesSolicitadas();
                    }

                    if (tipo_observacion_global == "observacion_elaborada")
                    {
                        var observacionElaborada = new DObservacionElaborada
                        {
                            observacion = txtObservacion.Text,
                            racion_elaborada_id = this.id_observacion_global
                        };

                        nObservaciones.CrearObservacionElaborada(observacionElaborada);
                        MessageBox.Show("Creado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.CargarDataObservacionesElaborada();
                    }

                    if (tipo_observacion_global == "observacion_general")
                    {
                        var observacionGeneral = new DObservacionGeneral
                        {
                            observacion = txtObservacion.Text,
                        };
                        nObservaciones.CrearObservacionGeneral(observacionGeneral);
                        MessageBox.Show("Creado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.CargarDataObservacionesGeneral();
                    }
                                        

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
                if (string.IsNullOrEmpty(txtObservacion.Text))
                {
                    MessageBox.Show("Debe detallar la observacion", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtIdObservacion.Text.Length > 500)
                {
                    MessageBox.Show("La observacion no puede tener mas de 500 caracteres", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var nObservaciones = new NObservaciones();
                    

                    if (tipo_observacion_global == "observacion_solicitada")
                    {
                        var observacionSolicitada = new DObservacionSolicitada
                        {
                            id_observacion_solicitada = Convert.ToInt32(txtIdObservacion.Text),
                            observacion = txtObservacion.Text,
                            vigente = chkVigente.Checked

                        };

                        nObservaciones.EditarObservacionSolicitada(observacionSolicitada);
                        MessageBox.Show("Editado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.CargarDataObservacionesSolicitadas();
                    }

                    if (tipo_observacion_global == "observacion_elaborada")
                    {
                        var observacionElaborada = new DObservacionElaborada
                        {
                            id_observacion_elaborada = Convert.ToInt32(txtIdObservacion.Text),
                            observacion = txtObservacion.Text,
                            vigente = chkVigente.Checked

                        };

                        nObservaciones.EditarObservacionElaborada(observacionElaborada);
                        MessageBox.Show("Editado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.CargarDataObservacionesElaborada();
                    }

                    if (tipo_observacion_global == "observacion_general")
                    {
                        var observacionGeneral = new DObservacionGeneral
                        {
                            id_observacion_general = Convert.ToInt32(txtIdObservacion.Text),
                            observacion = txtObservacion.Text,
                            vigente = chkVigente.Checked

                        };
                        nObservaciones.EditarObservacionGeneral(observacionGeneral);
                        MessageBox.Show("Editado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.CargarDataObservacionesGeneral();
                    }
                                        

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

        //METODO PARA OBTENER LA LISTA OBSERVACIONES SOLICITADA
        private void CargarDataObservacionesSolicitadas()
        {
            var nObservacionesSolicitadas = new NObservaciones();

            var (listaObservacionesSolicitada, error) = nObservacionesSolicitadas.ListarTodosXIdSolicitada(this.id_observacion_global);



            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var datosfiltrados = listaObservacionesSolicitada
                .Select(c => new
                {
                    Id = c.id_observacion_solicitada,
                    Observacion = c.observacion,
                    Vigente = c.vigente

                })
                .ToList();

            dtgObservaciones.DataSource = datosfiltrados;

            if (listaObservacionesSolicitada.Count == 0)
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
        //FIN METODO PARA OBTENER LA LISTA OBSERVACIONES SOLICITADA..............................................


        //METODO PARA OBTENER LA LISTA OBSERVACIONES ELABORADA
        private void CargarDataObservacionesElaborada()
        {
            var nObservacionesElaborada = new NObservaciones();

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


        //METODO PARA OBTENER LA LISTA OBSERVACIONES GENERAL
        private void CargarDataObservacionesGeneral()
        {
            var nObservacionesGeneral = new NObservaciones();

            var (listaObservacionesGeneral, error) = nObservacionesGeneral.ListarTodosGeneral();



            if (error != null)
            {
                MessageBox.Show(error, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var datosfiltrados = listaObservacionesGeneral
                .Select(c => new
                {
                    Id = c.id_observacion_general,
                    Observacion = c.observacion,
                    Vigente = c.vigente

                })
                .ToList();

            dtgObservaciones.DataSource = datosfiltrados;

            if (listaObservacionesGeneral.Count == 0)
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
            if(accion_global == "nuevo")
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
