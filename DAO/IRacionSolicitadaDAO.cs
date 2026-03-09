using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IRacionSolicitadaDAO
    {
        void Insertar(DRacionSolicitada racionSolicitada);
        void Editar(DRacionSolicitada racionSolicitada);
        DRacionSolicitada ObtenerPorId(int id);
        (List<DRacionSolicitada> lista, string error) ListaTodos();
        (List<DRacionSolicitada> lista, string error) ListaXFecha(string fechaInicio, string fechaFin);
    }
}
