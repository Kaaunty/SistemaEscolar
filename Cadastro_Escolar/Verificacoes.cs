using Cadastro_Escolar.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cadastro_Escolar
{

    internal class Verificacoes
    {
        public static bool VerificaNumeroOuCaracterEspecial(string text)
        {
            return Regex.IsMatch(text, @"[\d\W]");
        }

        public static bool Enumero(string entrada)
        {
            return int.TryParse(entrada, out _);
        }





    }
}
