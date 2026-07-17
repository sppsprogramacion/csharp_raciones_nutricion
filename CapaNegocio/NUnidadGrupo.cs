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
    public class NUnidadGrupo
    {
        private readonly IUnidadGrupoDAO unidadGrupoDAO;
        public NUnidadGrupo()
        {
            unidadGrupoDAO = new UnidadGrupoDaoImplement();
        }

        public (List<DUnidadGrupo> lista, string error) ListarTodos()
        {
            return unidadGrupoDAO.ListaTodos();
        }
    }
}
