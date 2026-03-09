using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOImplement
{
    public class ErrorHelper
    {
        // Devuelve true si la excepción indica problema de conexión a MySQL
        public static bool EsErrorDeConexion(Exception ex)
        {
            // Buscar si hay un MySqlException dentro del InnerException
            var mysqlEx = ex as MySqlException
                          ?? ex.InnerException as MySqlException
                          ?? ex.GetBaseException() as MySqlException;

            if (mysqlEx == null)
                return false;

            // Códigos MySQL típicos de error de conexión
            return mysqlEx.Number == 0     // Cannot connect to server
                || mysqlEx.Number == 1042  // Bad host
                || mysqlEx.Number == 2002  // Can't connect (socket)
                || mysqlEx.Number == 2003; // Can't connect (port)
        }
    }
}
