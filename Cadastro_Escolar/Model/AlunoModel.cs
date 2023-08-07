using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cadastro_Escolar.DAO;
using Cadastro_Escolar.Entidades;
using MySqlX.XDevAPI;

namespace Cadastro_Escolar.Model
{
    public class AlunoModel
    {
        AlunoDAO dao = new AlunoDAO();

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


        public void Salvar(Aluno dado)
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


        public void Editar(Aluno dado)
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

        public void Excluir(Aluno dado)
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







    }
}