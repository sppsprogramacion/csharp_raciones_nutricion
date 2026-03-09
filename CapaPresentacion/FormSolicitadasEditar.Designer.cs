namespace CapaPresentacion
{
    partial class FormSolicitadasEditar
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
            this.label7 = new System.Windows.Forms.Label();
            this.txtMenu = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCena = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAlmuerzo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIdSolicitadaDetalle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUnidad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgRacionesCargadas = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFechaElaborada = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtIdRacionSolicitada = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRacionesCargadas)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(117, 366);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 159;
            this.label7.Text = "TIPO MENU:";
            // 
            // txtMenu
            // 
            this.txtMenu.Enabled = false;
            this.txtMenu.Location = new System.Drawing.Point(120, 380);
            this.txtMenu.Name = "txtMenu";
            this.txtMenu.ReadOnly = true;
            this.txtMenu.Size = new System.Drawing.Size(195, 20);
            this.txtMenu.TabIndex = 158;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.White;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Green;
            this.btnGuardar.Location = new System.Drawing.Point(312, 414);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(98, 40);
            this.btnGuardar.TabIndex = 156;
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
            this.btnCancelar.Location = new System.Drawing.Point(416, 414);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 40);
            this.btnCancelar.TabIndex = 157;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(421, 366);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 155;
            this.label4.Text = "CENA:";
            // 
            // txtCena
            // 
            this.txtCena.Location = new System.Drawing.Point(424, 380);
            this.txtCena.Name = "txtCena";
            this.txtCena.Size = new System.Drawing.Size(90, 20);
            this.txtCena.TabIndex = 154;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(322, 366);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 153;
            this.label5.Text = "ALMUERZO:";
            // 
            // txtAlmuerzo
            // 
            this.txtAlmuerzo.Location = new System.Drawing.Point(324, 380);
            this.txtAlmuerzo.Name = "txtAlmuerzo";
            this.txtAlmuerzo.Size = new System.Drawing.Size(90, 20);
            this.txtAlmuerzo.TabIndex = 152;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 366);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 151;
            this.label6.Text = "IDENTIFICADOR:";
            // 
            // txtIdSolicitadaDetalle
            // 
            this.txtIdSolicitadaDetalle.Enabled = false;
            this.txtIdSolicitadaDetalle.Location = new System.Drawing.Point(10, 380);
            this.txtIdSolicitadaDetalle.Name = "txtIdSolicitadaDetalle";
            this.txtIdSolicitadaDetalle.ReadOnly = true;
            this.txtIdSolicitadaDetalle.Size = new System.Drawing.Size(100, 20);
            this.txtIdSolicitadaDetalle.TabIndex = 150;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 149;
            this.label2.Text = "UNIDAD:";
            // 
            // txtUnidad
            // 
            this.txtUnidad.Enabled = false;
            this.txtUnidad.Location = new System.Drawing.Point(237, 27);
            this.txtUnidad.Name = "txtUnidad";
            this.txtUnidad.ReadOnly = true;
            this.txtUnidad.Size = new System.Drawing.Size(177, 20);
            this.txtUnidad.TabIndex = 148;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 147;
            this.label1.Text = "RACIONES CARGADAS";
            // 
            // dtgRacionesCargadas
            // 
            this.dtgRacionesCargadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRacionesCargadas.Location = new System.Drawing.Point(9, 78);
            this.dtgRacionesCargadas.Name = "dtgRacionesCargadas";
            this.dtgRacionesCargadas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgRacionesCargadas.Size = new System.Drawing.Size(589, 259);
            this.dtgRacionesCargadas.TabIndex = 146;
            this.dtgRacionesCargadas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgRacionesCargadas_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 145;
            this.label3.Text = "FECHA:";
            // 
            // txtFechaElaborada
            // 
            this.txtFechaElaborada.Enabled = false;
            this.txtFechaElaborada.Location = new System.Drawing.Point(131, 27);
            this.txtFechaElaborada.Name = "txtFechaElaborada";
            this.txtFechaElaborada.ReadOnly = true;
            this.txtFechaElaborada.Size = new System.Drawing.Size(97, 20);
            this.txtFechaElaborada.TabIndex = 144;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 13);
            this.label11.TabIndex = 143;
            this.label11.Text = "ID SOLICITADA:";
            // 
            // txtIdRacionSolicitada
            // 
            this.txtIdRacionSolicitada.Enabled = false;
            this.txtIdRacionSolicitada.Location = new System.Drawing.Point(10, 27);
            this.txtIdRacionSolicitada.Name = "txtIdRacionSolicitada";
            this.txtIdRacionSolicitada.ReadOnly = true;
            this.txtIdRacionSolicitada.Size = new System.Drawing.Size(108, 20);
            this.txtIdRacionSolicitada.TabIndex = 142;
            // 
            // FormSolicitadasEditar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 489);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMenu);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCena);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAlmuerzo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtIdSolicitadaDetalle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUnidad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgRacionesCargadas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFechaElaborada);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtIdRacionSolicitada);
            this.Name = "FormSolicitadasEditar";
            this.Text = "EDITAR SOLICITADAS";
            this.Load += new System.EventHandler(this.FormSolicitadasEditar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgRacionesCargadas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMenu;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCena;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAlmuerzo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIdSolicitadaDetalle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUnidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgRacionesCargadas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFechaElaborada;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtIdRacionSolicitada;
    }
}