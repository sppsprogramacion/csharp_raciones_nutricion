namespace CapaPresentacion
{
    partial class FormSolicitadasNuevo
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtConvenio = new System.Windows.Forms.TextBox();
            this.txtDetalles = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.dtgRacionesSolicitadas = new System.Windows.Forms.DataGridView();
            this.dtpFechaSolicitada = new System.Windows.Forms.DateTimePicker();
            this.label34 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRacionesSolicitadas)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 105;
            this.label3.Text = "DETALLES";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 103;
            this.label1.Text = "CONVENIO";
            // 
            // txtConvenio
            // 
            this.txtConvenio.Location = new System.Drawing.Point(153, 28);
            this.txtConvenio.Name = "txtConvenio";
            this.txtConvenio.Size = new System.Drawing.Size(290, 20);
            this.txtConvenio.TabIndex = 92;
            // 
            // txtDetalles
            // 
            this.txtDetalles.Location = new System.Drawing.Point(17, 69);
            this.txtDetalles.Multiline = true;
            this.txtDetalles.Name = "txtDetalles";
            this.txtDetalles.Size = new System.Drawing.Size(609, 99);
            this.txtDetalles.TabIndex = 93;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.White;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Green;
            this.btnGuardar.Location = new System.Drawing.Point(690, 68);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(98, 40);
            this.btnGuardar.TabIndex = 100;
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
            this.btnCancelar.Location = new System.Drawing.Point(691, 111);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 40);
            this.btnCancelar.TabIndex = 101;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // dtgRacionesSolicitadas
            // 
            this.dtgRacionesSolicitadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRacionesSolicitadas.Location = new System.Drawing.Point(12, 189);
            this.dtgRacionesSolicitadas.Name = "dtgRacionesSolicitadas";
            this.dtgRacionesSolicitadas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgRacionesSolicitadas.Size = new System.Drawing.Size(776, 203);
            this.dtgRacionesSolicitadas.TabIndex = 102;
            this.dtgRacionesSolicitadas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgRacionesSolicitadas_KeyDown);
            // 
            // dtpFechaSolicitada
            // 
            this.dtpFechaSolicitada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaSolicitada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaSolicitada.Location = new System.Drawing.Point(16, 29);
            this.dtpFechaSolicitada.Name = "dtpFechaSolicitada";
            this.dtpFechaSolicitada.Size = new System.Drawing.Size(118, 21);
            this.dtpFechaSolicitada.TabIndex = 112;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(14, 10);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(120, 15);
            this.label34.TabIndex = 113;
            this.label34.Text = "FECHA SOLICITADA:";
            // 
            // FormSolicitadasNuevo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 422);
            this.Controls.Add(this.dtpFechaSolicitada);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtConvenio);
            this.Controls.Add(this.txtDetalles);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.dtgRacionesSolicitadas);
            this.Name = "FormSolicitadasNuevo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NUEVA SOLICITADA";
            this.Load += new System.EventHandler(this.FormSolicitadasNuevo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgRacionesSolicitadas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConvenio;
        private System.Windows.Forms.TextBox txtDetalles;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridView dtgRacionesSolicitadas;
        private System.Windows.Forms.DateTimePicker dtpFechaSolicitada;
        private System.Windows.Forms.Label label34;
    }
}