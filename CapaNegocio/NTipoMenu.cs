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
    public class NTipoMenu
    {
        private readonly ITipoMenuDao tipoMenuDAO;
        public NTipoMenu()
        {
            tipoMenuDAO = new TipoMenuDaoImplement();
        }

        public (List<DTipoMenu> lista, string error) ListarTodos()
        {
            return tipoMenuDAO.ListaTodos();
        }
    }
}
