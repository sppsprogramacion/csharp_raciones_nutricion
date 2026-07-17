using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IUnidadGrupoDAO
    {
        (List<DUnidadGrupo> lista, string error) ListaTodos();
    }
}
