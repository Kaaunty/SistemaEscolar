using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Cadastro_Escolar.Entidades;
using Cadastro_Escolar.Model;
using MySqlX.XDevAPI;

namespace Cadastro_Escolar.View
{
    public partial class CadastroProfessor : Form
    {
        #region Variaveis
        ProfessorModel model = new ProfessorModel();
        CursoModel modelCurso = new CursoModel();
        EstadoModel modelEstado = new EstadoModel();
        CidadeModel modelCidade = new CidadeModel();
        #endregion

        public CadastroProfessor()
        {
            InitializeComponent();
        }

        private void CadastroProfessor_Load(object sender, EventArgs e)
        {


            #region Carregar Todos Combobox

            #region CarregarComboBox ID
            CBPesquisa.DataSource = modelCurso.Listar();
            CBPesquisa.ValueMember = "id";
            CBPesquisa.DisplayMember = "nome";
            CBPesquisa.DropDownStyle = ComboBoxStyle.DropDown;
            CBPesquisa.DropDownHeight = CBPesquisa.ItemHeight * 5;
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

            #region CarregarComboBox Curso e Materia
            cbCurso.DataSource = modelCurso.Listar();
            cbCurso.DropDownHeight = CBPesquisa.ItemHeight * 5;
            cbCurso.ValueMember = "id";
            cbCurso.DisplayMember = "nome";
            cbCurso.SelectedIndex = -1;

            int cursoid = Convert.ToInt32(cbCurso.SelectedValue);
            DataTable materiasDoCurso = modelCurso.BuscarMateria(cursoid);
            cbMateria.DataSource = materiasDoCurso;
            cbMateria.DisplayMember = "nome";
            cbMateria.ValueMember = "MateriaId";
            cbMateria.SelectedIndex = -1;
            #endregion

            #region CarregarComboBox Pesquisa
            CBPesquisa.SelectedIndex = -1;
            CBPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
            CBPesquisa.DropDownHeight = CBPesquisa.ItemHeight * 5;
            cbMateria.DisplayMember = "nome";
            cbMateria.ValueMember = "MateriaId";
            cbMateria.SelectedIndex = -1;
            #endregion

            #endregion

            #region Data Nascimento Professor
            dtProfessor.CustomFormat = "dd/MM/yyyy";
            dtProfessor.MinDate = new DateTime(1953, 01, 01);
            dtProfessor.MaxDate = new DateTime(2000, 12, 31);
            dtProfessor.Format = DateTimePickerFormat.Custom;
            #endregion

            Listar();
        }

        #region Habilitar, Desabilitar e Limpar Campos

        public void HabilitarCampos()
        {
            txtNome.Enabled = true;
            cbCurso.Enabled = true;
            cbMateria.Enabled = true;
            CBPesquisa.Enabled = true;
            txtSalario.Enabled = true;
            btnEscolher.Enabled = true;
            txtEmail.Enabled = true;
            txtTelefone.Enabled = true;
            cbEstadoCivil.Enabled = true;
            cbGenero.Enabled = true;
            dtProfessor.Enabled = true;
            txtCEP.Enabled = true;
            cbEstado.Enabled = true;
            cbCidade.Enabled = true;
            txtEndereco.Enabled = true;
            txtID.Enabled = true;
        }
        public void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            cbCurso.Enabled = false;
            cbMateria.Enabled = false;
            CBPesquisa.Enabled = false;
            txtSalario.Enabled = false;
            btnEscolher.Enabled = false;
            txtEmail.Enabled = false;
            txtTelefone.Enabled = false;
            cbEstadoCivil.Enabled = false;
            cbGenero.Enabled = false;
            dtProfessor.Enabled = false;
            txtCEP.Enabled = false;
            cbEstado.Enabled = false;
            cbCidade.Enabled = false;
            txtEndereco.Enabled = false;
            txtID.Enabled = false;


        }
        public void LimparCampos()
        {
            txtNome.Text = "";
            cbCurso.SelectedIndex = -1;
            cbMateria.SelectedIndex = -1;
            txtSalario.Text = "";
            pbFoto.Image = null;
            txtEmail.Text = "";
            txtTelefone.Text = "";
            cbEstadoCivil.SelectedIndex = -1;
            cbGenero.SelectedIndex = -1;
            dtProfessor.Text = "";
            cbCurso.SelectedIndex = -1;
            txtCEP.Text = "";
            cbEstado.SelectedIndex = -1;
            cbCidade.SelectedIndex = -1;
            txtEndereco.Text = "";
            txtID.Text = "";

        }
        #endregion

        #region Escolher Foto, Novo, Salvar, Editar, Excluir, Pesquisar, Atualizar, Cancelar

