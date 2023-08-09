using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cadastro_Escolar.Entidades;
using Cadastro_Escolar.Model;
using MySql.Data.MySqlClient;

namespace Cadastro_Escolar.View
{
    public partial class CadastroProfessor : Form
    {
        CadastroAluno cadAluno = new CadastroAluno();
        ProfessorModel model = new ProfessorModel();
        CursoModel modelCurso = new CursoModel();
        EstadoModel modelEstado = new EstadoModel();
        CidadeModel modelCidade = new CidadeModel();


        public CadastroProfessor()
        {
            InitializeComponent();
        }

        #region Habilitar, Desabilitar e Limpar Campos

        public void HabilitarCampos()
        {
            txtNome.Enabled = true;
            cbCurso.Enabled = true;
            cbMateria.Enabled = true;
            Cd.Enabled = true;
            txtSalario.Enabled = true;
            BtnEscolher.Enabled = true;
            txtEmail.Enabled = true;
            txtTelefone.Enabled = true;
            cbEstadoCivil.Enabled = true;
            cbGenero.Enabled = true;
            dtProfessor.Enabled = true;
            txtCEP.Enabled = true;
            cbEstado.Enabled = true;
            cbCidade.Enabled = true;
            txtEndereco.Enabled = true;
            cbIDP.Enabled = true;
        }
        public void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            cbCurso.Enabled = false;
            cbMateria.Enabled = false;
            Cd.Enabled = false;
            txtSalario.Enabled = false;
            BtnEscolher.Enabled = false;
            txtEmail.Enabled = false;
            txtTelefone.Enabled = false;
            cbEstadoCivil.Enabled = false;
            cbGenero.Enabled = false;
            dtProfessor.Enabled = false;
            txtCEP.Enabled = false;
            cbEstado.Enabled = false;
            cbCidade.Enabled = false;
            txtEndereco.Enabled = false;
            cbIDP.Enabled = false;

        }
        public void LimparCampos()
        {
            txtNome.Text = "";
            cbCurso.Text = "";
            cbMateria.Text = "";
            Cd.Text = "";
            txtSalario.Text = "";
            pbFoto.Image = null;
            txtEmail.Text = "";
            txtTelefone.Text = "";
            cbEstadoCivil.Text = "";
            cbGenero.Text = "";
            dtProfessor.Text = "";
            cbCurso.Text = "";
            txtCEP.Text = "";
            cbEstado.Text = "";
            cbCidade.Text = "";
            txtEndereco.Text = "";
            cbIDP.Text = "";

        }
        #endregion

        #region Escolher Foto, Novo, Salvar, Editar, Excluir, Pesquisar

