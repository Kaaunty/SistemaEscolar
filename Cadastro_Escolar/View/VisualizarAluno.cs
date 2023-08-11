using Cadastro_Escolar.Model;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
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
            //Costumização do Grid
            gridAluno.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridAluno.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            gridAluno.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridAluno.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridAluno.RowHeadersVisible = false;
            gridAluno.AllowUserToAddRows = false;
            gridAluno.AllowUserToDeleteRows = false;
            gridAluno.AllowUserToResizeColumns = false;
            gridAluno.AllowUserToResizeRows = false;
            gridAluno.MultiSelect = false;
            gridAluno.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridAluno.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            gridAluno.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gridAluno.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            gridAluno.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            gridAluno.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            gridAluno.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            gridAluno.BackgroundColor = Color.White;
            gridAluno.BorderStyle = BorderStyle.None;
            gridAluno.ReadOnly = true;
            //
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
                pbFoto.Image = System.Drawing.Image.FromStream(ms);
            }
            gridAluno.CurrentRow.Selected = true;
            lblRA.Text = gridAluno.CurrentRow.Cells[1].Value.ToString();
            lblNome.Text = gridAluno.CurrentRow.Cells[2].Value.ToString();
            lblCurso.Text = gridAluno.CurrentRow.Cells[3].Value.ToString();
            lblMateria.Text = gridAluno.CurrentRow.Cells[4].Value.ToString();
            lblBolsista.Text = gridAluno.CurrentRow.Cells[5].Value.ToString();
            lblMensalidade.Text = gridAluno.CurrentRow.Cells[6].Value.ToString();
            lblEstadoCivil.Text = gridAluno.CurrentRow.Cells[7].Value.ToString();
            lblGenero.Text = gridAluno.CurrentRow.Cells[8].Value.ToString();
            if (DateTime.TryParse(gridAluno.CurrentRow.Cells[9].Value.ToString(), out DateTime data))
            {

                lblData.Text = data.ToString("dd/MM/yyyy");
            }
            lblEndereco.Text = gridAluno.CurrentRow.Cells[10].Value.ToString();
            lblEmail.Text = gridAluno.CurrentRow.Cells[11].Value.ToString();
            lblTelefone.Text = gridAluno.CurrentRow.Cells[12].Value.ToString();
            lblCep.Text = gridAluno.CurrentRow.Cells[14].Value.ToString();
            lblEstado.Text = gridAluno.CurrentRow.Cells[15].Value.ToString();
            lblCidade.Text = gridAluno.CurrentRow.Cells[16].Value.ToString();






        }


    }
}
