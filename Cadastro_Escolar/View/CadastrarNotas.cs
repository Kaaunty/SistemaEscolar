using Cadastro_Escolar.Entidades;
using Cadastro_Escolar.Model;
using Cadastro_Escolar.WSCorreio;
using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Cadastro_Escolar.View
{
    public partial class CadastrarNotas : Form
    {
        #region Argumentos
        MySqlCommand sql;
        Conexao con = new Conexao();
        BoletimModel model = new Model.BoletimModel();
        AlunoModel alunoModel = new AlunoModel();

        #endregion

        public CadastrarNotas()
        {
            InitializeComponent();
        }

        #region Salvar, Calcular e Limpar
        private void Salvar(Boletim dado)
        {
            try
            {
                dado.IdDisciplina = gridAluno.CurrentRow.Cells[4].Value.ToString();
                dado.IdAluno = Convert.ToInt32(gridAluno.CurrentRow.Cells[0].Value.ToString());
                dado.Nota1 = Convert.ToDouble(txtNota1.Text);
                dado.Nota2 = Convert.ToDouble(txtNota2.Text);
                dado.Nota3 = Convert.ToDouble(txtNota3.Text);
                dado.Nota4 = Convert.ToDouble(txtNota4.Text);

                dado.Media = Convert.ToDouble(lblMedia.Text);

                model.Salvar(dado);
                MessageBox.Show("Dados Salvos com Sucesso!");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Erro ao salvar os Daados: " + ex.Message);
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
                // Declaro as variáveis e as converto para double
                double nota1,
                    nota2,
                    nota3,
                    nota4,
                    media;

                if (
                    txtNota1.Text == ""
                    || txtNota2.Text == ""
                    || txtNota3.Text == ""
                    || txtNota4.Text == ""
                )
                {
                    MessageBox.Show("Digite todas as notas");
                }
                else
                {
                    // Convertendo e validando a nota 1
                    if (!double.TryParse(txtNota1.Text, out nota1) || nota1 < 0 || nota1 > 10)
                    {
                        MessageBox.Show(
                            "Digite um número de 0 a 10 para a nota 1",
                            "Mensagem do Sistema"
                        );
                        return;
                    }

                    // Convertendo e validando a nota 2
                    if (!double.TryParse(txtNota2.Text, out nota2) || nota2 < 0 || nota2 > 10)
                    {
                        MessageBox.Show(
                            "Digite um número de 0 a 10 para a nota 2",
                            "Mensagem do Sistema"
                        );
                        return;
                    }

                    // Convertendo e validando a nota 3
                    if (!double.TryParse(txtNota3.Text, out nota3) || nota3 < 0 || nota3 > 10)
                    {
                        MessageBox.Show(
                            "Digite um número de 0 a 10 para a nota 3",
                            "Mensagem do Sistema"
                        );
                        return;
                    }

                    // Convertendo e validando a nota 4
                    if (!double.TryParse(txtNota4.Text, out nota4) || nota4 < 0 || nota4 > 10)
                    {
                        MessageBox.Show(
                            "Digite um número de 0 a 10 para a nota 4",
                            "Mensagem do Sistema"
                        );
                        return;
                    }

                    // Calculando a média e exibindo no label
                    media = (nota1 + nota2 + nota3 + nota4) / 4;
                    lblMedia.Text = media.ToString();
                }
            }
            catch (FormatException)
            {
                // Caso ocorra algum erro, apresento uma mensagem ao usuário
                MessageBox.Show("Digite números válidos para as notas", "Mensagem do Sistema");
            }
        }


        public void Limpar()
        {
            //Limpo as variáveis
            txtNota1.Text = "";
            txtNota2.Text = "";
            txtNota3.Text = "";
            txtNota4.Text = "";
            lblMedia.Text = "";
            lblMateria.Text = "";
            lblAluno.Text = "";
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
        }
        private void gridAluno_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblAluno.Text = gridAluno.Rows[e.RowIndex].Cells[1].Value.ToString();
            lblMateria.Text = gridAluno.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void GridAluno()
        {

            gridAluno.Columns.Add("id", "ID");
            gridAluno.Columns.Add("nome", "Nome do Aluno");
            gridAluno.Columns.Add("materia", "Matéria");
            gridAluno.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridAluno.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }



        private void Preencher()
        {
            string query = "SELECT a.id, a.nome, a.materia " +
            "FROM aluno a " +
            "LEFT JOIN boletim b ON a.id = b.id_aluno" +
            " WHERE b.id_aluno IS NULL OR(b.nota1 IS NULL AND b.nota2 IS NULL AND b.nota3 IS NULL AND b.nota4 IS NULL)";

            try
            {
                con.AbrirConexao();
                MySqlCommand command = new MySqlCommand(query, con.con);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    gridAluno.Rows.Add(reader["id"], reader["nome"], reader["materia"]);
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


    }
}

