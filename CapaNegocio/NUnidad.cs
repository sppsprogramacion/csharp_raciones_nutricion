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
    public class NUnidad
    {
        private readonly IUnidadDAO unidadDAO;
        public NUnidad()
        {
            unidadDAO = new UnidadDaoImplement();
        }

        public (List<DUnidad> lista, string error) ListarTodos()
        {
            return unidadDAO.ListaTodos();
        }
    }
}
