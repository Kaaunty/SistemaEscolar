using Cadastro_Escolar.DAO;
using Cadastro_Escolar.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cadastro_Escolar.Model
{
    internal class LoginModel
    {
        LoginDAO dao = new LoginDAO();
        public Login Logar(Login dado)
        {
            try
            {
                return dao.Logar(dado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
