using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Conexion
{
    public class MiConexion
    {
        //private static readonly string connectionString = "Server=localhost;Database=db_pedro_raciones_nutricione;Uid=roote;Pwd=A123456b;SslMode=none;";

        public static MySqlConnection GetConnection()
        {
            //var conn = new MySqlConnection(connectionString);
            var conn = new MySqlConnection("");
            return conn;
        }
    }

        
    
}
