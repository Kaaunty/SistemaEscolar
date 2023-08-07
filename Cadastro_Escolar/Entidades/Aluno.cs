using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Cadastro_Escolar.Entidades
{
    public class Aluno : Pessoa
    {
        int ra;
        string bolsista;
        string mensalidade;



        public int Ra { get => ra; set => ra = value; }
        public string Bolsista { get => bolsista; set => bolsista = value; }
        public string Mensalidade { get => mensalidade; set => mensalidade = value; }

    }
}
