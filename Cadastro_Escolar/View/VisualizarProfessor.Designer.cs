namespace Cadastro_Escolar.View
{
    partial class VisualizarProfessor
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
            this.gridProfessor = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridProfessor)).BeginInit();
            this.SuspendLayout();
            // 
            // gridProfessor
            // 
            this.gridProfessor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProfessor.Location = new System.Drawing.Point(12, 13);
            this.gridProfessor.Name = "gridProfessor";
            this.gridProfessor.Size = new System.Drawing.Size(1054, 561);
            this.gridProfessor.TabIndex = 1;
            // 
            // VisualizarProfessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 586);
            this.Controls.Add(this.gridProfessor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VisualizarProfessor";
            this.Text = "VisualizarProfessor";
            this.Load += new System.EventHandler(this.VisualizarProfessor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridProfessor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridProfessor;
    }
}