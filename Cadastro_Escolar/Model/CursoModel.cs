using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastro_Escolar.Entidades;
using System.Windows.Forms;
using Cadastro_Escolar.DAO;

namespace Cadastro_Escolar.Model
{
    internal class CursoModel
    {
        CursoDAO dao = new CursoDAO();
        public DataTable Listar()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dao.Listar();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable BuscarMateria(int cursoid)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dao.BuscarMateria(cursoid);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
































    }
}
