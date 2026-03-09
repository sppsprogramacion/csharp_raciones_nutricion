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
    public class NRacionSolicitadaDetalles
    {
        private readonly IRacionSolicitadaDetallesDAO racionSolicitadaDetallesDAO;
        public NRacionSolicitadaDetalles()
        {
            racionSolicitadaDetallesDAO = new RacionSolicitadaDetallesDaoImplement();
        }

        public void InsertarDetalles(List<DRacionesSolicitadasDetalles> listaDetalles)
        {
            racionSolicitadaDetallesDAO.InsertarLista(listaDetalles);

        }

        public void EditarDetalle(DRacionesSolicitadasDetalles solicitadaDetalle)
        {
            racionSolicitadaDetallesDAO.Editar(solicitadaDetalle);

        }

        public (List<DRacionesSolicitadasDetalles> lista, string error) ListarXIdRacionSolicitada(int idRacionSolicitada)
        {
            return racionSolicitadaDetallesDAO.ListaXIdRacionSolicitada(idRacionSolicitada);
        }

        public (List<DRacionesSolicitadasDetalles> lista, string error) ListarXIdRacionSolicitadaXUnidad(int idRacionSolicitada, int idUnidad)
        {
            return racionSolicitadaDetallesDAO.ListaXIdRacionSolicitadaXUnidad(idRacionSolicitada, idUnidad);
        }

        public void EliminarDetalles(int idRacionSolicitada)
        {
            racionSolicitadaDetallesDAO.EliminarRacionesCargadas(idRacionSolicitada);

        }
    }
}
