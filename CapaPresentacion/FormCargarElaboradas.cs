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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace CapaPresentacion
{
    public partial class FormCargarElaboradas : Form
    {
        public FormCargarElaboradas()
        {
            InitializeComponent();
        }
              

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (FormElaboradasNuevo formulario = new FormElaboradasNuevo())
            {
                // Aquí se abre el FormularioB
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    // Recién después de cerrar FormularioB, puedo leer el dato
                    txtIdRacionElaborada.Text = formulario.IdRacionElaborada;
                    txtConvenio.Text = formulario.Convenio;
                    dtpFechaElaborada.Text = formulario.FechaElaborada;
                    txtDetalles.Text = formulario.Detalles;
                    txtFechaCarga.Text = formulario.FechaCarga;
                }
            }
        }

        private void FormCargarElaboradas_Load(object sender, EventArgs e)
        {
            //// Ajustar el tamaño del formulario            
            FormularioAyudas.AjustarFormulario(this);

            //cargar unidades
            var nUnidad = new NUnidad();
            cmbUnidades.ValueMember = "id_unidad";
            cmbUnidades.DisplayMember = "unidad";

            var (listaUnidades, error) = nUnidad.ListarTodos();

            if (error != null)
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmbUnidades.DataSource = listaUnidades;
            //fin cargar roles
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            // Carga datagrid SapMenus
            NSapMenu nSapMenu = new NSapMenu();
            int id_unidad = Convert.ToInt32(this.cmbUnidades.SelectedValue.ToString());

            (List<DSapMenu> listaSapMenu, string errorResponse) = nSapMenu.ListarxUnidad(id_unidad);

            if (errorResponse != null)
            {
                MessageBox.Show(errorResponse, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaSapMenu = listaSapMenu
                .OrderBy(s => s.tipo_menu.orden)
                .ToList();

            var datosfiltrados = listaSapMenu
                .Select(c => new
                {
                    IdSap = c.sap_id,
                    Sap = c.sap.sap,
                    IdUnidad = c.unidad_id,
                    Unidad = c.unidad.unidad,
                    IdTipoMenu = c.tipo_menu_id,
                    TipoMenu = c.tipo_menu.tipo_menu,
                })
                .ToList();

            dtgSapMenus.DataSource = null;
            dtgSapMenus.Columns.Clear();
            dtgSapMenus.Rows.Clear();
            dtgSapMenus.DataSource = datosfiltrados;
            dtgSapMenus.Columns["IdSap"].Visible = false;
            dtgSapMenus.Columns["IdUnidad"].Visible = false;
            dtgSapMenus.Columns["IdTipoMenu"].Visible = false;

            this.AgregarColumnasNumericas();

            if (listaSapMenu.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                dtgSapMenus.Columns[1].Width = 180;
                dtgSapMenus.Columns[3].Width = 150;
                dtgSapMenus.Columns[5].Width = 120;
                dtgSapMenus.Columns[6].Width = 45;
                dtgSapMenus.Columns[7].Width = 45;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            List<DRacionElaboradaDetalles> lista = new List<DRacionElaboradaDetalles>();

            if (string.IsNullOrEmpty(txtIdRacionElaborada.Text))
            {
                MessageBox.Show("No hay una Racion Elaborada selecionada", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow fila in dtgSapMenus.Rows)
            {
                if (fila.IsNewRow) continue;

                //validacion de cantidades
                foreach (DataGridViewRow filaAux in dtgSapMenus.Rows)
                {
                    if (filaAux.IsNewRow) continue;

                    if (string.IsNullOrEmpty(filaAux.Cells["Almuerzo"].Value.ToString()) ||   string.IsNullOrEmpty(filaAux.Cells["Cena"].Value.ToString()))
                    {
                        MessageBox.Show("Verifique que los campos de las cantidades no esten vacios", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                var entidad = new DRacionElaboradaDetalles
                {
                    racion_elaborada_id = Convert.ToInt32(txtIdRacionElaborada.Text),
                    sap_id = Convert.ToInt32(fila.Cells["IdSap"].Value.ToString()),
                    unidad_id = Convert.ToInt32(fila.Cells["IdUnidad"].Value.ToString()),
                    tipo_menu_id = Convert.ToInt32(fila.Cells["IdTipoMenu"].Value.ToString()),
                    almuerzo = Convert.ToInt32(fila.Cells["Almuerzo"].Value.ToString()),
                    cena = Convert.ToInt32(fila.Cells["Cena"].Value.ToString()),
                    usuario_id = 1
                    // fecha_carga NO se asigna
                };

                lista.Add(entidad);

            }

            if (!lista.Any())
            {
                MessageBox.Show("No hay datos para guardar", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var dao = new NRacionElaboradaDetalles();
                dao.InsertarDetalles(lista);

                MessageBox.Show("Datos guardados correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ActualizarUnidadesCantidades();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtIdRacionElaborada.Text))
            {
                MessageBox.Show("No hay una Racion Elaborada selecionada", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            FormElaboradasEditar form = new FormElaboradasEditar(Convert.ToInt32(txtIdRacionElaborada.Text), Convert.ToInt32(cmbUnidades.SelectedValue.ToString()), cmbUnidades.Text, dtpFechaElaborada.Text);
            form.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (FormElaboradasBuscar formulario = new FormElaboradasBuscar())
            {
                // Aquí se abre el FormularioB
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    // Recién después de cerrar FormularioB, puedo leer el dato
                    txtIdRacionElaborada.Text = formulario.IdRacionElaborada;
                    txtConvenio.Text = formulario.Convenio;
                    txtDetalles.Text = formulario.Detalles;
                    txtFechaCarga.Text = formulario.FechaCarga;
                    dtpFechaElaborada.Text = formulario.FechaElaborada;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Carga datagrid SapMenus
            NSapMenu nSapMenu = new NSapMenu();
            int id_unidad = Convert.ToInt32(this.cmbUnidades.SelectedValue.ToString());

            (List<DSapMenu> listaSapMenu, string errorResponse) = nSapMenu.ListarxUnidad(id_unidad);

            if (errorResponse != null)
            {
                MessageBox.Show(errorResponse, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaSapMenu = listaSapMenu
                .OrderBy(s => s.tipo_menu.orden)
                .ToList();

            var datosfiltrados = listaSapMenu
                .Select(c => new
                {
                    IdSap = c.sap_id,
                    Sap = c.sap.sap,
                    IdUnidad = c.unidad_id,
                    Unidad = c.unidad.unidad,
                    IdTipoMenu = c.tipo_menu_id,
                    TipoMenu = c.tipo_menu.tipo_menu,
                })
                .ToList();

            dtgSapMenus.DataSource = null;
            dtgSapMenus.Columns.Clear();
            dtgSapMenus.Rows.Clear();
            dtgSapMenus.DataSource = datosfiltrados;
            dtgSapMenus.Columns["IdSap"].Visible = false;
            dtgSapMenus.Columns["IdUnidad"].Visible = false;
            dtgSapMenus.Columns["IdTipoMenu"].Visible = false;

            this.AgregarColumnasNumericas();

            if (listaSapMenu.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                dtgSapMenus.Columns[1].Width = 180;
                dtgSapMenus.Columns[3].Width = 150;
                dtgSapMenus.Columns[5].Width = 120;
                dtgSapMenus.Columns[6].Width = 45;
                dtgSapMenus.Columns[7].Width = 45;
            }
        }

        private void cmbUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Carga datagrid SapMenus
            NSapMenu nSapMenu = new NSapMenu();
            int id_unidad = Convert.ToInt32(this.cmbUnidades.SelectedValue.ToString());

            (List<DSapMenu> listaSapMenu, string errorResponse) = nSapMenu.ListarxUnidad(id_unidad);

            if (errorResponse != null)
            {
                MessageBox.Show(errorResponse, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaSapMenu = listaSapMenu
                .OrderBy(s => s.tipo_menu.orden)
                .ToList();

            var datosfiltrados = listaSapMenu
                .Select(c => new
                {
                    IdSap = c.sap_id,
                    Sap = c.sap.sap,
                    IdUnidad = c.unidad_id,
                    Unidad = c.unidad.unidad,
                    IdTipoMenu = c.tipo_menu_id,
                    TipoMenu = c.tipo_menu.tipo_menu,
                })
                .ToList();

            dtgSapMenus.DataSource = null;
            dtgSapMenus.Columns.Clear();
            dtgSapMenus.Rows.Clear();
            dtgSapMenus.DataSource = datosfiltrados;
            dtgSapMenus.Columns["IdSap"].Visible = false;
            dtgSapMenus.Columns["IdUnidad"].Visible = false;
            dtgSapMenus.Columns["IdTipoMenu"].Visible = false;

            this.AgregarColumnasNumericas();

            if (listaSapMenu.Count == 0)
            {
                MessageBox.Show("No se encontraron registros", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                dtgSapMenus.Columns[1].Width = 180;
                dtgSapMenus.Columns[3].Width = 150;
                dtgSapMenus.Columns[5].Width = 120;
                dtgSapMenus.Columns[6].Width = 45;
                dtgSapMenus.Columns[7].Width = 45;
            }
        }


        private void btnActualizarUnidades_Click(object sender, EventArgs e)
        {
            this.ActualizarUnidadesCantidades();
        }

        private void btnVerCantidadesXSap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdRacionElaborada.Text))
            {
                MessageBox.Show("No hay una Racion Elaborada selecionada", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            FormCargarElaboradasCantidadesSap form = new FormCargarElaboradasCantidadesSap(Convert.ToInt32(txtIdRacionElaborada.Text));
            form.ShowDialog();
        }


        //AGREGAR COLUMNAS
        private void AgregarColumnasNumericas()
        {
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = $"Almuerzo";
            col2.HeaderText = $"A";
            col2.ValueType = typeof(int);
            col2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dtgSapMenus.Columns.Add(col2);

            
            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Name = $"Cena";
            col4.HeaderText = $"C";
            col4.ValueType = typeof(int);
            col4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dtgSapMenus.Columns.Add(col4);

            foreach (DataGridViewRow row in dtgSapMenus.Rows)
            {
                if (!row.IsNewRow)
                {
                    for (int i = 6; i < 8; i++)
                    {
                        row.Cells[i].Value = 0;
                    }
                }
            }
        }
        //FIN AGREGAR COLUMNAS...............................................

        //SOLO NUMERO EN DATAGRID
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        //FIN SOLO NUMERO EN DATAGRID....................


        private void dtgSapMenus_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dtgSapMenus.CurrentCell.ColumnIndex >= 7) // desde la 4ta columna
            {
                if (e.Control is TextBox txt)
                {
                    txt.KeyPress -= SoloNumeros_KeyPress;
                    txt.KeyPress += SoloNumeros_KeyPress;
                }
            }
        }


        //ACTUALIZAR UNIADES CANTIDADES
        private void ActualizarUnidadesCantidades()
        {
            if (string.IsNullOrEmpty(txtIdRacionElaborada.Text))
            {
                MessageBox.Show("No hay una Racion Elaborada selecionada", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idRacionElaborada = Convert.ToInt32(this.txtIdRacionElaborada.Text.ToString());


            //Listar Unidades
            NUnidad nUnidad = new NUnidad();
            (List<DUnidad> listaUnidades, string errorResponseUnidad) = nUnidad.ListarTodos();

            if (errorResponseUnidad != null)
            {
                MessageBox.Show(errorResponseUnidad, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaUnidades = listaUnidades
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar Unidades

            //Listar TipoMenu
            NTipoMenu nTipoMenu = new NTipoMenu();
            (List<DTipoMenu> listaTipoMenu, string errorResponseTipoMenu) = nTipoMenu.ListarTodos();

            if (errorResponseTipoMenu != null)
            {
                MessageBox.Show(errorResponseTipoMenu, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaTipoMenu = listaTipoMenu
                .OrderBy(s => s.orden)
                .ToList();
            //fin Listar TipoMenu

            //Listar Detalles elaboradas
            NRacionElaboradaDetalles nElaboradasDetalles = new NRacionElaboradaDetalles();

            (List<DRacionElaboradaDetalles> listaDetalles, string errorResponseDetalles) = nElaboradasDetalles.ListarXIdRacionElaborada(idRacionElaborada);

            if (errorResponseDetalles != null)
            {
                MessageBox.Show(errorResponseDetalles, "Nutricion: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listaDetalles = listaDetalles
                .OrderBy(s => s.tipo_menu.orden)
                .ToList();
            //fin Listar Detalles elaboradas

            //contar valores de menus en cada unidad
            List<DUnidadMenuCantidades> listaUnidadesCantidades = new List<DUnidadMenuCantidades>();

            List<DRacionElaboradaDetalles> listaFiltroDetallesXUnidad = new List<DRacionElaboradaDetalles>();
            foreach (DUnidad unidad in listaUnidades)
            {
                // 🔴 NUEVA instancia en cada vuelta
                var unidadCantidades = new DUnidadMenuCantidades();
                unidadCantidades.unidad = unidad.unidad;

                foreach (DTipoMenu tipoMenu in listaTipoMenu)
                {
                    listaFiltroDetallesXUnidad = listaDetalles.Where(x => x.unidad_id == unidad.id_unidad && x.tipo_menu_id == tipoMenu.id_tipo_menu).ToList();
                    int almuerzo = 0;
                    int cena = 0;

                    foreach (DRacionElaboradaDetalles detalle in listaFiltroDetallesXUnidad)
                    {
                        almuerzo = almuerzo + detalle.almuerzo;
                        cena = cena + detalle.cena;
                    }

                    if (tipoMenu.id_tipo_menu == 1)
                    {
                        unidadCantidades.P12_A = almuerzo;
                        unidadCantidades.P12_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 2)
                    {
                        unidadCantidades.P24_A = almuerzo;
                        unidadCantidades.P24_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 3)
                    {
                        unidadCantidades.IntN_A = almuerzo;
                        unidadCantidades.IntN_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 4)
                    {
                        unidadCantidades.Astr_A = almuerzo;
                        unidadCantidades.Astr_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 5)
                    {
                        unidadCantidades.Celi_A = almuerzo;
                        unidadCantidades.Celi_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 6)
                    {
                        unidadCantidades.AFib_A = almuerzo;
                        unidadCantidades.AFib_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 7)
                    {
                        unidadCantidades.Hep_A = almuerzo;
                        unidadCantidades.Hep_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 8)
                    {
                        unidadCantidades.SSal_A = almuerzo;
                        unidadCantidades.SSal_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 9)
                    {
                        unidadCantidades.HivTbc_A = almuerzo;
                        unidadCantidades.HivTbc_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 10)
                    {
                        unidadCantidades.Men_A = almuerzo;
                        unidadCantidades.Men_C = cena;
                    }
                    if (tipoMenu.id_tipo_menu == 11)
                    {
                        unidadCantidades.SobreAl_A = almuerzo;
                        unidadCantidades.SobreAl_C = cena;
                    }


                }

                listaUnidadesCantidades.Add(unidadCantidades);
            }
            //fin contar valores de menus en cada unidad

            AgregarFilaTotales(listaUnidadesCantidades);

            dtgUnidadesCantidades.DataSource = null;
            dtgUnidadesCantidades.DataSource = listaUnidadesCantidades;

            //formato columnas            
            if (listaUnidadesCantidades.Count > 0)
            {
                dtgUnidadesCantidades.Columns[0].Width = 150;
                dtgUnidadesCantidades.Columns[1].Width = 50;
                dtgUnidadesCantidades.Columns[2].Width = 50;
                dtgUnidadesCantidades.Columns[3].Width = 50;
                dtgUnidadesCantidades.Columns[4].Width = 50;
                dtgUnidadesCantidades.Columns[5].Width = 50;
                dtgUnidadesCantidades.Columns[6].Width = 50;
                dtgUnidadesCantidades.Columns[7].Width = 50;
                dtgUnidadesCantidades.Columns[8].Width = 50;
                dtgUnidadesCantidades.Columns[9].Width = 50;
                dtgUnidadesCantidades.Columns[10].Width = 50;
                dtgUnidadesCantidades.Columns[11].Width = 50;
                dtgUnidadesCantidades.Columns[12].Width = 50;
                dtgUnidadesCantidades.Columns[13].Width = 50;
                dtgUnidadesCantidades.Columns[14].Width = 50;
                dtgUnidadesCantidades.Columns[15].Width = 50;
                dtgUnidadesCantidades.Columns[16].Width = 50;
                dtgUnidadesCantidades.Columns[17].Width = 50;
                dtgUnidadesCantidades.Columns[18].Width = 50;
                dtgUnidadesCantidades.Columns[19].Width = 50;
                dtgUnidadesCantidades.Columns[20].Width = 50;
                dtgUnidadesCantidades.Columns[21].Width = 50;
                dtgUnidadesCantidades.Columns[22].Width = 50;

                dtgUnidadesCantidades.Columns[1].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgUnidadesCantidades.Columns[2].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgUnidadesCantidades.Columns[5].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgUnidadesCantidades.Columns[6].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgUnidadesCantidades.Columns[9].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgUnidadesCantidades.Columns[10].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgUnidadesCantidades.Columns[13].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgUnidadesCantidades.Columns[14].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgUnidadesCantidades.Columns[17].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgUnidadesCantidades.Columns[18].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgUnidadesCantidades.Columns[21].DefaultCellStyle.BackColor = Color.SandyBrown;
                dtgUnidadesCantidades.Columns[22].DefaultCellStyle.BackColor = Color.SandyBrown;

            }

            //formato fila totales
            foreach (DataGridViewRow row in dtgUnidadesCantidades.Rows)
            {
                if (row.Cells["unidad"].Value?.ToString() == "Totales")
                {
                    row.DefaultCellStyle.Font =
                        new Font(dtgUnidadesCantidades.Font, FontStyle.Bold);

                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.ForeColor = Color.Black;

                    break; // solo una fila
                }
            }

        }//FIN ACTUALIZAR UNIDADES CANTIDADES......................................

        //AGREGAR FILA TOTALES UNIDAD
        private void AgregarFilaTotales(List<DUnidadMenuCantidades> lista)
        {
            // Evitar duplicar la fila Totales
            lista.RemoveAll(x => x.unidad == "Totales");

            var totales = new DUnidadMenuCantidades
            {
                unidad = "Totales",

                P12_A = lista.Sum(x => x.P12_A),
                P12_C = lista.Sum(x => x.P12_C),

                P24_A = lista.Sum(x => x.P24_A),
                P24_C = lista.Sum(x => x.P24_C),

                IntN_A = lista.Sum(x => x.IntN_A),
                IntN_C = lista.Sum(x => x.IntN_C),

                Astr_A = lista.Sum(x => x.Astr_A),
                Astr_C = lista.Sum(x => x.Astr_C),

                Celi_A = lista.Sum(x => x.Celi_A),
                Celi_C = lista.Sum(x => x.Celi_C),

                AFib_A = lista.Sum(x => x.AFib_A),
                AFib_C = lista.Sum(x => x.AFib_C),

                Hep_A = lista.Sum(x => x.Hep_A),
                Hep_C = lista.Sum(x => x.Hep_C),

                SSal_A = lista.Sum(x => x.SSal_A),
                SSal_C = lista.Sum(x => x.SSal_C),

                HivTbc_A = lista.Sum(x => x.HivTbc_A),
                HivTbc_C = lista.Sum(x => x.HivTbc_C),

                Men_A = lista.Sum(x => x.Men_A),
                Men_C = lista.Sum(x => x.Men_C),

                SobreAl_A = lista.Sum(x => x.SobreAl_A),
                SobreAl_C = lista.Sum(x => x.SobreAl_C),
            };

            lista.Add(totales);

            int totalAux = totales.P12_A + totales.P12_C + totales.P24_A + totales.P24_C + totales.IntN_A + totales.IntN_C
                + totales.Astr_A + totales.Astr_C + totales.Celi_A + totales.Celi_C + totales.AFib_A + totales.AFib_C
                + totales.Hep_A + totales.Hep_C + totales.SSal_A + totales.SSal_C + totales.HivTbc_A + totales.HivTbc_C
                + totales.Men_A + totales.Men_C;

            txtTotal.Text = totalAux.ToString();

        } // FIN AGREGAR FILA TOTALES UNIDAD............................................................

        private void btnEditarEncabezado_Click(object sender, EventArgs e)
        {
            dtpFechaElaborada.Enabled = true;
            txtDetalles.Enabled = true;
            GuardarEditarEncabezado.Enabled = true;
            btnCancelarEditarEncabezado.Enabled = true;

        }

        private void GuardarEditarEncabezado_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdRacionElaborada.Text))
            {
                MessageBox.Show("Debe seleccionar un registro", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var nRacionElaborada = new NRacionElaborada();

                var elaborada = new DRacionElaborada
                {
                    id_racion_elaborada = Convert.ToInt32(txtIdRacionElaborada.Text),
                    fecha_elaborada = dtpFechaElaborada.Value,
                    detalles = txtDetalles.Text
                };

                nRacionElaborada.Editar(elaborada);
                MessageBox.Show("Editado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dtpFechaElaborada.Enabled = false;
                txtDetalles.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelarEditarEncabezado_Click(object sender, EventArgs e)
        {
            dtpFechaElaborada.Enabled = false;
            txtDetalles.Enabled = false;
            GuardarEditarEncabezado.Enabled = false;
            btnCancelarEditarEncabezado.Enabled = false;
        }

        private void btnEliminarRacionesCargadas_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtIdRacionElaborada.Text))
            {
                MessageBox.Show("Debe seleccionar un registro", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //advertencia al usuario
            DialogResult resultado = MessageBox.Show(
                "⚠️ Esta acción eliminará todos las raciones cargadas para esta elaborada.\n\n¿Desea continuar?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (resultado == DialogResult.No)
                return;


            try
            {
                var nRacionElaboradaDetalles = new NRacionElaboradaDetalles();

                nRacionElaboradaDetalles.EliminarDetalles(Convert.ToInt32(txtIdRacionElaborada.Text));
                MessageBox.Show("Eliminado correctamente", "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.ActualizarUnidadesCantidades();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nutricion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
