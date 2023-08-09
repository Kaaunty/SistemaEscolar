using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Escolar.Entidades
{
    public class Pessoa
    {
        public int id;
        string nome;
        string curso;
        string materia;
        string estadoCivil;
        string genero;
        DateTime dataNascimento;
        string email;
        string telefone;
        byte[] foto;
        string cep;
        string estado;
        string cidade;
        string endereco;


        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Curso { get => curso; set => curso = value; }
        public string Materia { get => materia; set => materia = value; }
        public string EstadoCivil { get => estadoCivil; set => estadoCivil = value; }
        public string Genero { get => genero; set => genero = value; }
        public DateTime DataNascimento { get => dataNascimento; set => dataNascimento = value; }
        public string Email { get => email; set => email = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public byte[] Foto { get => foto; set => foto = value; }
        public string Cep { get => cep; set => cep = value; }
        public string Estado { get => estado; set => estado = value; }
        public string Cidade { get => cidade; set => cidade = value; }
        public string Endereco { get => endereco; set => endereco = value; }
    }
}
