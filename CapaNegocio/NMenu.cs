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
    public class NMenu
    {
        private readonly IMenuDao menuDAO;
        public NMenu()
        {
            menuDAO = new MenuDaoImplement();
        }

        public (List<DMenu> lista, string error) ListarTodos()
        {
            return menuDAO.ListaTodos();
        }
    }
}
