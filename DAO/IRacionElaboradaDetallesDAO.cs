using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IRacionElaboradaDetallesDAO
    {
        void InsertarLista(List<DRacionElaboradaDetalles> listaDetalles);
        void Editar(DRacionElaboradaDetalles racionElaborada);
        DRacionElaboradaDetalles ObtenerPorId(int id);

        (List<DRacionElaboradaDetalles> lista, string error) ListaXIdRacionELaborada(int idRacionElaborada);
        (List<DRacionElaboradaDetalles> lista, string error) ListaXIdRacionELaboradaXUnidad(int idRacionElaborada, int idUnidad);
        (List<DRacionElaboradaDetalles> lista, string error) ListaTodos();

        void EliminarRacionesCargadas(int idRacionElaborada);
    }
}
