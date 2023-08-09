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
using GUI_V_2;
using Cadastro_Escolar.Model;
using Cadastro_Escolar.Entidades;
using System.IO;
using System.Windows.Input;

namespace FlatLoginWatermark
{
    public partial class FormLogin : Form
    {
        LoginModel model = new LoginModel();
        public FormLogin()
        {
            InitializeComponent();
        }

        public void Limpar()
        {
            txtuser.Text = "";
            txtpass.Text = "";

        }

        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text == "Usuario")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.LightGray;
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                txtuser.Text = "Usuario";
                txtuser.ForeColor = Color.Silver;
            }
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "Senha")
            {
                txtpass.Text = "";
                txtpass.ForeColor = Color.LightGray;
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "Senha";
                txtpass.ForeColor = Color.Silver;
                txtpass.UseSystemPasswordChar = false;
            }
        }


        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {

            Login dados = new Login();
            Logar(dados);

        }

        private void Logar(Login dado)
        {
            try
            {
                if (txtuser.Text == "" || txtpass.Text == "")
                {
                    lblMensagem.Text = "Preencha os campos";
                    return;
                }

                dado.Usuario = txtuser.Text;
                dado.Senha = txtpass.Text;


                dado = model.Logar(dado);

                if (dado.Usuario == null)
                {
                    lblMensagem.Text = "Preencha os campos";
                    lblMensagem.ForeColor = Color.Red;
                    Limpar();
                    txtuser.Focus();
                    return;
                }

                MenuPrincipal menu = new MenuPrincipal();
                this.Hide();
                menu.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Logar" + ex.Message);
                Limpar();
                txtuser.Focus();
            }



        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

            lblMensagem.Text = "";
        }

        private void FormLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            Keys key = (Keys)e.KeyChar;
            if (key == Keys.Enter)
            {
                btnlogin.PerformClick();
            }
        }
    }
}