        private void BtnEscolherP_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Image Files | *.jpg; *.jpeg; *.png; ....";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pbFoto.Image = Image.FromFile(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível carregar a imagem: " + ex.Message);
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {

            Professor dados = new Professor();
            Editar(dados);
            LimparCampos();
            DesabilitarCampos();
        }

        private void Editar(Professor dado)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                if (pbFoto.Image != null)
                {
                    pbFoto.Image.Save(ms, pbFoto.Image.RawFormat);
                }
                else
                {
                    MessageBox.Show("Insira uma imagem");
                    return;
                }
                byte[] img = ms.ToArray();

                dado.Id_professor = Convert.ToInt32(cbIDP.SelectedValue);
                dado.Nome = txtNome.Text;
                dado.Curso = cbCurso.Text;
                dado.Materia = cbMateria.Text;
                dado.Salario = txtSalario.Text;
                dado.EstadoCivil = cbEstadoCivil.Text;
                dado.Genero = cbGenero.Text;
                dado.DataNascimento = DateTime.Parse(dtProfessor.Text);
                dado.Email = txtEmail.Text;
                dado.Telefone = txtTelefone.Text;
                dado.Foto = img;
                dado.Cep = txtCEP.Text;
                dado.Estado = cbEstado.Text;
                dado.Cidade = cbCidade.Text;
                dado.Endereco = txtEndereco.Text;


                model.Editar(dado);

                MessageBox.Show("Dados Editados com Sucesso!");
                Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar os dados: " + ex.Message);
            }
        }
        private void MenuAluno_Click(object sender, EventArgs e)
        {
            Visible = false;
            cadAluno.ShowDialog();
            this.Close();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Double value;
            if (Double.TryParse(txtSalario.Text, out value))
                txtSalario.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);


        }

        private void btnNovo_Click(object sender, EventArgs e)
        {

            HabilitarCampos();
            Atualizar();
            btnSalvar.Enabled = true;
            LimparCampos();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            if (Verificacoes.IsValidEmail(txtEmail.Text))
            {

            }
            else
            {
                MessageBox.Show("Email Inválido");
            }


            Professor dados = new Professor();
            Salvar(dados);
            DesabilitarCampos();
            Atualizar();
            LimparCampos();
        }

        private void Salvar(Professor dado)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                if (pbFoto.Image != null)
                {
                    pbFoto.Image.Save(ms, pbFoto.Image.RawFormat);
                }
                else
                {
                    MessageBox.Show("Insira uma imagem");
                    return;
                }
                byte[] img = ms.ToArray();


                dado.Nome = txtNome.Text;
                dado.Curso = cbCurso.Text;
                dado.Materia = cbMateria.Text;
                dado.Salario = txtSalario.Text;
                dado.EstadoCivil = cbEstadoCivil.Text;
                dado.Genero = cbGenero.Text;
                dado.DataNascimento = DateTime.Parse(dtProfessor.Text);
                dado.Email = txtEmail.Text;
                dado.Telefone = txtTelefone.Text;
                dado.Foto = img;
                dado.Id_professor = Convert.ToInt32(Cd.SelectedValue);
                dado.Cep = txtCEP.Text;
                dado.Estado = cbEstado.Text;
                dado.Cidade = cbCidade.Text;
                dado.Endereco = txtEndereco.Text;


                model.Salvar(dado);

                MessageBox.Show("Dados Salvos com Sucesso!");
                Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar os Dadosss: " + ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (cbIDP.Text == "")
            {
                MessageBox.Show("Selecione na tabela um registro para excluir!");
                return;
            }
            if (MessageBox.Show("Deseja Excluir o Cliente?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            Professor dados = new Professor();
            Excluir(dados);
            LimparCampos();
            DesabilitarCampos();
        }

        private void Excluir(Professor dado)
        {
            try
            {
                dado.Id = Convert.ToInt32(cbIDP.Text);

                model.Excluir(dado);
                MessageBox.Show("Dados excluido com Sucesso!");
                Atualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir os Dados: " + ex.Message);
            }
        }

        public void Buscar(Professor dado)
        {
            try
            {
                if (Cd.SelectedValue != null)
                {
                    DataRowView rowView = (DataRowView)Cd.SelectedItem;


                    string cursoNome = Convert.ToString(rowView["nome"]);

                    dado.Curso = cursoNome;

                    DataTable professor = (DataTable)model.Buscar(dado);
                    grid.DataSource = professor;
                }
                else
                {

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        private void CadastroProfessor_Load(object sender, EventArgs e)
        {
            LimparCampos();
            Listar();

            #region CarregarComboBox ID
            Cd.DataSource = modelCurso.Listar();
            Cd.ValueMember = "id";
            Cd.DisplayMember = "nome";
            Cd.DropDownStyle = ComboBoxStyle.DropDown;
            Cd.DropDownHeight = Cd.ItemHeight * 5;
            #endregion

            #region CarregarComboBox Estado e Cidade
            cbEstado.DataSource = modelEstado.Listar();
            cbEstado.ValueMember = "id";
            cbEstado.DisplayMember = "nome";
            cbEstado.DropDownHeight = cbEstado.ItemHeight * 5;

            int estadoId = Convert.ToInt32(cbEstado.SelectedValue);
            DataTable cidadesDoEstado = modelCidade.BuscarCidade(estadoId);
            cbCidade.DataSource = cidadesDoEstado;
            cbCidade.ValueMember = "uf";
            cbCidade.DisplayMember = "nome";

            #endregion

            dtProfessor.CustomFormat = "dd/MM/yyyy";

            #region CarregarComboBox Curso e Materia
            cbCurso.DataSource = modelCurso.Listar();
            cbCurso.DropDownHeight = Cd.ItemHeight * 5;
            cbCurso.ValueMember = "id";
            cbCurso.DisplayMember = "nome";
            cbCurso.SelectedIndex = -1;

            int cursoid = Convert.ToInt32(cbCurso.SelectedValue);
            DataTable materiasDoCurso = modelCurso.BuscarMateria(cursoid);
            cbMateria.DataSource = materiasDoCurso;
            cbMateria.DisplayMember = "nome";
            cbMateria.ValueMember = "MateriaId";
            #endregion

            #region CarregarComboBox ID
            cbIDP.Text = "";
            cbIDP.DataSource = model.Listar();
            cbIDP.DropDownStyle = ComboBoxStyle.DropDownList;
            cbIDP.DropDownHeight = cbIDP.ItemHeight * 5;
            cbIDP.ValueMember = "Id";
            cbIDP.DisplayMember = "Id";
            #endregion

            Atualizar();

            Cd.Text = "";
            Cd.DropDownStyle = ComboBoxStyle.DropDownList;
            Cd.DropDownHeight = Cd.ItemHeight * 5;
            cbMateria.DisplayMember = "nome";
            cbMateria.ValueMember = "MateriaId";
            cbMateria.Text = "";

        }

        #region textChanged
        private void cbID_TextChanged(object sender, EventArgs e)
        {
            Professor dados = new Professor();
            Buscar(dados);

        }

        private void cbCurso_TextChanged(object sender, EventArgs e)
        {
            cbCurso.DisplayMember = "nome";
            cbCurso.ValueMember = "id";
            DataTable materiasDoCurso = modelCurso.BuscarMateria(Convert.ToInt32(cbCurso.SelectedValue));
            cbMateria.DataSource = materiasDoCurso;
        }

        #endregion
        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSalvar.Enabled = false;
            HabilitarCampos();
            cbIDP.Text = grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            cbCurso.Text = grid.CurrentRow.Cells[2].Value.ToString();
            cbMateria.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txtSalario.Text = grid.CurrentRow.Cells[4].Value.ToString();
            cbEstadoCivil.Text = grid.CurrentRow.Cells[5].Value.ToString();
            cbGenero.Text = grid.CurrentRow.Cells[6].Value.ToString();
            if (DateTime.TryParse(grid.CurrentRow.Cells[7].Value.ToString(), out DateTime data))
            {

                dtProfessor.Text = data.ToString("dd/MM/yyyy");
            }
            txtEmail.Text = grid.CurrentRow.Cells[8].Value.ToString();
            txtTelefone.Text = grid.CurrentRow.Cells[9].Value.ToString();

            byte[] img = (byte[])grid.CurrentRow.Cells[10].Value;

            MemoryStream ms = new MemoryStream(img);
            pbFoto.Image = Image.FromStream(ms);


            txtCEP.Text = grid.CurrentRow.Cells[12].Value.ToString();
            cbEstado.Text = grid.CurrentRow.Cells[13].Value.ToString();
            cbCidade.Text = grid.CurrentRow.Cells[14].Value.ToString();
            txtEndereco.Text = grid.CurrentRow.Cells[15].Value.ToString();
        }

        public void Listar()
        {

            grid.DataSource = model.Listar();
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            grid.Columns[0].Visible = false;
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "Curso";
            grid.Columns[3].HeaderText = "Materia";
            grid.Columns[4].HeaderText = "Salario";
            grid.Columns[5].HeaderText = "Estado Civil";
            grid.Columns[6].HeaderText = "Genero";
            grid.Columns[7].HeaderText = "Data de Nascimento";
            grid.Columns[8].HeaderText = "Email";
            grid.Columns[9].HeaderText = "Telefone";
            grid.Columns[10].Visible = false;
            grid.Columns[11].Visible = false;
            grid.Columns[12].Visible = false;
            grid.Columns[13].HeaderText = "Estado";
            grid.Columns[14].HeaderText = "Cidade";
            grid.Columns[15].HeaderText = "Endereço";
        }

        public void Atualizar()
        {
            grid.DataSource = model.Listar();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Cd.Text = "";
            Atualizar();
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            string originalText = txtNome.Text;
            string modifiedText = originalText;

            while (Verificacoes.VerificaNumeroOuCaracterEspecial(modifiedText))
            {
                modifiedText = Regex.Replace(modifiedText, @"\d", "");
                modifiedText = Regex.Replace(modifiedText, @"[^\w\s]", "");

                if (modifiedText == originalText)
                    break;

                originalText = modifiedText;
            }

            txtNome.Text = modifiedText;
            txtNome.SelectionStart = modifiedText.Length;
        }


        private void txtSalario_TextChanged(object sender, EventArgs e)
        {
            string text = txtSalario.Text;
            if (!Verificacoes.Enumero(text))
            {
                text = Regex.Replace(text, @"[^\d]", "");

            }
        }

        private void cbIDP_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            cbEstado.DisplayMember = "nome";
            cbEstado.ValueMember = "id";
            int estadoId = Convert.ToInt32(cbEstado.SelectedValue);
            DataTable cidadesDoEstado = modelCidade.BuscarCidade(estadoId);
            cbCidade.DataSource = cidadesDoEstado;


        }
    }
}
