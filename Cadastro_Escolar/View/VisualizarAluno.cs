using Cadastro_Escolar.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cadastro_Escolar.View
{
    public partial class VisualizarAluno : Form
    {
        AlunoModel modelAluno = new AlunoModel();
        public VisualizarAluno()
        {
            InitializeComponent();
            Listar();
        }


        public void Listar()
        {

            gridAluno.DataSource = modelAluno.Listar();
            gridAluno.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridAluno.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            gridAluno.Columns[0].Visible = false;
            gridAluno.Columns[1].HeaderText = "RA";
            gridAluno.Columns[2].HeaderText = "Nome";
            gridAluno.Columns[3].HeaderText = "Curso";
            gridAluno.Columns[4].HeaderText = "Materia";
            gridAluno.Columns[5].HeaderText = "Bolsista";
            gridAluno.Columns[6].HeaderText = "Mensalidade";
            gridAluno.Columns[7].HeaderText = "Estado Civil";
            gridAluno.Columns[8].HeaderText = "Genero";
            gridAluno.Columns[9].HeaderText = "Data de Nascimento";
            gridAluno.Columns[10].HeaderText = "Endereco";
            gridAluno.Columns[11].HeaderText = "Email";
            gridAluno.Columns[12].HeaderText = "Telefone";
            gridAluno.Columns[13].Visible = false;
            gridAluno.Columns[14].HeaderText = "CEP";
            gridAluno.Columns[15].HeaderText = "Estado";
            gridAluno.Columns[16].HeaderText = "Cidade";
        }

        private void gridAluno_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridAluno.CurrentRow.Cells[13].Value == DBNull.Value)
            {
                pbFoto.Image = null;
            }
            else
            {
                byte[] img = (byte[])gridAluno.CurrentRow.Cells[13].Value;
                MemoryStream ms = new MemoryStream(img);
                pbFoto.Image = Image.FromStream(ms);
            }
        }
    }
}
