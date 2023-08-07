using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Escolar.Entidades
{
    public class Curso : Pessoa
    {
        int cursoid;

        public int Cursoid { get => cursoid; set => cursoid = value; }
    }
}
