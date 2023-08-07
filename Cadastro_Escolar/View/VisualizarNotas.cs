using Cadastro_Escolar.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cadastro_Escolar.View
{
    public partial class VisualizarNotas : Form
    {
        BoletimModel model = new BoletimModel();
        public VisualizarNotas()
        {
            InitializeComponent();
            Listar();
        }


        public void Listar()
        {

            gridNotas.DataSource = model.Listar();
            gridNotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridNotas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            gridNotas.Columns[0].Visible = false;
            gridNotas.Columns[1].HeaderText = "Nome";
            gridNotas.Columns[2].HeaderText = "Materia";
            gridNotas.Columns[3].HeaderText = "Nota 1";
            gridNotas.Columns[4].HeaderText = "Nota 2";
            gridNotas.Columns[5].HeaderText = "Nota 3";
            gridNotas.Columns[6].HeaderText = "Nota 4";
            gridNotas.Columns[7].HeaderText = "Media";


        }


    }
}
