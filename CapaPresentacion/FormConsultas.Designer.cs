namespace CapaPresentacion
{
    partial class FormConsultas
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
            this.dtgResultado = new System.Windows.Forms.DataGridView();
            this.btnMostrar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.btnImrpimirParteDiario = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnImprimirEstadistico = new System.Windows.Forms.Button();
            this.btnImprimirParteDiarioNovedades = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgResultado)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgResultado
            // 
            this.dtgResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgResultado.Location = new System.Drawing.Point(12, 124);
            this.dtgResultado.Name = "dtgResultado";
            this.dtgResultado.Size = new System.Drawing.Size(897, 546);
            this.dtgResultado.TabIndex = 6;
            // 
            // btnMostrar
            // 
            this.btnMostrar.BackColor = System.Drawing.Color.White;
            this.btnMostrar.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnMostrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMostrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMostrar.ForeColor = System.Drawing.Color.Indigo;
            this.btnMostrar.Location = new System.Drawing.Point(285, 14);
            this.btnMostrar.Name = "btnMostrar";
            this.btnMostrar.Size = new System.Drawing.Size(100, 40);
            this.btnMostrar.TabIndex = 3;
            this.btnMostrar.Text = "Mostrar";
            this.btnMostrar.UseVisualStyleBackColor = false;
            this.btnMostrar.Click += new System.EventHandler(this.btnMostrar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 15);
            this.label3.TabIndex = 123;
            this.label3.Text = "FECHA INICIO:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(15, 33);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(115, 21);
            this.dtpFechaInicio.TabIndex = 1;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(149, 13);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(71, 15);
            this.label21.TabIndex = 121;
            this.label21.Text = "FECHA FIN:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(152, 33);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(115, 21);
            this.dtpFechaFin.TabIndex = 2;
            // 
            // btnImrpimirParteDiario
            // 
            this.btnImrpimirParteDiario.BackColor = System.Drawing.Color.White;
            this.btnImrpimirParteDiario.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnImrpimirParteDiario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImrpimirParteDiario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImrpimirParteDiario.ForeColor = System.Drawing.Color.Indigo;
            this.btnImrpimirParteDiario.Location = new System.Drawing.Point(619, 15);
            this.btnImrpimirParteDiario.Name = "btnImrpimirParteDiario";
            this.btnImrpimirParteDiario.Size = new System.Drawing.Size(128, 40);
            this.btnImrpimirParteDiario.TabIndex = 5;
            this.btnImrpimirParteDiario.Text = "Imprimir partes diarios";
            this.btnImrpimirParteDiario.UseVisualStyleBackColor = false;
            this.btnImrpimirParteDiario.Click += new System.EventHandler(this.btnImrpimirParteDiario_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.White;
            this.btnExportar.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnExportar.Location = new System.Drawing.Point(285, 60);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(100, 40);
            this.btnExportar.TabIndex = 4;
            this.btnExportar.Text = "Exportar excel";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnImprimirEstadistico
            // 
            this.btnImprimirEstadistico.BackColor = System.Drawing.Color.White;
            this.btnImprimirEstadistico.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnImprimirEstadistico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirEstadistico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirEstadistico.ForeColor = System.Drawing.Color.Indigo;
            this.btnImprimirEstadistico.Location = new System.Drawing.Point(619, 60);
            this.btnImprimirEstadistico.Name = "btnImprimirEstadistico";
            this.btnImprimirEstadistico.Size = new System.Drawing.Size(128, 40);
            this.btnImprimirEstadistico.TabIndex = 124;
            this.btnImprimirEstadistico.Text = "Imprimir estadistico";
            this.btnImprimirEstadistico.UseVisualStyleBackColor = false;
            this.btnImprimirEstadistico.Click += new System.EventHandler(this.btnImprimirEstadistico_Click);
            // 
            // btnImprimirParteDiarioNovedades
            // 
            this.btnImprimirParteDiarioNovedades.BackColor = System.Drawing.Color.White;
            this.btnImprimirParteDiarioNovedades.FlatAppearance.BorderColor = System.Drawing.Color.Indigo;
            this.btnImprimirParteDiarioNovedades.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirParteDiarioNovedades.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirParteDiarioNovedades.ForeColor = System.Drawing.Color.Indigo;
            this.btnImprimirParteDiarioNovedades.Location = new System.Drawing.Point(753, 14);
            this.btnImprimirParteDiarioNovedades.Name = "btnImprimirParteDiarioNovedades";
            this.btnImprimirParteDiarioNovedades.Size = new System.Drawing.Size(128, 40);
            this.btnImprimirParteDiarioNovedades.TabIndex = 125;
            this.btnImprimirParteDiarioNovedades.Text = "Imprimir parte diario novedades";
            this.btnImprimirParteDiarioNovedades.UseVisualStyleBackColor = false;
            this.btnImprimirParteDiarioNovedades.Click += new System.EventHandler(this.btnImprimirParteDiarioNovedades_Click);
            // 
            // FormConsultas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 691);
            this.Controls.Add(this.btnImprimirParteDiarioNovedades);
            this.Controls.Add(this.btnImprimirEstadistico);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnImrpimirParteDiario);
            this.Controls.Add(this.btnMostrar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.dtpFechaFin);
            this.Controls.Add(this.dtgResultado);
            this.Name = "FormConsultas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONSULTAS";
            this.Load += new System.EventHandler(this.FormConsultas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgResultado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgResultado;
        private System.Windows.Forms.Button btnMostrar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Button btnImrpimirParteDiario;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnImprimirEstadistico;
        private System.Windows.Forms.Button btnImprimirParteDiarioNovedades;
    }
}