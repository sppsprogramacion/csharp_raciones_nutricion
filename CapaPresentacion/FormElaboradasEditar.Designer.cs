namespace CapaPresentacion
{
    partial class FormElaboradasEditar
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtFechaElaborada = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtIdRacionElaborada = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgRacionesCargadas = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUnidad = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCena = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAlmuerzo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIdElaboradaDetalle = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMenu = new System.Windows.Forms.TextBox();
            this.gboxCargarUna = new System.Windows.Forms.GroupBox();
            this.txtIdSapCargarUna = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtIdMenuCargarUna = new System.Windows.Forms.TextBox();
            this.dtgTiposMenus = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAlmuerzoCargaUna = new System.Windows.Forms.TextBox();
            this.txtMenuCargarUna = new System.Windows.Forms.TextBox();
            this.btnGuardarCargaUna = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCancelarCargaUna = new System.Windows.Forms.Button();
            this.txtCenaCargaUna = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.gboxEditarCargadas = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRacionesCargadas)).BeginInit();
            this.gboxCargarUna.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTiposMenus)).BeginInit();
            this.gboxEditarCargadas.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 127;
            this.label3.Text = "FECHA:";
            // 
            // txtFechaElaborada
            // 
            this.txtFechaElaborada.Enabled = false;
            this.txtFechaElaborada.Location = new System.Drawing.Point(131, 24);
            this.txtFechaElaborada.Name = "txtFechaElaborada";
            this.txtFechaElaborada.ReadOnly = true;
            this.txtFechaElaborada.Size = new System.Drawing.Size(97, 20);
            this.txtFechaElaborada.TabIndex = 126;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 13);
            this.label11.TabIndex = 125;
            this.label11.Text = "ID ELABORADA:";
            // 
            // txtIdRacionElaborada
            // 
            this.txtIdRacionElaborada.Enabled = false;
            this.txtIdRacionElaborada.Location = new System.Drawing.Point(10, 24);
            this.txtIdRacionElaborada.Name = "txtIdRacionElaborada";
            this.txtIdRacionElaborada.ReadOnly = true;
            this.txtIdRacionElaborada.Size = new System.Drawing.Size(108, 20);
            this.txtIdRacionElaborada.TabIndex = 124;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 129;
            this.label1.Text = "RACIONES CARGADAS";
            // 
            // dtgRacionesCargadas
            // 
            this.dtgRacionesCargadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRacionesCargadas.Location = new System.Drawing.Point(8, 36);
            this.dtgRacionesCargadas.Name = "dtgRacionesCargadas";
            this.dtgRacionesCargadas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgRacionesCargadas.Size = new System.Drawing.Size(520, 259);
            this.dtgRacionesCargadas.TabIndex = 128;
            this.dtgRacionesCargadas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgRacionesCargadas_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 131;
            this.label2.Text = "UNIDAD:";
            // 
            // txtUnidad
            // 
            this.txtUnidad.Enabled = false;
            this.txtUnidad.Location = new System.Drawing.Point(237, 24);
            this.txtUnidad.Name = "txtUnidad";
            this.txtUnidad.ReadOnly = true;
            this.txtUnidad.Size = new System.Drawing.Size(177, 20);
            this.txtUnidad.TabIndex = 130;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(420, 324);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 137;
            this.label4.Text = "CENA:";
            // 
            // txtCena
            // 
            this.txtCena.Location = new System.Drawing.Point(423, 338);
            this.txtCena.Name = "txtCena";
            this.txtCena.Size = new System.Drawing.Size(90, 20);
            this.txtCena.TabIndex = 136;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(321, 324);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 135;
            this.label5.Text = "ALMUERZO:";
            // 
            // txtAlmuerzo
            // 
            this.txtAlmuerzo.Location = new System.Drawing.Point(323, 338);
            this.txtAlmuerzo.Name = "txtAlmuerzo";
            this.txtAlmuerzo.Size = new System.Drawing.Size(90, 20);
            this.txtAlmuerzo.TabIndex = 134;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 324);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 133;
            this.label6.Text = "IDENTIFICADOR:";
            // 
            // txtIdElaboradaDetalle
            // 
            this.txtIdElaboradaDetalle.Enabled = false;
            this.txtIdElaboradaDetalle.Location = new System.Drawing.Point(9, 338);
            this.txtIdElaboradaDetalle.Name = "txtIdElaboradaDetalle";
            this.txtIdElaboradaDetalle.ReadOnly = true;
            this.txtIdElaboradaDetalle.Size = new System.Drawing.Size(100, 20);
            this.txtIdElaboradaDetalle.TabIndex = 132;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.White;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Green;
            this.btnGuardar.Location = new System.Drawing.Point(311, 372);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(98, 40);
            this.btnGuardar.TabIndex = 138;
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
            this.btnCancelar.Location = new System.Drawing.Point(415, 372);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 40);
            this.btnCancelar.TabIndex = 139;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(116, 324);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 141;
            this.label7.Text = "TIPO MENU:";
            // 
            // txtMenu
            // 
            this.txtMenu.Enabled = false;
            this.txtMenu.Location = new System.Drawing.Point(119, 338);
            this.txtMenu.Name = "txtMenu";
            this.txtMenu.ReadOnly = true;
            this.txtMenu.Size = new System.Drawing.Size(195, 20);
            this.txtMenu.TabIndex = 140;
            // 
            // gboxCargarUna
            // 
            this.gboxCargarUna.Controls.Add(this.txtIdSapCargarUna);
            this.gboxCargarUna.Controls.Add(this.label14);
            this.gboxCargarUna.Controls.Add(this.label13);
            this.gboxCargarUna.Controls.Add(this.txtIdMenuCargarUna);
            this.gboxCargarUna.Controls.Add(this.dtgTiposMenus);
            this.gboxCargarUna.Controls.Add(this.label8);
            this.gboxCargarUna.Controls.Add(this.label12);
            this.gboxCargarUna.Controls.Add(this.txtAlmuerzoCargaUna);
            this.gboxCargarUna.Controls.Add(this.txtMenuCargarUna);
            this.gboxCargarUna.Controls.Add(this.btnGuardarCargaUna);
            this.gboxCargarUna.Controls.Add(this.label9);
            this.gboxCargarUna.Controls.Add(this.btnCancelarCargaUna);
            this.gboxCargarUna.Controls.Add(this.txtCenaCargaUna);
            this.gboxCargarUna.Controls.Add(this.label10);
            this.gboxCargarUna.Location = new System.Drawing.Point(561, 59);
            this.gboxCargarUna.Name = "gboxCargarUna";
            this.gboxCargarUna.Size = new System.Drawing.Size(402, 417);
            this.gboxCargarUna.TabIndex = 164;
            this.gboxCargarUna.TabStop = false;
            this.gboxCargarUna.Text = "Cargar nuevo";
            // 
            // txtIdSapCargarUna
            // 
            this.txtIdSapCargarUna.Enabled = false;
            this.txtIdSapCargarUna.Location = new System.Drawing.Point(76, 384);
            this.txtIdSapCargarUna.Name = "txtIdSapCargarUna";
            this.txtIdSapCargarUna.ReadOnly = true;
            this.txtIdSapCargarUna.Size = new System.Drawing.Size(46, 20);
            this.txtIdSapCargarUna.TabIndex = 170;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(73, 370);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 171;
            this.label14.Text = "ID SAP:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 13);
            this.label13.TabIndex = 169;
            this.label13.Text = "TIPOS DE MENU";
            // 
            // txtIdMenuCargarUna
            // 
            this.txtIdMenuCargarUna.Enabled = false;
            this.txtIdMenuCargarUna.Location = new System.Drawing.Point(11, 384);
            this.txtIdMenuCargarUna.Name = "txtIdMenuCargarUna";
            this.txtIdMenuCargarUna.ReadOnly = true;
            this.txtIdMenuCargarUna.Size = new System.Drawing.Size(46, 20);
            this.txtIdMenuCargarUna.TabIndex = 160;
            // 
            // dtgTiposMenus
            // 
            this.dtgTiposMenus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgTiposMenus.Location = new System.Drawing.Point(9, 37);
            this.dtgTiposMenus.Name = "dtgTiposMenus";
            this.dtgTiposMenus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgTiposMenus.Size = new System.Drawing.Size(385, 259);
            this.dtgTiposMenus.TabIndex = 168;
            this.dtgTiposMenus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgTiposMenus_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 321);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 163;
            this.label8.Text = "TIPO MENU:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 370);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 161;
            this.label12.Text = "ID TIPO:";
            // 
            // txtAlmuerzoCargaUna
            // 
            this.txtAlmuerzoCargaUna.Location = new System.Drawing.Point(224, 335);
            this.txtAlmuerzoCargaUna.Name = "txtAlmuerzoCargaUna";
            this.txtAlmuerzoCargaUna.Size = new System.Drawing.Size(80, 20);
            this.txtAlmuerzoCargaUna.TabIndex = 162;
            // 
            // txtMenuCargarUna
            // 
            this.txtMenuCargarUna.Enabled = false;
            this.txtMenuCargarUna.Location = new System.Drawing.Point(10, 335);
            this.txtMenuCargarUna.Name = "txtMenuCargarUna";
            this.txtMenuCargarUna.ReadOnly = true;
            this.txtMenuCargarUna.Size = new System.Drawing.Size(205, 20);
            this.txtMenuCargarUna.TabIndex = 162;
            // 
            // btnGuardarCargaUna
            // 
            this.btnGuardarCargaUna.BackColor = System.Drawing.Color.White;
            this.btnGuardarCargaUna.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnGuardarCargaUna.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarCargaUna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarCargaUna.ForeColor = System.Drawing.Color.Green;
            this.btnGuardarCargaUna.Location = new System.Drawing.Point(190, 368);
            this.btnGuardarCargaUna.Name = "btnGuardarCargaUna";
            this.btnGuardarCargaUna.Size = new System.Drawing.Size(98, 40);
            this.btnGuardarCargaUna.TabIndex = 166;
            this.btnGuardarCargaUna.Text = "Guardar";
            this.btnGuardarCargaUna.UseVisualStyleBackColor = false;
            this.btnGuardarCargaUna.Click += new System.EventHandler(this.btnGuardarCargaUna_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(222, 321);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 163;
            this.label9.Text = "ALMUERZO:";
            // 
            // btnCancelarCargaUna
            // 
            this.btnCancelarCargaUna.BackColor = System.Drawing.Color.White;
            this.btnCancelarCargaUna.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrange;
            this.btnCancelarCargaUna.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarCargaUna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarCargaUna.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnCancelarCargaUna.Location = new System.Drawing.Point(294, 368);
            this.btnCancelarCargaUna.Name = "btnCancelarCargaUna";
            this.btnCancelarCargaUna.Size = new System.Drawing.Size(98, 40);
            this.btnCancelarCargaUna.TabIndex = 167;
            this.btnCancelarCargaUna.Text = "Cancelar";
            this.btnCancelarCargaUna.UseVisualStyleBackColor = false;
            this.btnCancelarCargaUna.Click += new System.EventHandler(this.btnCancelarCargaUna_Click);
            // 
            // txtCenaCargaUna
            // 
            this.txtCenaCargaUna.Location = new System.Drawing.Point(313, 335);
            this.txtCenaCargaUna.Name = "txtCenaCargaUna";
            this.txtCenaCargaUna.Size = new System.Drawing.Size(80, 20);
            this.txtCenaCargaUna.TabIndex = 164;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(310, 321);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 165;
            this.label10.Text = "CENA:";
            // 
            // gboxEditarCargadas
            // 
            this.gboxEditarCargadas.Controls.Add(this.label1);
            this.gboxEditarCargadas.Controls.Add(this.dtgRacionesCargadas);
            this.gboxEditarCargadas.Controls.Add(this.label7);
            this.gboxEditarCargadas.Controls.Add(this.txtIdElaboradaDetalle);
            this.gboxEditarCargadas.Controls.Add(this.txtMenu);
            this.gboxEditarCargadas.Controls.Add(this.label6);
            this.gboxEditarCargadas.Controls.Add(this.btnGuardar);
            this.gboxEditarCargadas.Controls.Add(this.txtAlmuerzo);
            this.gboxEditarCargadas.Controls.Add(this.btnCancelar);
            this.gboxEditarCargadas.Controls.Add(this.label5);
            this.gboxEditarCargadas.Controls.Add(this.label4);
            this.gboxEditarCargadas.Controls.Add(this.txtCena);
            this.gboxEditarCargadas.Location = new System.Drawing.Point(10, 60);
            this.gboxEditarCargadas.Name = "gboxEditarCargadas";
            this.gboxEditarCargadas.Size = new System.Drawing.Size(540, 417);
            this.gboxEditarCargadas.TabIndex = 165;
            this.gboxEditarCargadas.TabStop = false;
            this.gboxEditarCargadas.Text = "Editar Cargadas";
            // 
            // FormElaboradasEditar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 489);
            this.Controls.Add(this.gboxEditarCargadas);
            this.Controls.Add(this.gboxCargarUna);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUnidad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFechaElaborada);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtIdRacionElaborada);
            this.Name = "FormElaboradasEditar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EDITAR ELABORADAS";
            this.Load += new System.EventHandler(this.FormElaboradasEditar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgRacionesCargadas)).EndInit();
            this.gboxCargarUna.ResumeLayout(false);
            this.gboxCargarUna.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTiposMenus)).EndInit();
            this.gboxEditarCargadas.ResumeLayout(false);
            this.gboxEditarCargadas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFechaElaborada;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtIdRacionElaborada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgRacionesCargadas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUnidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCena;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAlmuerzo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIdElaboradaDetalle;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMenu;
        private System.Windows.Forms.GroupBox gboxCargarUna;
        private System.Windows.Forms.TextBox txtIdSapCargarUna;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtIdMenuCargarUna;
        private System.Windows.Forms.DataGridView dtgTiposMenus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtAlmuerzoCargaUna;
        private System.Windows.Forms.TextBox txtMenuCargarUna;
        private System.Windows.Forms.Button btnGuardarCargaUna;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCancelarCargaUna;
        private System.Windows.Forms.TextBox txtCenaCargaUna;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox gboxEditarCargadas;
    }
}