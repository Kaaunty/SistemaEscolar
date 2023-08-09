using Cadastro_Escolar.Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cadastro_Escolar
{

    internal class Verificacoes
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var enderecoEmail = new System.Net.Mail.MailAddress(email);
                return enderecoEmail.Address == email;
            }
            catch
            {
                return false;
            }
        }



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

