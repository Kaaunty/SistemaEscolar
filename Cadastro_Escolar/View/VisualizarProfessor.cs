using Cadastro_Escolar.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Cadastro_Escolar.View
{
    public partial class VisualizarProfessor : Form
    {
        ProfessorModel model = new ProfessorModel();
        public VisualizarProfessor()
        {
            InitializeComponent();
        }

        public void Listar()
        {

            gridProfessor.DataSource = model.Listar();
            gridProfessor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            gridProfessor.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            gridProfessor.Columns[0].Visible = false;
            gridProfessor.Columns[1].HeaderText = "Nome";
            gridProfessor.Columns[2].HeaderText = "Curso";
            gridProfessor.Columns[3].HeaderText = "Materia";
            gridProfessor.Columns[4].HeaderText = "Salario";
            gridProfessor.Columns[5].HeaderText = "Estado Civil";
            gridProfessor.Columns[6].HeaderText = "Genero";
            gridProfessor.Columns[7].HeaderText = "Data de Nascimento";
            gridProfessor.Columns[8].HeaderText = "Email";
            gridProfessor.Columns[9].HeaderText = "Telefone";
            gridProfessor.Columns[10].Visible = false;
            gridProfessor.Columns[11].Visible = false;
            gridProfessor.Columns[12].Visible = false;
            gridProfessor.Columns[13].HeaderText = "Estado";
            gridProfessor.Columns[14].HeaderText = "Cidade";
            gridProfessor.Columns[15].HeaderText = "Endereço";
        }

        private void VisualizarProfessor_Load(object sender, EventArgs e)
        {
            Listar();
        }
    }
}
