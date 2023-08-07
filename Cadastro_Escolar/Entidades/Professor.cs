using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Escolar.Entidades
{
    public class Professor : Pessoa
    {
        string salario;
        int id_professor;
        public string Salario { get => salario; set => salario = value; }
        public int Id_professor { get => id_professor; set => id_professor = value; }
    }
}
