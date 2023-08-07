using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cadastro_Escolar.Entidades;
using MySql.Data.MySqlClient;

namespace Cadastro_Escolar.DAO
{
    internal class AlunoDAO
    {
        MySqlCommand sql;
        Conexao con = new Conexao();
        public DataTable Listar()
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("SELECT * FROM aluno order by id asc", con.con);
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

        public void Salvar(Aluno dado)
        {
            try
            {
                con.AbrirConexao();

                sql = new MySqlCommand("INSERT INTO aluno(ra, nome, curso, materia, bolsista, mensalidade, estadoCivil, genero, dataNascimento, email, telefone, foto, cep, estado, cidade, endereco) VALUES (@ra, @nome, @curso, @materia,@bolsista, @mensalidade, @estadoCivil, @genero, @Nascimento, @email, @telefone, @foto, @cep, @estado, @cidade, @endereco)", con.con);
                sql.Parameters.AddWithValue("@ra", dado.Ra);
                sql.Parameters.AddWithValue("@nome", dado.Nome);
                sql.Parameters.AddWithValue("@curso", dado.Curso);
                sql.Parameters.AddWithValue("@materia", dado.Materia);
                sql.Parameters.AddWithValue("@bolsista", dado.Bolsista);
                sql.Parameters.AddWithValue("@mensalidade", dado.Mensalidade);
                sql.Parameters.AddWithValue("@estadoCivil", dado.EstadoCivil);
                sql.Parameters.AddWithValue("@genero", dado.Genero);
                sql.Parameters.AddWithValue("@Nascimento", dado.DataNascimento);
                sql.Parameters.AddWithValue("@email", dado.Email);
                sql.Parameters.AddWithValue("@telefone", dado.Telefone);
                sql.Parameters.AddWithValue("@foto", dado.Foto);
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

        public void Editar(Aluno dado)
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("UPDATE aluno SET ra = @ra, nome = @nome, curso = @curso, materia = @materia, bolsista = @bolsista, mensalidade = @mensalidade , estadocivil = @estadoCivil, genero = @genero, dataNascimento =@Nascimento,email = @email,telefone = @telefone,foto = @foto," +
                    "cep = @cep, estado = @estado, cidade = @cidade, endereco = @endereco WHERE ra = @ra", con.con);
                sql.Parameters.AddWithValue("@ra", dado.Ra);
                sql.Parameters.AddWithValue("@nome", dado.Nome);
                sql.Parameters.AddWithValue("@curso", dado.Curso);
                sql.Parameters.AddWithValue("@materia", dado.Materia);
                sql.Parameters.AddWithValue("@bolsista", dado.Bolsista);
                sql.Parameters.AddWithValue("@mensalidade", dado.Mensalidade);
                sql.Parameters.AddWithValue("@estadoCivil", dado.EstadoCivil);
                sql.Parameters.AddWithValue("@genero", dado.Genero);
                sql.Parameters.AddWithValue("@Nascimento", dado.DataNascimento);
                sql.Parameters.AddWithValue("@email", dado.Email);
                sql.Parameters.AddWithValue("@telefone", dado.Telefone);
                sql.Parameters.AddWithValue("@foto", dado.Foto);
                sql.Parameters.AddWithValue("@cep", dado.Cep);
                sql.Parameters.AddWithValue("@estado", dado.Estado);
                sql.Parameters.AddWithValue("@cidade", dado.Cidade);
                sql.Parameters.AddWithValue("@endereco", dado.Endereco);
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

        public void Excluir(Aluno dado)
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("DELETE FROM aluno WHERE ra = @ra", con.con);
                sql.Parameters.AddWithValue("@ra", dado.Ra);
                sql.ExecuteNonQuery();
                sql.Dispose();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao excluir os Dados: " + ex.Message);
            }
            finally
            {
                con.FecharConexao();
            }


        }





































    }
}
