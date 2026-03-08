using CapaDatos;
using DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOImplement
{
    public class RolDaoImplement : IRolDao
    {

        public (List<DRol> lista, string error) ListaTodos()
        {
            List<DRol> lista = new List<DRol>();

            try
            {
                using (var db = new MiDbContext())
                {
                    //lista = db.Roles.ToList();
                    lista = db.Roles
                     .Where(r => r.id_rol != "superadmin")              
                     .ToList();

                    return (lista, null);
                }
            }
            catch (Exception ex)
            {
                // Buscar MySqlException dentro del InnerException
                var mysqlEx = ex.InnerException as MySqlException;

                if (mysqlEx != null)
                {
                    // Códigos de MySQL típicos por falta de conexión
                    if (mysqlEx.Number == 0 || mysqlEx.Number == 1042 ||
                        mysqlEx.Number == 2002 || mysqlEx.Number == 2003)
                    {
                        return (null, "No hay conexión con el servidor de base de datos.");
                    }

                    // Si es otro error MySQL
                    return (null, "Error de MySQL: " + mysqlEx.Message);
                }

                // Si no es mysqlEx → error inesperado
                return (null, "Error inesperado: " + ex.Message);
            }
        }
    }
}
