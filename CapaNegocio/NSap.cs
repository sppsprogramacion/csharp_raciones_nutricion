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
    public class NSap
    {
        private readonly ISapDAO sapDAO;
        public NSap()
        {
            sapDAO = new SapDaoImplement();
        }

        public (List<DSap> lista, string error) ListarTodos()
        {
            return sapDAO.ListaTodos();
        }
    }
}
