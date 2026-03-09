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
    public class SapMenuDaoImplement : ISapMenuDAO
    {
        //LISTAR TODOS
        public (List<DSapMenu> lista, string error) ListaTodos()
        {
            List<DSapMenu> lista = new List<DSapMenu>();
            try
            {
                using (var db = new MiDbContext())
                {
                    lista = db.SapMenu
                     .Include(s => s.sap)
                     .Include(s => s.unidad)
                     .Include(s => s.tipo_menu)
                     .OrderBy(s => s.tipo_menu.orden)   // Orden ascendente
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
        //´FIN LISTAR TODOS....................................................

        //LISTAR POR UNIDAD
        public (List<DSapMenu> lista, string error) ListaXUnidad(int id_unidad)
        {
            List<DSapMenu> lista = new List<DSapMenu>();
            try
            {
                using (var db = new MiDbContext())
                {
                    lista = db.SapMenu
                     .Include(s => s.sap)
                     .Include(s => s.unidad)
                     .Include(s => s.tipo_menu)
                     .Where( s => s.unidad_id == id_unidad)
                     .OrderBy(u => u.sap.orden)   // Orden ascendente
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
        //FIN LISTAR POR UNIDAD.................................................
    }
}
