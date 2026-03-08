using CapaDatos;
using DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOImplement
{
    public class TipoMenuDaoImplement : ITipoMenuDao
    {
        public (List<DTipoMenu> lista, string error) ListaTodos()
        {
            List<DTipoMenu> lista = new List<DTipoMenu>();
            try
            {
                using (var db = new MiDbContext())
                {
                    lista = db.TiposMenu
                     .Include(s => s.menu)
                     .OrderBy(u => u.orden)   // Orden ascendente
                     .ToList();

                    return (lista, null);
                }
            }
            catch (Exception ex)
            {
                // 🟦 Detecta si realmente es error de conexión MySQL
                if (ErrorHelper.EsErrorDeConexion(ex))
                {
                    return (null, "No hay conexión con el servidor de base de datos.");
                }

                // Si no es mysqlEx → error inesperado
                return (null, "Error inesperado: " + ex.Message);
            }
        }
    }
}
