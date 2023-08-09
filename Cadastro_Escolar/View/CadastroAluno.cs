using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Cadastro_Escolar.Entidades;
using Cadastro_Escolar.Model;
using Google.Protobuf.WellKnownTypes;

namespace Cadastro_Escolar
{
    public partial class CadastroAluno : Form
    {
        #region Argumentos

        AlunoModel model = new AlunoModel();
        Random raRandom = new Random();
        CursoModel modelCurso = new CursoModel();
        EstadoModel modelEstado = new EstadoModel();
        CidadeModel modelCidade = new CidadeModel();
        #endregion

        private void CadastroAluno_Load(object sender, EventArgs e)
        {
            Listar();

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

            dtAluno.MinDate = DateTime.Now.AddYears(-60);
            dtAluno.MaxDate = DateTime.Now.AddYears(-18);


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

        public CadastroAluno()
        {
            InitializeComponent();
        }
        #region Escolher Foto ,Novo, Salvar, Editar, Excluir, Pesquisar
        private void btnEscolherFoto_Click(object sender, EventArgs e)
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
        private void txtMensalidade_Leave(object sender, EventArgs e)
        {
            Double value;
            if (Double.TryParse(txtMensalidade.Text, out value))
                txtMensalidade.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:C2}", value);
            else
                txtMensalidade.Text = String.Empty;
        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            HabilitarCampos();
            LimparCampos();
            GerarRA();
            Listar();
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Aluno dados = new Aluno();
            Salvar(dados);
        }

        private void Salvar(Aluno dado)
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


                VerificaCampos();

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
                dado.Email = txtEmail.Text;
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
                LimparCampos();
                DesabilitarCampos();
                Listar();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao salvar os Dados: Preencha todos os campos");
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtRA.Text == "")
            {
                MessageBox.Show("Selecione na tabela um registro para editar!");
                return;
            }
            Aluno dados = new Aluno();
            Editar(dados);
            LimparCampos();
            DesabilitarCampos();
            Listar();
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

                VerificaCampos();

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
                dado.Email = txtEmail.Text;
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
        }

        private void btnExcluir_Click(object sender, EventArgs e)
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
                Aluno dados = new Aluno();
                Excluir(dados);
                LimparCampos();
                DesabilitarCampos();
                Listar();
            }
        }
        private void Excluir(Aluno dado)
        {
            try
            {
                dado.Ra = Convert.ToInt32(gridAluno.CurrentRow.Cells[1].Value.ToString());
                model.Excluir(dado);
                MessageBox.Show("Dados excluido com Sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir os Dados: " + ex.Message);
            }
        }
        #endregion

        private void GerarRA()
        {
            raRandom = new Random();
            int ra = raRandom.Next(1, 2000);
            string rareal = ra.ToString();
            txtRA.Text = rareal;



        }

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

        public void Listar()
        {

            gridAluno.DataSource = model.Listar();
            gridAluno.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridAluno.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
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


        #region TextChanged's
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

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void gridAluno_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //btnSalvar.Enabled = false;
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
                pbFoto.Image = Image.FromStream(ms);
            }



            txtCEP.Text = gridAluno.CurrentRow.Cells[14].Value.ToString();
            cbEstado.Text = gridAluno.CurrentRow.Cells[15].Value.ToString();
            cbCidade.Text = gridAluno.CurrentRow.Cells[16].Value.ToString();

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


        #endregion

        private void txtEmail_Leave(object sender, EventArgs e)
        {

        }
    }
}
