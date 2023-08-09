using Cadastro_Escolar.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Escolar.DAO
{
    internal class LoginDAO
    {
        MySqlCommand sql;
        Conexao con = new Conexao();
        public Login Logar(Login dado)
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("SELECT * FROM login WHERE username = @username AND password = @password", con.con);
                sql.Parameters.AddWithValue("@username", dado.Usuario);
                sql.Parameters.AddWithValue("@password", dado.Senha);
                MySqlDataReader dr;
                dr = sql.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dado.Usuario = Convert.ToString(dr["username"]);
                        dado.Senha = Convert.ToString(dr["password"]);
                    }
                }
                else
                {
                    dado.Usuario = null;
                    dado.Senha = null;
                }
                return dado;


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
