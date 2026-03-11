using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IObservacionElaboradaDAO
    {
        void Insertar(DObservacionElaborada observacionElaborada);
        void Editar(DObservacionElaborada observacionElaborada);
        (DObservacionElaborada observacionElaborada, string error) ObtenerPorId(int idObservacion);
        (List<DObservacionElaborada> lista, string error) ListaTodosXElaborada(int idElaborada);
    }
}
