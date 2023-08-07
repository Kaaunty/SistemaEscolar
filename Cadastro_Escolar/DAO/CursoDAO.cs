using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastro_Escolar.Entidades;
using MySql.Data.MySqlClient;

namespace Cadastro_Escolar.DAO
{
    internal class CursoDAO
    {
        MySqlCommand sql;
        Conexao con = new Conexao();


        public DataTable Listar()
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("SELECT * FROM curso order by id asc", con.con);
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

        public DataTable BuscarMateria(int cursoid)
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("SELECT materia.*, curso_materia.materiaid FROM curso_materia INNER JOIN materia ON materia.id = curso_materia.materiaid WHERE curso_materia.cursoid = @cursoid;", con.con);

                sql.Parameters.AddWithValue("@cursoid", cursoid);

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
