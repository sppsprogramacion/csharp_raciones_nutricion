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
    public class NAnexoMenuTipo
    {
        private readonly IAnexoMenuTipoDAO anexoMenuTipoDAO;
        public NAnexoMenuTipo()
        {
            anexoMenuTipoDAO = new AnexoMenuTipoDaoImplement();
        }

        public (List<DAnexoMenuTipo> lista, string error) ListarTodos()
        {
            return anexoMenuTipoDAO.ListaTodos();
        }
    }
}
