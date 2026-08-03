namespace CapaPresentacion
{
    partial class FormAnexos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnObservaciones = new System.Windows.Forms.Button();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.btnEliminarRegistrosCargados = new System.Windows.Forms.Button();
            this.btnEditarEncabezado = new System.Windows.Forms.Button();
            this.btnActualizarAnexo = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFechaCarga = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtIdAnexo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgAnexoDetalles = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbMenus = new System.Windows.Forms.ComboBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFactor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDetalle = new System.Windows.Forms.TextBox();
            this.btnGuardarCantidad = new System.Windows.Forms.Button();
            this.btnCancelarGuardarCantidad = new System.Windows.Forms.Button();
            this.gboxCargaDetalle = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAnexoDetalles)).BeginInit();
            this.gboxCargaDetalle.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnObservaciones
            // 
            this.btnObservaciones.BackColor = System.Drawing.Color.White;
            this.btnObservaciones.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnObservaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObservaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnObservaciones.ForeColor = System.Drawing.Color.Indigo;
            this.btnObservaciones.Location = new System.Drawing.Point(127, 170);
            this.btnObservaciones.Name = "btnObservaciones";
            this.btnObservaciones.Size = new System.Drawing.Size(112, 28);
            this.btnObservaciones.TabIndex = 4;
            this.btnObservaciones.Text = "Observaciones";
            this.btnObservaciones.UseVisualStyleBackColor = false;
            this.btnObservaciones.Click += new System.EventHandler(this.btnObservaciones_Click);
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Enabled = false;
            this.dtpFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(128, 21);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(113, 21);
            this.dtpFechaInicio.TabIndex = 178;
            // 
            // btnEliminarRegistrosCargados
            // 
            this.btnEliminarRegistrosCargados.BackColor = System.Drawing.Color.White;
            this.btnEliminarRegistrosCargados.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnEliminarRegistrosCargados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarRegistrosCargados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarRegistrosCargados.ForeColor = System.Drawing.Color.Red;
            this.btnEliminarRegistrosCargados.Location = new System.Drawing.Point(265, 222);
            this.btnEliminarRegistrosCargados.Name = "btnEliminarRegistrosCargados";
            this.btnEliminarRegistrosCargados.Size = new System.Drawing.Size(196, 28);
            this.btnEliminarRegistrosCargados.TabIndex = 7;
            this.btnEliminarRegistrosCargados.Text = "Eliminar registros cargadas";
            this.btnEliminarRegistrosCargados.UseVisualStyleBackColor = false;
            this.btnEliminarRegistrosCargados.Click += new System.EventHandler(this.btnEliminarRegistrosCargados_Click);
            // 
            // btnEditarEncabezado
            // 
            this.btnEditarEncabezado.BackColor = System.Drawing.Color.White;
            this.btnEditarEncabezado.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnEditarEncabezado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarEncabezado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarEncabezado.ForeColor = System.Drawing.Color.Indigo;
            this.btnEditarEncabezado.Location = new System.Drawing.Point(393, 96);
            this.btnEditarEncabezado.Name = "btnEditarEncabezado";
            this.btnEditarEncabezado.Size = new System.Drawing.Size(85, 28);
            this.btnEditarEncabezado.TabIndex = 3;
            this.btnEditarEncabezado.Text = "Editar";
            this.btnEditarEncabezado.UseVisualStyleBackColor = false;
            this.btnEditarEncabezado.Click += new System.EventHandler(this.btnEditarEncabezado_Click);
            // 
            // btnActualizarAnexo
            // 
            this.btnActualizarAnexo.BackColor = System.Drawing.Color.White;
            this.btnActualizarAnexo.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnActualizarAnexo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarAnexo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarAnexo.ForeColor = System.Drawing.Color.Indigo;
            this.btnActualizarAnexo.Location = new System.Drawing.Point(154, 222);
            this.btnActualizarAnexo.Name = "btnActualizarAnexo";
            this.btnActualizarAnexo.Size = new System.Drawing.Size(98, 28);
            this.btnActualizarAnexo.TabIndex = 6;
            this.btnActualizarAnexo.Text = "Actualizar";
            this.btnActualizarAnexo.UseVisualStyleBackColor = false;
            this.btnActualizarAnexo.Click += new System.EventHandler(this.btnActualizarAnexo_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.White;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.Indigo;
            this.btnBuscar.Location = new System.Drawing.Point(393, 23);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(85, 28);
            this.btnBuscar.TabIndex = 1;
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
            this.btnNuevo.Location = new System.Drawing.Point(393, 61);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(85, 28);
            this.btnNuevo.TabIndex = 2;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 169;
            this.label6.Text = "FECHA CARGA:";
            // 
            // txtFechaCarga
            // 
            this.txtFechaCarga.Enabled = false;
            this.txtFechaCarga.Location = new System.Drawing.Point(9, 176);
            this.txtFechaCarga.Name = "txtFechaCarga";
            this.txtFechaCarga.ReadOnly = true;
            this.txtFechaCarga.Size = new System.Drawing.Size(107, 20);
            this.txtFechaCarga.TabIndex = 168;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 167;
            this.label3.Text = "FECHA INICIO:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 166;
            this.label11.Text = "ID ANEXO:";
            // 
            // txtIdAnexo
            // 
            this.txtIdAnexo.Enabled = false;
            this.txtIdAnexo.Location = new System.Drawing.Point(10, 21);
            this.txtIdAnexo.Name = "txtIdAnexo";
            this.txtIdAnexo.ReadOnly = true;
            this.txtIdAnexo.Size = new System.Drawing.Size(108, 20);
            this.txtIdAnexo.TabIndex = 161;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 165;
            this.label4.Text = "DESCRIPCION:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(10, 60);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(375, 95);
            this.txtDescripcion.TabIndex = 162;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 159;
            this.label2.Text = "DETALLES DEL ANEXO";
            // 
            // dtgAnexoDetalles
            // 
            this.dtgAnexoDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAnexoDetalles.Location = new System.Drawing.Point(10, 253);
            this.dtgAnexoDetalles.Name = "dtgAnexoDetalles";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgAnexoDetalles.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgAnexoDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAnexoDetalles.Size = new System.Drawing.Size(918, 300);
            this.dtgAnexoDetalles.TabIndex = 8;
            this.dtgAnexoDetalles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgAnexoDetalles_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 155;
            this.label5.Text = "MENUS";
            // 
            // cmbMenus
            // 
            this.cmbMenus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMenus.FormattingEnabled = true;
            this.cmbMenus.Location = new System.Drawing.Point(9, 34);
            this.cmbMenus.Name = "cmbMenus";
            this.cmbMenus.Size = new System.Drawing.Size(284, 21);
            this.cmbMenus.TabIndex = 1;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(8, 121);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(90, 20);
            this.txtCantidad.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 182;
            this.label1.Text = "CANTIDAD";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(113, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 184;
            this.label7.Text = "FACTOR";
            // 
            // txtFactor
            // 
            this.txtFactor.Location = new System.Drawing.Point(116, 121);
            this.txtFactor.Name = "txtFactor";
            this.txtFactor.Size = new System.Drawing.Size(90, 20);
            this.txtFactor.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 186;
            this.label8.Text = "DETALLE";
            // 
            // txtDetalle
            // 
            this.txtDetalle.Location = new System.Drawing.Point(9, 79);
            this.txtDetalle.Name = "txtDetalle";
            this.txtDetalle.Size = new System.Drawing.Size(398, 20);
            this.txtDetalle.TabIndex = 2;
            // 
            // btnGuardarCantidad
            // 
            this.btnGuardarCantidad.BackColor = System.Drawing.Color.White;
            this.btnGuardarCantidad.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnGuardarCantidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarCantidad.ForeColor = System.Drawing.Color.Green;
            this.btnGuardarCantidad.Location = new System.Drawing.Point(227, 149);
            this.btnGuardarCantidad.Name = "btnGuardarCantidad";
            this.btnGuardarCantidad.Size = new System.Drawing.Size(85, 28);
            this.btnGuardarCantidad.TabIndex = 5;
            this.btnGuardarCantidad.Text = "Guardar";
            this.btnGuardarCantidad.UseVisualStyleBackColor = false;
            this.btnGuardarCantidad.Click += new System.EventHandler(this.btnGuardarCantidad_Click);
            // 
            // btnCancelarGuardarCantidad
            // 
            this.btnCancelarGuardarCantidad.BackColor = System.Drawing.Color.White;
            this.btnCancelarGuardarCantidad.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
            this.btnCancelarGuardarCantidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarGuardarCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarGuardarCantidad.ForeColor = System.Drawing.Color.Orange;
            this.btnCancelarGuardarCantidad.Location = new System.Drawing.Point(322, 149);
            this.btnCancelarGuardarCantidad.Name = "btnCancelarGuardarCantidad";
            this.btnCancelarGuardarCantidad.Size = new System.Drawing.Size(85, 28);
            this.btnCancelarGuardarCantidad.TabIndex = 6;
            this.btnCancelarGuardarCantidad.Text = "Cancelar";
            this.btnCancelarGuardarCantidad.UseVisualStyleBackColor = false;
            this.btnCancelarGuardarCantidad.Click += new System.EventHandler(this.btnCancelarGuardarCantidad_Click);
            // 
            // gboxCargaDetalle
            // 
            this.gboxCargaDetalle.Controls.Add(this.label5);
            this.gboxCargaDetalle.Controls.Add(this.label8);
            this.gboxCargaDetalle.Controls.Add(this.btnCancelarGuardarCantidad);
            this.gboxCargaDetalle.Controls.Add(this.cmbMenus);
            this.gboxCargaDetalle.Controls.Add(this.txtDetalle);
            this.gboxCargaDetalle.Controls.Add(this.btnGuardarCantidad);
            this.gboxCargaDetalle.Controls.Add(this.txtCantidad);
            this.gboxCargaDetalle.Controls.Add(this.txtFactor);
            this.gboxCargaDetalle.Controls.Add(this.label1);
            this.gboxCargaDetalle.Controls.Add(this.label7);
            this.gboxCargaDetalle.Location = new System.Drawing.Point(508, 13);
            this.gboxCargaDetalle.Name = "gboxCargaDetalle";
            this.gboxCargaDetalle.Size = new System.Drawing.Size(420, 195);
            this.gboxCargaDetalle.TabIndex = 5;
            this.gboxCargaDetalle.TabStop = false;
            this.gboxCargaDetalle.Text = "Carga cantidad";
            // 
            // FormAnexos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(946, 571);
            this.Controls.Add(this.gboxCargaDetalle);
            this.Controls.Add(this.btnObservaciones);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.btnEliminarRegistrosCargados);
            this.Controls.Add(this.btnEditarEncabezado);
            this.Controls.Add(this.btnActualizarAnexo);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFechaCarga);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtIdAnexo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtgAnexoDetalles);
            this.Name = "FormAnexos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ANEXOS";
            this.Load += new System.EventHandler(this.FormAnexos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgAnexoDetalles)).EndInit();
            this.gboxCargaDetalle.ResumeLayout(false);
            this.gboxCargaDetalle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnObservaciones;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Button btnEliminarRegistrosCargados;
        private System.Windows.Forms.Button btnEditarEncabezado;
        private System.Windows.Forms.Button btnActualizarAnexo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFechaCarga;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtIdAnexo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dtgAnexoDetalles;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbMenus;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFactor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.Button btnGuardarCantidad;
        private System.Windows.Forms.Button btnCancelarGuardarCantidad;
        private System.Windows.Forms.GroupBox gboxCargaDetalle;
    }
}