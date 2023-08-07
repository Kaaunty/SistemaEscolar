using MySql.Data.MySqlClient;
using System;

namespace Cadastro_Escolar
{
    internal class Conexao
    {
        string conexao = "SERVER=localhost; DATABASE=escola; UID=root; PWD=;";
        public MySqlConnection con = null;

        public void AbrirConexao()
        {
            try
            {
                con = new MySqlConnection(conexao);
                con.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void FecharConexao()
        {
            try
            {
                con = new MySqlConnection(conexao);
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
