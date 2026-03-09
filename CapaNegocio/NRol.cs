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
    public class NRol
    {
        private readonly IRolDao rolDAO;
        public NRol()
        {
            rolDAO = new RolDaoImplement();
        }

        public (List<DRol> lista, string error) ListarTodos()
        {
            return rolDAO.ListaTodos();
        }
    }
}
