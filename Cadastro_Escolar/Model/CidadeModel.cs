using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastro_Escolar.DAO;
using MySql.Data.MySqlClient;

namespace Cadastro_Escolar.Model
{
    internal class CidadeModel
    {


        CidadeDAO daoCidade = new CidadeDAO();

        public DataTable BuscarCidade(int estadoId)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = daoCidade.BuscarCidade(estadoId);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
