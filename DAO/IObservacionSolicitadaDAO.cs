using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IObservacionSolicitadaDAO
    {
        void Insertar(DObservacionSolicitada observacionSolicitada);
        void Editar(DObservacionSolicitada observacionSolicitada);
        (DObservacionSolicitada observacionSolicitada, string error) ObtenerPorId(int id);
        (List<DObservacionSolicitada> lista, string error) ListaTodosXSolicitada(int id);
    }
}
