using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Cadastro_Escolar.View;

namespace Cadastro_Escolar
{
    public partial class TelaPrincipal : Form
    {
        CadastroAluno cadAluno = new CadastroAluno();
        CadastroProfessor cadProfessor = new CadastroProfessor();

        public TelaPrincipal()
        {
            InitializeComponent();

        }

        private void SobreClick(object sender, EventArgs e)
        {
            //MessageBox.Show("Sistema de Cadastro Escolar\nVersão 1.0\nDesenvolvido por: Kauã Freitas\nE-mail: ------------", "Sobre", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void VisualizarProfessorClick(object sender, EventArgs e)
        {
            Visible = false;

            this.Close();
        }

        private void AdicionarProfessorClick(object sender, EventArgs e)
        {
            Visible = false;
            cadProfessor.ShowDialog();
            this.Close();
        }

        private void VisualizarAlunoClick(object sender, EventArgs e)
        {
            Visible = false;

            this.Close();
        }

        private void AdicionarAlunoClick(object sender, EventArgs e)
        {
            Visible = false;

            cadAluno.ShowDialog();
            this.Close();
        }

        private void TelaPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void btnCadastrarAluno_Click(object sender, EventArgs e)
        {
            AbrirFormnoPainel(new CadastroAluno());
            btnCadastrarAluno.Enabled = false;
            btnCadastrarAluno.BackColor = Color.FromArgb(12, 61, 92);
        }
        private void AbrirFormnoPainel(object formf)
        {
            if (this.painelConteudo.Controls.Count > 0)
                this.painelConteudo.Controls.RemoveAt(0);
            Form fh = formf as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.painelConteudo.Controls.Add(fh);
            this.painelConteudo.Tag = fh;
            fh.Show();
        }




    }
}
