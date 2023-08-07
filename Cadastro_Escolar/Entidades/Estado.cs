using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Escolar.Entidades
{
    internal class Estado : Pessoa
    {

        string estadoSigla;
        int idEstado;


        public string EstadoSigla { get => estadoSigla; set => estadoSigla = value; }
        public int IdEstado { get => idEstado; set => idEstado = value; }

    }
}
