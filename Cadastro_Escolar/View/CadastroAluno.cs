using Cadastro_Escolar.Entidades;
using Cadastro_Escolar.Model;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Cadastro_Escolar
{
    public partial class CadastroAluno : Form
    {
        #region Variaveis
        AlunoModel model = new AlunoModel();
        Random raRandom = new Random();
        CursoModel modelCurso = new CursoModel();
        EstadoModel modelEstado = new EstadoModel();
        CidadeModel modelCidade = new CidadeModel();
        #endregion

        public CadastroAluno()
        {
            InitializeComponent();
        }


        private void CadastroAluno_Load(object sender, EventArgs e)
        {
            Listar();

            #region Carregar Todos Combobox

            #region Carregar ComboBox Estado e Cidade
            cbEstado.DataSource = modelEstado.Listar();
            cbEstado.DisplayMember = "nome";
            cbEstado.ValueMember = "id";
            cbEstado.DropDownHeight = cbEstado.ItemHeight * 5;
            cbEstado.SelectedIndex = -1;

            int estadoId = Convert.ToInt32(cbEstado.SelectedValue);
            DataTable cidadesDoEstado = modelCidade.BuscarCidade(estadoId);
            cbCidade.DataSource = cidadesDoEstado;
            cbCidade.ValueMember = "uf";
            cbCidade.DisplayMember = "nome";
            #endregion

            #region CarregarComboBox Curso e Materia
            cbCurso.DataSource = modelCurso.Listar();
            cbCurso.DisplayMember = "nome";
            cbCurso.ValueMember = "id";
            cbCurso.DropDownHeight = cbCurso.ItemHeight * 5;
            cbCurso.SelectedIndex = -1;

            int cursoId = Convert.ToInt32(cbCurso.SelectedValue);
            DataTable materiasDoCurso = modelCurso.BuscarMateria(cursoId);
            cbMateria.DataSource = materiasDoCurso;
            cbMateria.ValueMember = "MateriaId";
            cbMateria.DisplayMember = "nome";
            #endregion

            #endregion

            #region Data Nascimento Aluno
            dtAluno.CustomFormat = "dd/MM/yyyy";
            dtAluno.MaxDate = new DateTime(2004, 12, 31);
            dtAluno.MinDate = new DateTime(1953, 1, 1);
            dtAluno.Format = DateTimePickerFormat.Custom;
            #endregion
        }

        #region Habilitar, Desabilitar , Limpar, Verifica Campos

        public void HabilitarCampos()
        {
            txtNome.Enabled = true;
            cbCurso.Enabled = true;
            cbMateria.Enabled = true;
            txtMensalidade.Enabled = true;
            cbEstadoCivil.Enabled = true;
            cbGenero.Enabled = true;
            dtAluno.Enabled = true;
            txtCEP.Enabled = true;
            cbEstado.Enabled = true;
            cbCidade.Enabled = true;
            txtEndereco.Enabled = true;
            txtEmail.Enabled = true;
            txtTelefone.Enabled = true;
            txtRA.Enabled = true;
            cbSim.Enabled = true;
            BtnEscolher.Enabled = true;
        }
        public void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            cbCurso.Enabled = false;
            cbMateria.Enabled = false;
            txtMensalidade.Enabled = false;
            cbEstadoCivil.Enabled = false;
            cbGenero.Enabled = false;
            dtAluno.Enabled = false;
            txtCEP.Enabled = false;
            cbEstado.Enabled = false;
            cbCidade.Enabled = false;
            txtEndereco.Enabled = false;
            txtEmail.Enabled = false;
            txtTelefone.Enabled = false;
            txtRA.Enabled = false;
            cbSim.Enabled = false;
            BtnEscolher.Enabled = false;
        }
        public void LimparCampos()
        {
            txtNome.Text = "";
            cbCurso.SelectedIndex = -1;
            cbMateria.SelectedIndex = -1;
            txtMensalidade.Text = "";
            cbEstadoCivil.SelectedIndex = -1;
            cbGenero.SelectedIndex = -1;
            dtAluno.Text = "";
            txtCEP.Text = "";
            cbEstado.SelectedIndex = -1;
            cbCidade.SelectedIndex = -1;
            txtEndereco.Text = "";
            txtEmail.Text = "";
            txtTelefone.Text = "";
            txtRA.Text = "";
            cbSim.Checked = false;
            pbFoto.Image = null;
        }

        public void VerificaCampos()
        {

            bool txtnome = string.IsNullOrEmpty(txtNome.Text);
            bool txtcep = string.IsNullOrEmpty(txtCEP.Text);
            bool txtemail = string.IsNullOrEmpty(txtCEP.Text);
            bool txtendereco = string.IsNullOrEmpty(txtEndereco.Text);
            bool cbestado = string.IsNullOrEmpty(cbEstado.Text);
            bool cbcurso = string.IsNullOrEmpty(cbCurso.Text);
            bool txtmensalidade = string.IsNullOrEmpty(txtMensalidade.Text);

            if (!txtnome && txtcep && txtemail && txtendereco && cbestado && cbcurso && txtmensalidade)
            {
                MessageBox.Show("Preencha todos os campos");
            }

            if (cbSim.Checked == false)
            {
                while (txtMensalidade.ToString() == "0")
                {
                    MessageBox.Show("Preencha o campo Mensalidade");
                }
            }
            if (cbSim.Checked == true)
            {
                txtMensalidade.Text = "0";
            }



        }

        #endregion

        #region Escolher Foto ,Novo, Salvar, Editar, Excluir, Cancelar, Atualizar
        private void btnEscolherFoto_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Image Files | *.jpg; *.jpeg; *.png; ....";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pbFoto.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
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
            btnCancelar.Enabled = true;
            HabilitarCampos();
            LimparCampos();
            GerarRA();
            Listar();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = true;
            Aluno dados = new Aluno();
            Salvar(dados);
        }

        private void Salvar(Aluno dado)
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
                    dado.Nome = txtNome.Text;
                    dado.Curso = cbCurso.Text;
                    dado.Materia = cbMateria.Text;
                    dado.Mensalidade = txtMensalidade.Text;
                    dado.EstadoCivil = cbEstadoCivil.Text;
                    dado.Genero = cbGenero.Text;
                    dado.DataNascimento = DateTime.Parse(dtAluno.Text);
                    dado.Cep = txtCEP.Text;
                    dado.Estado = cbEstado.Text;
                    dado.Cidade = cbCidade.Text;
                    dado.Endereco = txtEndereco.Text;
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
                    dado.Ra = Convert.ToInt32(txtRA.Text);
                    if (cbSim.Checked)
                    {
                        dado.Bolsista = "Sim";

                    }
                    else
                    {
                        dado.Bolsista = "Não";
                    }
                    dado.Foto = img;

                    model.Salvar(dado);

                    MessageBox.Show("Dados Salvos com Sucesso!");
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao salvar os Dados: Preencha todos os campos");
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
            Aluno dados = new Aluno();
            Editar(dados);
        }

        private void Editar(Aluno dado)
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

                dado.Ra = Convert.ToInt32(txtRA.Text);
                dado.Nome = txtNome.Text;
                dado.Curso = cbCurso.Text;
                dado.Materia = cbMateria.Text;
                dado.Mensalidade = txtMensalidade.Text;
                dado.EstadoCivil = cbEstadoCivil.Text;
                dado.Genero = cbGenero.Text;
                dado.DataNascimento = DateTime.Parse(dtAluno.Text);
                dado.Cep = txtCEP.Text;
                dado.Estado = cbEstado.Text;
                dado.Cidade = cbCidade.Text;
                dado.Endereco = txtEndereco.Text;
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
                if (cbSim.Checked)
                {
                    dado.Bolsista = "Sim";

                }
                else
                {
                    dado.Bolsista = "Não";
                }
                dado.Foto = img;

                model.Editar(dado);

                MessageBox.Show("Dados Editado com Sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar os Dados: " + ex.Message);
            }
            finally
            {
                LimparCampos();
                DesabilitarCampos();
                Listar();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Aluno dados = new Aluno();
            Excluir(dados);
        }

        private void Excluir(Aluno dado)
        {
            try
            {
                if (txtRA.Text == "")
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
                    dado.Ra = Convert.ToInt32(gridAluno.CurrentRow.Cells[1].Value.ToString());
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
            Listar();
        }
        #endregion

        #region Grid Aluno
        private void gridAluno_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = true;
            btnCancelar.Enabled = true;
            HabilitarCampos();
            txtRA.Text = gridAluno.CurrentRow.Cells[1].Value.ToString();
            txtNome.Text = gridAluno.CurrentRow.Cells[2].Value.ToString();
            cbCurso.Text = gridAluno.CurrentRow.Cells[3].Value.ToString();
            cbMateria.Text = gridAluno.CurrentRow.Cells[4].Value.ToString();
            if (gridAluno.CurrentRow.Cells[5].Value.ToString() == "Sim")
            {
                cbSim.Checked = true;
            }
            txtMensalidade.Text = gridAluno.CurrentRow.Cells[6].Value.ToString();
            cbEstadoCivil.Text = gridAluno.CurrentRow.Cells[7].Value.ToString();
            cbGenero.Text = gridAluno.CurrentRow.Cells[8].Value.ToString();
            if (DateTime.TryParse(gridAluno.CurrentRow.Cells[9].Value.ToString(), out DateTime data))
            {

                dtAluno.Text = data.ToString("dd/MM/yyyy");
            }
            txtEndereco.Text = gridAluno.CurrentRow.Cells[10].Value.ToString();
            txtEmail.Text = gridAluno.CurrentRow.Cells[11].Value.ToString();
            txtTelefone.Text = gridAluno.CurrentRow.Cells[12].Value.ToString();
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
            txtCEP.Text = gridAluno.CurrentRow.Cells[14].Value.ToString();
            cbEstado.Text = gridAluno.CurrentRow.Cells[15].Value.ToString();
            cbCidade.Text = gridAluno.CurrentRow.Cells[16].Value.ToString();
        }

        public void Listar()
        {
            gridAluno.DataSource = model.Listar();
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
            gridAluno.Columns[0].Visible = false;
            gridAluno.Columns[1].HeaderText = "RA";
            gridAluno.Columns[2].HeaderText = "Nome";
            gridAluno.Columns[3].HeaderText = "Curso";
            gridAluno.Columns[4].HeaderText = "Materia";
            gridAluno.Columns[5].Visible = false;
            gridAluno.Columns[6].Visible = false;
            gridAluno.Columns[7].Visible = false;
            gridAluno.Columns[8].Visible = false;
            gridAluno.Columns[9].Visible = false;
            gridAluno.Columns[10].Visible = false;
            gridAluno.Columns[11].Visible = false;
            gridAluno.Columns[12].Visible = false;
            gridAluno.Columns[13].Visible = false;
            gridAluno.Columns[14].Visible = false;
            gridAluno.Columns[15].Visible = false;
            gridAluno.Columns[16].Visible = false;
        }

        private void GerarRA()
        {
            raRandom = new Random();
            int ra = raRandom.Next(1, 2000);
            string rareal = ra.ToString();
            txtRA.Text = rareal;
        }
        #endregion

        #region Validações dos textos e dos checkbox

        private void cbSim_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSim.Checked == true)
            {
                txtMensalidade.Enabled = false;
            }
            else
            {
                txtMensalidade.Enabled = true;
            }
        }

        private void txtMensalidade_Leave(object sender, EventArgs e)
        {
            Double value;
            if (Double.TryParse(txtMensalidade.Text, out value))
            {
                txtMensalidade.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
                if (value <= 499)
                {
                    MessageBox.Show("Mensalidade Inválida (Menor que R$ 500)");
                    txtMensalidade.Text = "";
                    txtMensalidade.Focus();
                    return;
                }
                if (value >= 10000)
                {
                    MessageBox.Show("Mensalidade Inválida (Maior que R$ 10.000)");
                    txtMensalidade.Text = "";
                    txtMensalidade.Focus();
                    return;

                }
            }

            else
                txtMensalidade.Text = String.Empty;
        }

        private void cbCurso_TextChanged(object sender, EventArgs e)
        {
            cbCurso.DisplayMember = "nome";
            cbCurso.ValueMember = "id";
            DataTable materiasDoCurso = modelCurso.BuscarMateria(Convert.ToInt32(cbCurso.SelectedValue));
            cbMateria.DataSource = materiasDoCurso;
        }

        private void cbEstado_TextChanged(object sender, EventArgs e)
        {
            cbEstado.DisplayMember = "nome";
            cbEstado.ValueMember = "id";
            int estadoId = Convert.ToInt32(cbEstado.SelectedValue);
            DataTable cidadesDoEstado = modelCidade.BuscarCidade(estadoId);
            cbCidade.DataSource = cidadesDoEstado;

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
            if (txtMensalidade.Text == "" && cbSim.Checked == false)
            {
                MessageBox.Show("Preencha o campo Mensalidade");
                txtMensalidade.Focus();
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
            if (txtRA.Text == "")
            {
                MessageBox.Show("Selecione na tabela um registro para editar!");
                txtRA.Focus();
                return false;
            }
            return true;

        }

        #endregion

    }
}
