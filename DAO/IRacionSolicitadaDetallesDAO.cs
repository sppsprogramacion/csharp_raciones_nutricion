using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IRacionSolicitadaDetallesDAO
    {
        void InsertarLista(List<DRacionesSolicitadasDetalles> listaDetalles);
        void Editar(DRacionesSolicitadasDetalles racionSolicitada);
        DRacionesSolicitadasDetalles ObtenerPorId(int id);

        (List<DRacionesSolicitadasDetalles> lista, string error) ListaXIdRacionSolicitada(int idRacionSolicitada);
        (List<DRacionesSolicitadasDetalles> lista, string error) ListaXIdRacionSolicitadaXUnidad(int idRacionSolicitada, int idUnidad);
        (List<DRacionesSolicitadasDetalles> lista, string error) ListaTodos();

        void EliminarRacionesCargadas(int idRacionSolicitada);
    }
}
