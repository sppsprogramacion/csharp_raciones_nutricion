namespace CapaPresentacion
{
    partial class FormCargaSolicitadasCantidadesSap
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
            this.label8 = new System.Windows.Forms.Label();
            this.dtgSapCantidades = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSapCantidades)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 13);
            this.label8.TabIndex = 111;
            this.label8.Text = "SAP - CANTIDADES:";
            // 
            // dtgSapCantidades
            // 
            this.dtgSapCantidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSapCantidades.Location = new System.Drawing.Point(10, 24);
            this.dtgSapCantidades.Name = "dtgSapCantidades";
            this.dtgSapCantidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgSapCantidades.Size = new System.Drawing.Size(1231, 375);
            this.dtgSapCantidades.TabIndex = 110;
            // 
            // FormCargaSolicitadasCantidadesSap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 450);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtgSapCantidades);
            this.Name = "FormCargaSolicitadasCantidadesSap";
            this.Text = "CANTIDAD SAP - SOLICITADAS";
            this.Load += new System.EventHandler(this.FormCargaSolicitadasCantidadesSap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgSapCantidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dtgSapCantidades;
    }
}