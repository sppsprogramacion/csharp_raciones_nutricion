namespace CapaPresentacion
{
    partial class FormAnexosBuscar
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
            this.dtgAnexos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAnexos)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgAnexos
            // 
            this.dtgAnexos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAnexos.Location = new System.Drawing.Point(12, 29);
            this.dtgAnexos.Name = "dtgAnexos";
            this.dtgAnexos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAnexos.Size = new System.Drawing.Size(776, 385);
            this.dtgAnexos.TabIndex = 105;
            this.dtgAnexos.DoubleClick += new System.EventHandler(this.dtgAnexos_DoubleClick);
            this.dtgAnexos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgAnexos_KeyDown);
            // 
            // FormAnexosBuscar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dtgAnexos);
            this.Name = "FormAnexosBuscar";
            this.Text = "BUSCAR ANEXO";
            this.Load += new System.EventHandler(this.FormAnexosBuscar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgAnexos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgAnexos;
    }
}