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
    public class NAnexoMenu
    {
        private readonly IAnexoMenuDAO anexoMenuDAO;
        public NAnexoMenu()
        {
            anexoMenuDAO = new AnexoMenuDaoImplement();
        }

        public (List<DAnexoMenu> lista, string error) ListarTodos()
        {
            return anexoMenuDAO.ListaTodos();
        }
    }
}
