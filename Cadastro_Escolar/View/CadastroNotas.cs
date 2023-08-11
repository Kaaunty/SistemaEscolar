using Cadastro_Escolar.Entidades;
using Cadastro_Escolar.Model;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Cadastro_Escolar.View
{
    public partial class CadastroNotas : Form
    {
        Conexao con = new Conexao();
        BoletimModel model = new Model.BoletimModel();

        public CadastroNotas()
        {
            InitializeComponent();
            lblMedia.Text = "";

            btnSalvar.FlatAppearance.MouseOverBackColor = btnSalvar.BackColor;
            btnSalvar.BackColorChanged += (s, e) =>
            {
                btnSalvar.FlatAppearance.MouseOverBackColor = btnSalvar.BackColor;
            };
        }

        #region Salvar, Calcular e Limpar
        private void Salvar(Boletim dado)
        {
            try
            {
                dado.IdDisciplina = gridAluno.CurrentRow.Cells[2].Value.ToString();
                dado.IdAluno = Convert.ToInt32(gridAluno.CurrentRow.Cells[0].Value.ToString());
                dado.Idprofessor = Convert.ToInt32(gridAluno.CurrentRow.Cells[3].Value.ToString());

                dado.Nota1 = Convert.ToDouble(txtNota1.Text);
                dado.Nota2 = Convert.ToDouble(txtNota4.Text);
                dado.Nota3 = Convert.ToDouble(txtNota2.Text);
                dado.Nota4 = Convert.ToDouble(txtNota3.Text);
                dado.Media = Convert.ToDouble(lblMedia.Text);
                if (dado.Media >= 7)
                {
                    dado.Situacao = "Aprovado";
                }
                else
                {
                    dado.Situacao = "Reprovado";
                }

                model.Salvar(dado);
                MessageBox.Show("Dados Salvos com Sucesso!");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Erro ao salvar os Dados: " + ex.Message);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Calcular();
            Boletim dados = new Boletim();
            Salvar(dados);
            Limpar();
        }

        public void Calcular()
        {
            try
            {

                double
                    nota1,
                    nota2,
                    nota3,
                    nota4,
                    media;

                if (
                    txtNota1.Text == ""
                    || txtNota4.Text == ""
                    || txtNota2.Text == ""
                    || txtNota3.Text == ""
                )
                {
                    MessageBox.Show("Digite todas as notas");
                }
                else
                {
                    if (!double.TryParse(txtNota1.Text, out nota1) || nota1 < 0 || nota1 > 10)
                    {
                        MessageBox.Show(
                            "Digite um número de 0 a 10 para a nota 1",
                            "Mensagem do Sistema"
                        );
                        return;
                    }


                    if (!double.TryParse(txtNota4.Text, out nota2) || nota2 < 0 || nota2 > 10)
                    {
                        MessageBox.Show(
                            "Digite um número de 0 a 10 para a nota 2",
                            "Mensagem do Sistema"
                        );
                        return;
                    }


                    if (!double.TryParse(txtNota2.Text, out nota3) || nota3 < 0 || nota3 > 10)
                    {
                        MessageBox.Show(
                            "Digite um número de 0 a 10 para a nota 3",
                            "Mensagem do Sistema"
                        );
                        return;
                    }


                    if (!double.TryParse(txtNota3.Text, out nota4) || nota4 < 0 || nota4 > 10)
                    {
                        MessageBox.Show(
                            "Digite um número de 0 a 10 para a nota 4",
                            "Mensagem do Sistema"
                        );
                        return;
                    }


                    media = (nota1 + nota2 + nota3 + nota4) / 4;
                    lblMedia.Text = media.ToString("N2");
                }
            }
            catch (FormatException)
            {

                MessageBox.Show("Digite números válidos para as notas", "Mensagem do Sistema");
            }
        }


        public void Limpar()
        {
            //Limpo as variáveis
            txtNota1.Text = "";
            txtNota4.Text = "";
            txtNota2.Text = "";
            txtNota3.Text = "";
            lblMedia.Text = "";
            lblMateria.Text = "";
            lblAluno.Text = "";
            lblProfessor.Text = "";
        }
        #endregion

        #region Keypress Verificar
        private void txtNota1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Teste condicional para aceitar números no textbox
            if (!(char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }

            //Teste condicional para aceitar a tecla Backspace e vírgula
            if (e.KeyChar == 8 || e.KeyChar == 44)
            {
                e.Handled = false;
            }
        }

        private void txtNota2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 8 || e.KeyChar == 44)
            {
                e.Handled = false;
            }
        }

        private void txtNota3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 8 || e.KeyChar == 44)
            {
                e.Handled = false;
            }
        }

        private void txtNota4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 8 || e.KeyChar == 44)
            {
                e.Handled = false;
            }
        }

        #endregion

        private void CadastrarNotas_Load(object sender, EventArgs e)
        {

            GridAluno();
            Preencher();
            lblMateria.Text = "";
            lblAluno.Text = "";
            lblProfessor.Text = "";
        }
        private void gridAluno_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblAluno.Text = gridAluno.Rows[e.RowIndex].Cells[1].Value.ToString();
            lblMateria.Text = gridAluno.Rows[e.RowIndex].Cells[2].Value.ToString();
            lblProfessor.Text = gridAluno.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void GridAluno()
        {
            gridAluno.Columns.Add("ID_Aluno", "ID Aluno");
            gridAluno.Columns.Add("Nome_Aluno", "Nome do Aluno");
            gridAluno.Columns.Add("Disciplina_Aluno", "Matéria do Aluno");
            gridAluno.Columns.Add("ID_Professor", "ID Professor");
            gridAluno.Columns[3].Visible = false;
            gridAluno.Columns.Add("Nome_Professor", "Nome do Professor");
            gridAluno.Columns.Add("Materia_Professor", "Matéria do Professor");
            gridAluno.Columns[5].Visible = false;
            // Costumização do Grid
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
        }



        private void Preencher()
        {
            string query = @"SELECT a.id AS ID_Aluno,
                                a.nome AS Nome_Aluno, 
                                a.materia AS Disciplina_Aluno, 
                                p.id AS ID_Professor,
                                p.nome AS Nome_Professor, 
                                p.materia AS Materia_Professor
                                FROM aluno a
                                JOIN professor p ON a.materia = p.materia
                                LEFT JOIN boletim b ON a.id = b.id_aluno
                                WHERE b.id IS NULL";




            try
            {
                con.AbrirConexao();
                MySqlCommand command = new MySqlCommand(query, con.con);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    gridAluno.Rows.Add(
                        reader["ID_Aluno"],
                        reader["Nome_Aluno"],
                        reader["Disciplina_Aluno"],
                        reader["ID_Professor"],
                        reader["Nome_Professor"],
                        reader["Materia_Professor"]
                    );
                }

                reader.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            finally
            {
                con.FecharConexao();
            }
        }

        private void txtNota4_Leave(object sender, EventArgs e)
        {
            Calcular();
        }
    }
}

