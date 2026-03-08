using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IMenuDao
    {
        (List<DMenu> lista, string error) ListaTodos();
    }
}
