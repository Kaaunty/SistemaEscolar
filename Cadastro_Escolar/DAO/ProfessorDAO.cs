using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastro_Escolar.Entidades;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Cadastro_Escolar.DAO
{
    internal class ProfessorDAO
    {
        MySqlCommand sql;
        Conexao con = new Conexao();

        public DataTable Listar()
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("SELECT * FROM professor order by id asc", con.con);
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
        }


        public void Salvar(Professor dado)
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("INSERT INTO professor(nome, curso, materia, salario, estadoCivil, genero, dataNascimento, email, telefone, foto, id_professor, cep, estado, cidade, endereco) " +
                    "VALUES (@nome, @curso, @materia, @salario, @estadoCivil, @genero, @Nascimento, @email, @telefone, @foto, @professor, @cep, @estado, @cidade, @endereco)", con.con);
                sql.Parameters.AddWithValue("@nome", dado.Nome);
                sql.Parameters.AddWithValue("@curso", dado.Curso);
                sql.Parameters.AddWithValue("@materia", dado.Materia);
                sql.Parameters.AddWithValue("@salario", dado.Salario);
                sql.Parameters.AddWithValue("@estadoCivil", dado.EstadoCivil);
                sql.Parameters.AddWithValue("@genero", dado.Genero);
                sql.Parameters.AddWithValue("@Nascimento", dado.DataNascimento);
                sql.Parameters.AddWithValue("@email", dado.Email);
                sql.Parameters.AddWithValue("@telefone", dado.Telefone);
                sql.Parameters.AddWithValue("@foto", dado.Foto);
                sql.Parameters.AddWithValue("@professor", dado.Id_professor);
                sql.Parameters.AddWithValue("@cep", dado.Cep);
                sql.Parameters.AddWithValue("@estado", dado.Estado);
                sql.Parameters.AddWithValue("@cidade", dado.Cidade);
                sql.Parameters.AddWithValue("@endereco", dado.Endereco);

                sql.ExecuteNonQuery();
                sql.Dispose();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao salvar os dados" + ex);
            }
            finally
            {
                con.FecharConexao();
            }
        }

        public void Editar(Professor dado)
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("UPDATE professor SET nome = @nome, curso = @curso WHERE id = @id", con.con);
                //("UPDATE professor SET nome = @nome,curso = @curso,materia = @materia,salario = @salario,estadoCivil = @estadoCivil,genero = @genero,DataNasicmento = @Datanascimento,email = @email,telefone = @telefone,foto = @foto,id_professor = @professor,cep = @cep,estado = @estado,cidade = @cidade,endereco = @endereco) WHERE id = @id", con.con);
                sql.Parameters.AddWithValue("@nome", dado.Nome);
                sql.Parameters.AddWithValue("@curso", dado.Curso);
                //sql.Parameters.AddWithValue("@materia", dado.Materia);
                //sql.Parameters.AddWithValue("@salario", dado.Salario);
                //sql.Parameters.AddWithValue("@estadoCivil", dado.EstadoCivil);
                //sql.Parameters.AddWithValue("@genero", dado.Genero);
                //sql.Parameters.AddWithValue("@Nascimento", dado.DataNascimento);
                //sql.Parameters.AddWithValue("@email", dado.Email);
                //sql.Parameters.AddWithValue("@telefone", dado.Telefone);
                //sql.Parameters.AddWithValue("@foto", dado.Foto);
                //sql.Parameters.AddWithValue("@professor", dado.Id_professor);
                //sql.Parameters.AddWithValue("@cep", dado.Cep);
                //sql.Parameters.AddWithValue("@estado", dado.Estado);
                //sql.Parameters.AddWithValue("@cidade", dado.Cidade);
                //sql.Parameters.AddWithValue("@endereco", dado.Endereco);
                sql.Parameters.AddWithValue("@id", dado.Id);

                sql.ExecuteNonQuery();
                sql.Dispose();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao editar os Dados: " + ex.Message);
            }
            finally
            {
                con.FecharConexao();
            }
        }

        public void Excluir(Professor dado)
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("DELETE FROM professor WHERE id = @id", con.con);
                sql.Parameters.AddWithValue("@id", dado.Id);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao excluir os Dados: " + ex.Message);
            }


        }

        public DataTable Buscar(Professor dado)
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("SELECT * FROM professor WHERE curso = @curso", con.con);
                sql.Parameters.AddWithValue("@curso", dado.Curso);
                MySqlDataAdapter da = new MySqlDataAdapter(sql);
                da.SelectCommand = sql;
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }








    }
}
