using Cadastro_Escolar.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cadastro_Escolar.View
{
    public partial class VisualizarNotas : Form
    {
        BoletimModel model = new BoletimModel();
        public VisualizarNotas()
        {
            InitializeComponent();
            Listar();
        }

        public void Listar()
        {
            gridNotas.DataSource = model.Listar();
            // Costumização do Grid
            gridNotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridNotas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            gridNotas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridNotas.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridNotas.RowHeadersVisible = false;
            gridNotas.AllowUserToAddRows = false;
            gridNotas.AllowUserToDeleteRows = false;
            gridNotas.AllowUserToResizeColumns = false;
            gridNotas.AllowUserToResizeRows = false;
            gridNotas.MultiSelect = false;
            gridNotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridNotas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            gridNotas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gridNotas.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            gridNotas.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            gridNotas.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            gridNotas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            gridNotas.BackgroundColor = Color.FromArgb(228, 232, 236);
            gridNotas.BorderStyle = BorderStyle.None;
            gridNotas.ReadOnly = true;
            //
            gridNotas.Columns[0].Visible = false;
            gridNotas.Columns[1].HeaderText = "Nome";
            gridNotas.Columns[2].HeaderText = "Materia";
            gridNotas.Columns[3].HeaderText = "Nota 1";
            gridNotas.Columns[4].HeaderText = "Nota 2";
            gridNotas.Columns[5].HeaderText = "Nota 3";
            gridNotas.Columns[6].HeaderText = "Nota 4";
            gridNotas.Columns[7].HeaderText = "Media";
            gridNotas.Columns[8].HeaderText = "Professor";
            gridNotas.Columns[9].HeaderText = "Situação";
            gridNotas.CellFormatting += GridNotas_CellFormatting;
        }
        private void GridNotas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 9) // Verifica se é uma célula válida na coluna "Situação"
            {
                string cellValue = e.Value.ToString();

                if (cellValue == "Aprovado")
                {
                    e.CellStyle.ForeColor = Color.Green; // Altera a cor do texto para verde
                }
                else if (cellValue == "Reprovado")
                {
                    e.CellStyle.ForeColor = Color.Red; // Altera a cor do texto para vermelho
                }
            }
        }
    }
}

