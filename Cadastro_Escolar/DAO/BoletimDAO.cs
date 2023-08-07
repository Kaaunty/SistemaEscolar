using Cadastro_Escolar.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Cadastro_Escolar.DAO
{
    internal class BoletimDAO
    {


        MySqlCommand sql;
        Conexao con = new Conexao();
        public void Salvar(Boletim dado)
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("INSERT INTO boletim (nota1,nota2,nota3,nota4,media) VALUES (@nota1,@nota2,@nota3,@nota4,@media)", con.con);
                sql.Parameters.AddWithValue("@nota1", dado.Nota1);
                sql.Parameters.AddWithValue("@nota2", dado.Nota2);
                sql.Parameters.AddWithValue("@nota3", dado.Nota3);
                sql.Parameters.AddWithValue("@nota4", dado.Nota4);
                sql.Parameters.AddWithValue("@media", dado.Media);
                sql.ExecuteNonQuery();
                sql.Dispose();


                sql = new MySqlCommand("INSERT INTO boletim (id_aluno, disciplina, nota1, nota2 , nota3 , nota4,media ) VALUES (@id_aluno, @id_disciplina, @nota1, @nota2, @nota3, @nota4, @media)", con.con);
                {
                    sql.Parameters.AddWithValue("@id_aluno", dado.IdAluno);
                    sql.Parameters.AddWithValue("@id_disciplina", dado.IdDisciplina);
                    sql.Parameters.AddWithValue("@nota1", dado.Nota1);
                    sql.Parameters.AddWithValue("@nota2", dado.Nota2);
                    sql.Parameters.AddWithValue("@nota3", dado.Nota3);
                    sql.Parameters.AddWithValue("@nota4", dado.Nota4);
                    sql.Parameters.AddWithValue("@media", dado.Media);
                    sql.ExecuteNonQuery();
                    sql.Dispose();
                }





            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.FecharConexao();
            }

        }











    }
}
