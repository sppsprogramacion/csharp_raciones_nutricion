namespace CapaPresentacion
{
    partial class FormSolicitadasBuscar
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
            this.dtgRacionesSolicitadas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRacionesSolicitadas)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgRacionesSolicitadas
            // 
            this.dtgRacionesSolicitadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRacionesSolicitadas.Location = new System.Drawing.Point(12, 34);
            this.dtgRacionesSolicitadas.Name = "dtgRacionesSolicitadas";
            this.dtgRacionesSolicitadas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgRacionesSolicitadas.Size = new System.Drawing.Size(776, 385);
            this.dtgRacionesSolicitadas.TabIndex = 103;
            this.dtgRacionesSolicitadas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgRacionesSolicitadas_KeyDown);
            // 
            // FormSolicitadasBuscar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dtgRacionesSolicitadas);
            this.Name = "FormSolicitadasBuscar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BUSCAR SOLICITADA";
            this.Load += new System.EventHandler(this.FormSolicitadasBuscar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgRacionesSolicitadas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgRacionesSolicitadas;
    }
}