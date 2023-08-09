using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Cadastro_Escolar;
using Cadastro_Escolar.View;

namespace GUI_V_2
{
    public partial class MenuPrincipal : Form
    {
        CadastroAluno cad = new CadastroAluno();
        public MenuPrincipal()
        {
            InitializeComponent();
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 250)
            {
                MenuVertical.Width = 70;
            }
            else
                MenuVertical.Width = 250;
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconmaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }

        private void iconrestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;
        }

        private void iconminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void AbrirFormEnPanel(object Formhijo)
        {
            if (this.painelConteudo.Controls.Count > 0)
                this.painelConteudo.Controls.RemoveAt(0);
            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.painelConteudo.Controls.Add(fh);
            this.painelConteudo.Tag = fh;
            fh.Show();
        }

        private void btnCadastrarAluno_click(object sender, EventArgs e)
        {
            AbrirFormnoPainel(new CadastroAluno());
            //
            btnCadastrarProfessor.Enabled = true;
            btnCadastrarProfessor.BackColor = Color.FromArgb(39, 57, 80);
            //
            btnVisualizarAluno.Enabled = true;
            btnVisualizarAluno.BackColor = Color.FromArgb(39, 57, 80);
            //
            btnCadastrarNotas.Enabled = true;
            btnCadastrarNotas.BackColor = Color.FromArgb(39, 57, 80);
            btnVisualizarNotas.Enabled = true;
            btnVisualizarNotas.BackColor = Color.FromArgb(39, 57, 80);
            //
            btnCadastrarAluno.Enabled = false;
            btnCadastrarAluno.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnlogoInicio_Click(object sender, EventArgs e)
        {
            // AbrirFormEnPanel(new ());

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

        private void button3_Click(object sender, EventArgs e)
        {
            AbrirFormnoPainel(new CadastroProfessor());
            //
            btnCadastrarAluno.Enabled = true;
            btnCadastrarAluno.BackColor = Color.FromArgb(39, 57, 80);
            btnVisualizarAluno.Enabled = true;
            btnVisualizarAluno.BackColor = Color.FromArgb(39, 57, 80);
            btnCadastrarNotas.Enabled = true;
            btnCadastrarNotas.BackColor = Color.FromArgb(39, 57, 80);
            btnVisualizarNotas.Enabled = true;
            btnVisualizarNotas.BackColor = Color.FromArgb(39, 57, 80);
            //
            btnCadastrarProfessor.Enabled = false;
            btnCadastrarProfessor.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormnoPainel(new VisualizarAluno());
            //
            btnCadastrarAluno.Enabled = true;
            btnCadastrarAluno.BackColor = Color.FromArgb(39, 57, 80);
            btnCadastrarProfessor.Enabled = true;
            btnCadastrarProfessor.BackColor = Color.FromArgb(39, 57, 80);
            btnCadastrarNotas.Enabled = true;
            btnCadastrarNotas.BackColor = Color.FromArgb(39, 57, 80);
            btnVisualizarNotas.Enabled = true;
            btnVisualizarNotas.BackColor = Color.FromArgb(39, 57, 80);
            //
            btnVisualizarAluno.Enabled = false;
            btnVisualizarAluno.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new CadastroNotas());
            //
            btnCadastrarAluno.Enabled = true;
            btnCadastrarAluno.BackColor = Color.FromArgb(39, 57, 80);
            btnCadastrarProfessor.Enabled = true;
            btnCadastrarProfessor.BackColor = Color.FromArgb(39, 57, 80);
            btnVisualizarAluno.Enabled = true;
            btnVisualizarAluno.BackColor = Color.FromArgb(39, 57, 80);
            btnVisualizarNotas.Enabled = true;
            btnVisualizarNotas.BackColor = Color.FromArgb(39, 57, 80);
            //
            btnCadastrarNotas.Enabled = false;
            btnCadastrarNotas.BackColor = Color.FromArgb(12, 61, 92);


        }

        private void btnVisualizarNotas_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new VisualizarNotas());
            //
            btnCadastrarAluno.Enabled = true;
            btnCadastrarAluno.BackColor = Color.FromArgb(39, 57, 80);
            btnCadastrarProfessor.Enabled = true;
            btnCadastrarProfessor.BackColor = Color.FromArgb(39, 57, 80);
            btnVisualizarAluno.Enabled = true;
            btnVisualizarAluno.BackColor = Color.FromArgb(39, 57, 80);
            btnCadastrarNotas.Enabled = true;
            btnCadastrarNotas.BackColor = Color.FromArgb(39, 57, 80);
            //
            btnVisualizarNotas.Enabled = false;
            btnVisualizarNotas.BackColor = Color.FromArgb(12, 61, 92);

        }
    }
}
