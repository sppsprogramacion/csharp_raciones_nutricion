using CapaDatos;
using DAO;
using DAOImplement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NSapMenu
    {
        private readonly ISapMenuDAO sapMenuDAO;
        public NSapMenu()
        {
            sapMenuDAO = new SapMenuDaoImplement();
        }

        public (List<DSapMenu> lista, string error) ListarTodos()
        {
            return sapMenuDAO.ListaTodos();
        }

        public (List<DSapMenu> lista, string error) ListarxUnidad(int id_unidad)
        {
            return sapMenuDAO.ListaXUnidad(id_unidad);
        }
    }
}
