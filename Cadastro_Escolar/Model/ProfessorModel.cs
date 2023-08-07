using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastro_Escolar.DAO;
using Cadastro_Escolar.Entidades;
using System.Windows.Forms;

namespace Cadastro_Escolar.Model
{
    public class ProfessorModel
    {
        ProfessorDAO dao = new ProfessorDAO();

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


        public void Salvar(Professor dado)
        {
            try
            {
                dao.Salvar(dado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar os Dados: " + ex.Message);
            }
        }


        public void Editar(Professor dado)
        {
            try
            {
                dao.Editar(dado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar os Dados: " + ex.Message);
            }
        }

        public void Excluir(Professor dado)
        {
            try
            {
                dao.Excluir(dado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir os Dados: " + ex.Message);
            }
        }

        public object Buscar(Professor dado)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dao.Buscar(dado);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}
