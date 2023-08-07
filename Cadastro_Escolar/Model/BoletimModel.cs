using Cadastro_Escolar.DAO;
using Cadastro_Escolar.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;

namespace Cadastro_Escolar.Model
{

    internal class BoletimModel
    {
        MySqlCommand sql;
        Conexao con = new Conexao();
        BoletimDAO dao = new BoletimDAO();
        public void Salvar(Boletim dado)
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

        public DataTable Listar()
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("SELECT b.id, a.nome AS nome_aluno, b.disciplina, b.nota1, b.nota2, b.nota3, b.nota4, b.media FROM boletim b INNER JOIN aluno a ON b.id_aluno = a.id;", con.con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = sql;
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.FecharConexao();
            }
        }
    }
}
