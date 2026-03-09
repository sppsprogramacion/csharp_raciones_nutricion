using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IRacionElaboradaDAO
    {
        void Insertar(DRacionElaborada racionElaborada);
        void Editar(DRacionElaborada racionElaborada);
        DRacionElaborada ObtenerPorId(int id);
        (List<DRacionElaborada> lista, string error) ListaTodos();
        (List<DRacionElaborada> lista, string error) ListaXFecha(string fechaInicio, string fechaFin);
    }
}