        private void BtnEscolherP_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Image Files | *.jpg; *.jpeg; *.png; *.gif; ....";
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = false;
            HabilitarCampos();
            LimparCampos();
            Listar();
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = true;
            Professor dados = new Professor();
            Salvar(dados);
        }
        private void Salvar(Professor dado)
        {

            if (VerificarEspaços())
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    if (pbFoto.Image != null)
                    {
                        pbFoto.Image.Save(ms, pbFoto.Image.RawFormat);
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
                    dado.Id_professor = Convert.ToInt32(CBPesquisa.SelectedValue);
                    dado.Cep = txtCEP.Text;
                    dado.Estado = cbEstado.Text;
                    dado.Cidade = cbCidade.Text;
                    dado.Endereco = txtEndereco.Text;


                    model.Salvar(dado);

                    MessageBox.Show("Dados Salvos com Sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar os Dadosss: " + ex.Message);
                }
                finally
                {
                    LimparCampos();
                    DesabilitarCampos();
                    Listar();
                }
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Professor dados = new Professor();
            Editar(dados);

        }
        private void Editar(Professor dado)
        {
            if (VerificarEspaços())
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

                    dado.Id_professor = Convert.ToInt32(txtID.Text);
                    dado.Nome = txtNome.Text;
                    dado.Curso = cbCurso.Text;
                    dado.Materia = cbMateria.Text;
                    dado.Salario = txtSalario.Text;
                    dado.EstadoCivil = cbEstadoCivil.Text;
                    dado.Genero = cbGenero.Text;
                    dado.DataNascimento = DateTime.Parse(dtProfessor.Text);
                    if (Verificacoes.IsValidEmail(txtEmail.Text))
                    {
                        dado.Email = txtEmail.Text;
                    }
                    else
                    {
                        MessageBox.Show("Email Inválido");
                        txtEmail.Text = "";
                        txtEmail.Focus();
                        return;
                    }
                    dado.Telefone = txtTelefone.Text;
                    dado.Foto = img;
                    dado.Cep = txtCEP.Text;
                    dado.Estado = cbEstado.Text;
                    dado.Cidade = cbCidade.Text;
                    dado.Endereco = txtEndereco.Text;


                    model.Editar(dado);

                    MessageBox.Show("Dados Editados com Sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao editar os dados: " + ex.Message);
                }
                finally
                {
                    LimparCampos();
                    DesabilitarCampos();
                    Listar();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

            Professor dados = new Professor();
            Excluir(dados);

        }

        private void Excluir(Professor dado)
        {
            try
            {
                if (txtID.Text == "")
                {
                    MessageBox.Show("Selecione na tabela um registro para excluir!");
                    return;
                }
                if (MessageBox.Show("Deseja Excluir o Cliente?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                else
                {
                    dado.Id_professor = Convert.ToInt32(grid.CurrentRow.Cells[0].Value.ToString());
                    model.Excluir(dado);
                    MessageBox.Show("Dados excluido com Sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir os Dados: " + ex.Message);
            }
            finally
            {
                LimparCampos();
                DesabilitarCampos();
                Listar();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            LimparCampos();
            HabilitarCampos();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CBPesquisa.SelectedIndex = -1;
            Listar();
        }

        public void Buscar(Professor dado)
        {
            try
            {
                if (CBPesquisa.SelectedValue != null)
                {
                    DataRowView rowView = (DataRowView)CBPesquisa.SelectedItem;


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

        #region Grid Professor
        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = true;
            btnCancelar.Enabled = true;
            HabilitarCampos();
            txtID.Text = grid.CurrentRow.Cells[0].Value.ToString();
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
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grid.BorderStyle = BorderStyle.Fixed3D;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            grid.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            grid.BackgroundColor = Color.White;
            grid.Columns[0].Visible = false;
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "Curso";
            grid.Columns[3].HeaderText = "Materia";
            grid.Columns[4].HeaderText = "Salario";
            grid.Columns[5].Visible = false;
            grid.Columns[6].Visible = false;
            grid.Columns[7].Visible = false;
            grid.Columns[8].Visible = false;
            grid.Columns[9].Visible = false;
            grid.Columns[10].Visible = false;
            grid.Columns[11].Visible = false;
            grid.Columns[12].Visible = false;
            grid.Columns[13].Visible = false;
            grid.Columns[14].Visible = false;
            grid.Columns[15].Visible = false;
        }

        #endregion

        #region Validações dos textos e dos checkbox
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

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            string originalText = txtNome.Text;
            string modifiedText = originalText;

            while (Verificacoes.VerificaNumeroOuCaracterEspecial(modifiedText))
            {
                modifiedText = Regex.Replace(modifiedText, @"\d", "");
                modifiedText = Regex.Replace(modifiedText, @"[^\w\s]", "");
                modifiedText = modifiedText.Replace("_", "");

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

        private void txtSalario_Leave(object sender, EventArgs e)
        {
            Double value;

            if (Double.TryParse(txtSalario.Text, out value))
            {
                txtSalario.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);

                if (value <= 2000)
                {
                    MessageBox.Show("Salário Inválido (Menor que R$ 2.000)");
                    txtSalario.Text = "";
                    txtSalario.Focus();
                    return;
                }
                if (value >= 10001)
                {
                    MessageBox.Show("Salário Inválido (Maior que R$ 10.000)");
                    txtSalario.Text = "";
                    txtSalario.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Digite um valor numérico válido.");
                txtSalario.Text = "";
                txtSalario.Focus();
            }
        }

        private void cbEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            cbEstado.DisplayMember = "nome";
            cbEstado.ValueMember = "id";
            int estadoId = Convert.ToInt32(cbEstado.SelectedValue);
            DataTable cidadesDoEstado = modelCidade.BuscarCidade(estadoId);
            cbCidade.DataSource = cidadesDoEstado;
        }

        private bool VerificarEspaços()
        {
            if (pbFoto.Image == null)
            {
                MessageBox.Show("Selecione uma foto");
                return false;
            }
            if (txtNome.Text == "")
            {
                MessageBox.Show("Preencha o campo Nome");
                txtNome.Focus();
                return false;
            }
            if (cbCurso.SelectedIndex == -1)
            {
                MessageBox.Show("Preencha o campo Curso");
                cbCurso.Focus();
                return false;
            }
            if (cbCurso.Text == "")
            {
                MessageBox.Show("Preencha o campo Curso");
                cbCurso.Focus();
                return false;
            }
            if (cbMateria.SelectedIndex == -1)
            {
                MessageBox.Show("Preencha o campo Materia");
                cbMateria.Focus();
                return false;
            }
            if (cbMateria.Text == "")
            {
                MessageBox.Show("Preencha o campo Materia");
                cbMateria.Focus();
                return false;
            }
            if (cbGenero.SelectedIndex == -1)
            {
                MessageBox.Show("Preencha o campo Genero");
                cbGenero.Focus();
                return false;
            }
            if (cbGenero.Text == "")
            {
                MessageBox.Show("Preencha o campo Genero");
                cbGenero.Focus();
                return false;
            }
            if (cbEstadoCivil.SelectedIndex == -1)
            {
                MessageBox.Show("Preencha o campo Estado Civil");
                cbEstadoCivil.Focus();
                return false;
            }
            if (cbEstadoCivil.Text == "")
            {
                MessageBox.Show("Preencha o campo Estado Civil");
                cbEstadoCivil.Focus();
                return false;
            }
            if (txtNome.Text == "")
            {
                MessageBox.Show("Preencha o campo Nome");
                txtNome.Focus();
                return false;
            }
            if (txtSalario.Text == "")
            {
                MessageBox.Show("Preencha o campo Salario");
                txtSalario.Focus();
                return false;
            }
            if (Verificacoes.IsValidEmail(txtEmail.Text))
            {

            }
            else
            {
                MessageBox.Show("Email Inválido");
                txtEmail.Text = "";
                txtEmail.Focus();
                return false;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Preencha o campo Email");
                txtEmail.Focus();
                return false;
            }
            if (txtTelefone.TextLength < 14)
            {
                MessageBox.Show("Preencha o campo Telefone");
                txtTelefone.Focus();
                return false;
            }
            if (txtTelefone.Text == "(  )      -")
            {
                MessageBox.Show("Preencha o campo Telefone");
                txtTelefone.Focus();
                return false;
            }
            if (txtCEP.TextLength < 9)
            {
                MessageBox.Show("Preencha o campo CEP");
                txtCEP.Focus();
                return false;
            }
            if (txtCEP.Text == "     -")
            {
                MessageBox.Show("Preencha o campo CEP");
                txtCEP.Focus();
                return false;
            }
            if (cbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Preencha o campo Estado");
                cbEstado.Focus();
                return false;
            }
            if (cbEstado.Text == "")
            {
                MessageBox.Show("Preencha o campo Estado");
                cbEstado.Focus();
                return false;
            }
            if (cbCidade.SelectedIndex == -1)
            {
                MessageBox.Show("Preencha o campo Cidade");
                cbCidade.Focus();
                return false;
            }
            if (cbCidade.Text == "")
            {
                MessageBox.Show("Preencha o campo Cidade");
                cbCidade.Focus();
                return false;
            }
            if (txtEndereco.Text == "")
            {
                MessageBox.Show("Preencha o campo Endereço");
                txtEndereco.Focus();
                return false;
            }
            return true;

        }

        #endregion
    }
}
