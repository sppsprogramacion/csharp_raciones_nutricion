using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface ISapMenuDAO
    {
        (List<DSapMenu> lista, string error) ListaTodos();
        (List<DSapMenu> lista, string error) ListaXUnidad(int id_unidad);
    }
}
