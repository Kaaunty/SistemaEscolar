using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Escolar.Entidades
{
    internal class Login
    {
        int id;
        string nome;
        string usuario;
        string senha;

        public string Usuario { get => usuario; set => usuario = value; }
        public string Senha { get => senha; set => senha = value; }
        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
    }
}
