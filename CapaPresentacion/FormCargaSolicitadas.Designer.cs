namespace CapaPresentacion
{
    partial class FormCargaSolicitadas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbUnidades = new System.Windows.Forms.ComboBox();
            this.dtgSapMenus = new System.Windows.Forms.DataGridView();
            this.dtgUnidadesCantidades = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMostrar = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtIdRacionSolicitada = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtConvenio = new System.Windows.Forms.TextBox();
            this.txtDetalles = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFechaCarga = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnActualizarUnidades = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnVerCantidadesXSap = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.GuardarEditarEncabezado = new System.Windows.Forms.Button();
            this.btnEditarEncabezado = new System.Windows.Forms.Button();
            this.btnEliminarRacionesCargadas = new System.Windows.Forms.Button();
            this.btnCancelarEditarEncabezado = new System.Windows.Forms.Button();
            this.dtpFechaSolicitada = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSapMenus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgUnidadesCantidades)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 82;
            this.label5.Text = "UNIDADES:";
            // 
            // cmbUnidades
            // 
            this.cmbUnidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnidades.FormattingEnabled = true;
            this.cmbUnidades.Location = new System.Drawing.Point(11, 221);
            this.cmbUnidades.Name = "cmbUnidades";
            this.cmbUnidades.Size = new System.Drawing.Size(385, 21);
            this.cmbUnidades.TabIndex = 81;
            this.cmbUnidades.SelectedIndexChanged += new System.EventHandler(this.cmbUnidades_SelectedIndexChanged);
            // 
            // dtgSapMenus
            // 
            this.dtgSapMenus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSapMenus.Location = new System.Drawing.Point(498, 20);
            this.dtgSapMenus.Name = "dtgSapMenus";
            this.dtgSapMenus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtgSapMenus.Size = new System.Drawing.Size(631, 259);
            this.dtgSapMenus.TabIndex = 83;
            this.dtgSapMenus.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dtgSapMenus_EditingControlShowing);
            // 
            // dtgUnidadesCantidades
            // 
            this.dtgUnidadesCantidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgUnidadesCantidades.Location = new System.Drawing.Point(11, 324);
            this.dtgUnidadesCantidades.Name = "dtgUnidadesCantidades";
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgUnidadesCantidades.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgUnidadesCantidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgUnidadesCantidades.Size = new System.Drawing.Size(1231, 378);
            this.dtgUnidadesCantidades.TabIndex = 84;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(501, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 85;
            this.label1.Text = "CARGAR CANTIDADES:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 308);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "UNIDADES - CANTIDADES:";
            // 
            // btnMostrar
            // 
            this.btnMostrar.BackColor = System.Drawing.Color.White;
            this.btnMostrar.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnMostrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMostrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMostrar.ForeColor = System.Drawing.Color.Indigo;
            this.btnMostrar.Location = new System.Drawing.Point(404, 216);
            this.btnMostrar.Name = "btnMostrar";
            this.btnMostrar.Size = new System.Drawing.Size(85, 28);
            this.btnMostrar.TabIndex = 87;
            this.btnMostrar.Text = "Mostrar";
            this.btnMostrar.UseVisualStyleBackColor = false;
            this.btnMostrar.Click += new System.EventHandler(this.btnMostrar_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 13);
            this.label11.TabIndex = 95;
            this.label11.Text = "ID SOLICITADA:";
            // 
            // txtIdRacionSolicitada
            // 
            this.txtIdRacionSolicitada.Enabled = false;
            this.txtIdRacionSolicitada.Location = new System.Drawing.Point(11, 19);
            this.txtIdRacionSolicitada.Name = "txtIdRacionSolicitada";
            this.txtIdRacionSolicitada.ReadOnly = true;
            this.txtIdRacionSolicitada.Size = new System.Drawing.Size(108, 20);
            this.txtIdRacionSolicitada.TabIndex = 88;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 94;
            this.label4.Text = "DETALLES:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(249, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 91;
            this.label7.Text = "CONVENIO:";
            // 
            // txtConvenio
            // 
            this.txtConvenio.Enabled = false;
            this.txtConvenio.Location = new System.Drawing.Point(251, 19);
            this.txtConvenio.Name = "txtConvenio";
            this.txtConvenio.ReadOnly = true;
            this.txtConvenio.Size = new System.Drawing.Size(145, 20);
            this.txtConvenio.TabIndex = 90;
            // 
            // txtDetalles
            // 
            this.txtDetalles.Enabled = false;
            this.txtDetalles.Location = new System.Drawing.Point(11, 58);
            this.txtDetalles.Multiline = true;
            this.txtDetalles.Name = "txtDetalles";
            this.txtDetalles.Size = new System.Drawing.Size(385, 95);
            this.txtDetalles.TabIndex = 89;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 97;
            this.label3.Text = "FECHA:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 101;
            this.label6.Text = "FECHA CARGA:";
            // 
            // txtFechaCarga
            // 
            this.txtFechaCarga.Enabled = false;
            this.txtFechaCarga.Location = new System.Drawing.Point(10, 174);
            this.txtFechaCarga.Name = "txtFechaCarga";
            this.txtFechaCarga.ReadOnly = true;
            this.txtFechaCarga.Size = new System.Drawing.Size(107, 20);
            this.txtFechaCarga.TabIndex = 100;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.White;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.Indigo;
            this.btnBuscar.Location = new System.Drawing.Point(405, 5);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(85, 28);
            this.btnBuscar.TabIndex = 103;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.White;
            this.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.Indigo;
            this.btnNuevo.Location = new System.Drawing.Point(405, 37);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(85, 28);
            this.btnNuevo.TabIndex = 102;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.White;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Green;
            this.btnGuardar.Location = new System.Drawing.Point(1140, 19);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(98, 40);
            this.btnGuardar.TabIndex = 104;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrange;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnCancelar.Location = new System.Drawing.Point(1140, 66);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 40);
            this.btnCancelar.TabIndex = 105;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnActualizarUnidades
            // 
            this.btnActualizarUnidades.BackColor = System.Drawing.Color.White;
            this.btnActualizarUnidades.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnActualizarUnidades.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarUnidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarUnidades.ForeColor = System.Drawing.Color.Indigo;
            this.btnActualizarUnidades.Location = new System.Drawing.Point(158, 293);
            this.btnActualizarUnidades.Name = "btnActualizarUnidades";
            this.btnActualizarUnidades.Size = new System.Drawing.Size(98, 28);
            this.btnActualizarUnidades.TabIndex = 108;
            this.btnActualizarUnidades.Text = "Actualizar";
            this.btnActualizarUnidades.UseVisualStyleBackColor = false;
            this.btnActualizarUnidades.Click += new System.EventHandler(this.btnActualizarUnidades_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.White;
            this.btnEditar.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.Indigo;
            this.btnEditar.Location = new System.Drawing.Point(404, 250);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(85, 28);
            this.btnEditar.TabIndex = 135;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnVerCantidadesXSap
            // 
            this.btnVerCantidadesXSap.BackColor = System.Drawing.Color.White;
            this.btnVerCantidadesXSap.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnVerCantidadesXSap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerCantidadesXSap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerCantidadesXSap.ForeColor = System.Drawing.Color.Indigo;
            this.btnVerCantidadesXSap.Location = new System.Drawing.Point(271, 293);
            this.btnVerCantidadesXSap.Name = "btnVerCantidadesXSap";
            this.btnVerCantidadesXSap.Size = new System.Drawing.Size(170, 28);
            this.btnVerCantidadesXSap.TabIndex = 137;
            this.btnVerCantidadesXSap.Text = "Ver Cantidades por Sap";
            this.btnVerCantidadesXSap.UseVisualStyleBackColor = false;
            this.btnVerCantidadesXSap.Click += new System.EventHandler(this.btnVerCantidadesXSap_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(981, 710);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 16);
            this.label8.TabIndex = 139;
            this.label8.Text = "TOTAL:";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(1043, 706);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(200, 22);
            this.txtTotal.TabIndex = 138;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.White;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label28.Location = new System.Drawing.Point(7, 729);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1230, 17);
            this.label28.TabIndex = 140;
            this.label28.Text = "S.N.";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GuardarEditarEncabezado
            // 
            this.GuardarEditarEncabezado.BackColor = System.Drawing.Color.White;
            this.GuardarEditarEncabezado.Enabled = false;
            this.GuardarEditarEncabezado.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.GuardarEditarEncabezado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GuardarEditarEncabezado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GuardarEditarEncabezado.ForeColor = System.Drawing.Color.Green;
            this.GuardarEditarEncabezado.Location = new System.Drawing.Point(405, 102);
            this.GuardarEditarEncabezado.Name = "GuardarEditarEncabezado";
            this.GuardarEditarEncabezado.Size = new System.Drawing.Size(85, 28);
            this.GuardarEditarEncabezado.TabIndex = 148;
            this.GuardarEditarEncabezado.Text = "Guardar";
            this.GuardarEditarEncabezado.UseVisualStyleBackColor = false;
            this.GuardarEditarEncabezado.Click += new System.EventHandler(this.GuardarEditarEncabezado_Click);
            // 
            // btnEditarEncabezado
            // 
            this.btnEditarEncabezado.BackColor = System.Drawing.Color.White;
            this.btnEditarEncabezado.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnEditarEncabezado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarEncabezado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarEncabezado.ForeColor = System.Drawing.Color.Indigo;
            this.btnEditarEncabezado.Location = new System.Drawing.Point(405, 70);
            this.btnEditarEncabezado.Name = "btnEditarEncabezado";
            this.btnEditarEncabezado.Size = new System.Drawing.Size(85, 28);
            this.btnEditarEncabezado.TabIndex = 147;
            this.btnEditarEncabezado.Text = "Editar";
            this.btnEditarEncabezado.UseVisualStyleBackColor = false;
            this.btnEditarEncabezado.Click += new System.EventHandler(this.btnEditarEncabezado_Click);
            // 
            // btnEliminarRacionesCargadas
            // 
            this.btnEliminarRacionesCargadas.BackColor = System.Drawing.Color.White;
            this.btnEliminarRacionesCargadas.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEliminarRacionesCargadas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarRacionesCargadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarRacionesCargadas.ForeColor = System.Drawing.Color.Red;
            this.btnEliminarRacionesCargadas.Location = new System.Drawing.Point(405, 167);
            this.btnEliminarRacionesCargadas.Name = "btnEliminarRacionesCargadas";
            this.btnEliminarRacionesCargadas.Size = new System.Drawing.Size(85, 28);
            this.btnEliminarRacionesCargadas.TabIndex = 149;
            this.btnEliminarRacionesCargadas.Text = "Eliminar";
            this.btnEliminarRacionesCargadas.UseVisualStyleBackColor = false;
            this.btnEliminarRacionesCargadas.Click += new System.EventHandler(this.btnEliminarRacionesCargadas_Click);
            // 
            // btnCancelarEditarEncabezado
            // 
            this.btnCancelarEditarEncabezado.BackColor = System.Drawing.Color.White;
            this.btnCancelarEditarEncabezado.Enabled = false;
            this.btnCancelarEditarEncabezado.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
            this.btnCancelarEditarEncabezado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarEditarEncabezado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarEditarEncabezado.ForeColor = System.Drawing.Color.Orange;
            this.btnCancelarEditarEncabezado.Location = new System.Drawing.Point(405, 134);
            this.btnCancelarEditarEncabezado.Name = "btnCancelarEditarEncabezado";
            this.btnCancelarEditarEncabezado.Size = new System.Drawing.Size(85, 28);
            this.btnCancelarEditarEncabezado.TabIndex = 150;
            this.btnCancelarEditarEncabezado.Text = "Cancelar";
            this.btnCancelarEditarEncabezado.UseVisualStyleBackColor = false;
            this.btnCancelarEditarEncabezado.Click += new System.EventHandler(this.btnCancelarEditarEncabezado_Click);
            // 
            // dtpFechaSolicitada
            // 
            this.dtpFechaSolicitada.Enabled = false;
            this.dtpFechaSolicitada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaSolicitada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaSolicitada.Location = new System.Drawing.Point(129, 19);
            this.dtpFechaSolicitada.Name = "dtpFechaSolicitada";
            this.dtpFechaSolicitada.Size = new System.Drawing.Size(113, 21);
            this.dtpFechaSolicitada.TabIndex = 151;
            // 
            // FormCargaSolicitadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 749);
            this.Controls.Add(this.dtpFechaSolicitada);
            this.Controls.Add(this.btnCancelarEditarEncabezado);
            this.Controls.Add(this.btnEliminarRacionesCargadas);
            this.Controls.Add(this.GuardarEditarEncabezado);
            this.Controls.Add(this.btnEditarEncabezado);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.btnVerCantidadesXSap);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnActualizarUnidades);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFechaCarga);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtIdRacionSolicitada);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtConvenio);
            this.Controls.Add(this.txtDetalles);
            this.Controls.Add(this.btnMostrar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgUnidadesCantidades);
            this.Controls.Add(this.dtgSapMenus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbUnidades);
            this.Name = "FormCargaSolicitadas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CARGA SOLICITADAS";
            this.Load += new System.EventHandler(this.FormCargaSolicitadas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgSapMenus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgUnidadesCantidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbUnidades;
        private System.Windows.Forms.DataGridView dtgSapMenus;
        private System.Windows.Forms.DataGridView dtgUnidadesCantidades;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMostrar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtIdRacionSolicitada;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtConvenio;
        private System.Windows.Forms.TextBox txtDetalles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFechaCarga;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnActualizarUnidades;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnVerCantidadesXSap;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button GuardarEditarEncabezado;
        private System.Windows.Forms.Button btnEditarEncabezado;
        private System.Windows.Forms.Button btnEliminarRacionesCargadas;
        private System.Windows.Forms.Button btnCancelarEditarEncabezado;
        private System.Windows.Forms.DateTimePicker dtpFechaSolicitada;
    }
}