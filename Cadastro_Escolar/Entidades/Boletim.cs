using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Escolar.Entidades
{
    internal class Boletim
    {
        int idAluno;
        string idDisciplina;
        double nota1;
        double nota2;
        double nota3;
        double nota4;
        double media;


        public int IdAluno { get => idAluno; set => idAluno = value; }
        public string IdDisciplina { get => idDisciplina; set => idDisciplina = value; }
        public double Nota1 { get => nota1; set => nota1 = value; }
        public double Nota2 { get => nota2; set => nota2 = value; }
        public double Nota3 { get => nota3; set => nota3 = value; }
        public double Nota4 { get => nota4; set => nota4 = value; }
        public double Media { get => media; set => media = value; }
    }
}
