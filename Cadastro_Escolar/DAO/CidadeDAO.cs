using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Cadastro_Escolar.DAO
{
    internal class CidadeDAO
    {
        MySqlCommand sql;
        Conexao con = new Conexao();
        public DataTable BuscarCidade(int estadoId)
        {
            try
            {
                con.AbrirConexao();
                sql = new MySqlCommand("SELECT cidade.nome , estados.* FROM estados INNER JOIN cidade ON estados.id = cidade.uf WHERE estados.id = @estadoid;", con.con);

                sql.Parameters.AddWithValue("@estadoid", estadoId);

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
