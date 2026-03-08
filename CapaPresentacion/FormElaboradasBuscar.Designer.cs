namespace CapaPresentacion
{
    partial class FormElaboradasBuscar
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
            this.dtgRacionesElaboradas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRacionesElaboradas)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgRacionesElaboradas
            // 
            this.dtgRacionesElaboradas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRacionesElaboradas.Location = new System.Drawing.Point(12, 33);
            this.dtgRacionesElaboradas.Name = "dtgRacionesElaboradas";
            this.dtgRacionesElaboradas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgRacionesElaboradas.Size = new System.Drawing.Size(776, 385);
            this.dtgRacionesElaboradas.TabIndex = 104;
            this.dtgRacionesElaboradas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgRacionesElaboradas_KeyDown);
            // 
            // FormElaboradasBuscar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dtgRacionesElaboradas);
            this.Name = "FormElaboradasBuscar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BUSCAR ELABORADA";
            this.Load += new System.EventHandler(this.FormElaboradasBuscar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgRacionesElaboradas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgRacionesElaboradas;
    }
}