using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cadastro_Escolar.DAO;
using MySql.Data.MySqlClient;

namespace Cadastro_Escolar.Model
{
    internal class EstadoModel
    {
        EstadoDAO daoEstado = new EstadoDAO();

        public DataTable Listar()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = daoEstado.Listar();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
