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
    public class NRacionElaboradaDetalles
    {
        private readonly IRacionElaboradaDetallesDAO racionElaboradaDetallesDAO;
        public NRacionElaboradaDetalles()
        {
            racionElaboradaDetallesDAO = new RacionElaboradaDetallesDaoImplement();
        }

        public void InsertarDetalles(List<DRacionElaboradaDetalles> listaDetalles)
        {
            racionElaboradaDetallesDAO.InsertarLista(listaDetalles);

        }

        public void InsertarUnDetalle(DRacionElaboradaDetalles detalle)
        {
            racionElaboradaDetallesDAO.InsertarUno(detalle);

        }

        public void EditarDetalle(DRacionElaboradaDetalles elaboradaDetalle)
        {
            racionElaboradaDetallesDAO.Editar(elaboradaDetalle);

        }

        public (List<DRacionElaboradaDetalles> lista, string error) ListarXIdRacionElaborada(int idRacionElaborada)
        {
            return racionElaboradaDetallesDAO.ListaXIdRacionELaborada(idRacionElaborada);
        }

        public (List<DRacionElaboradaDetalles> lista, string error) ListarXIdRacionElaboradaXUnidad(int idRacionElaborada, int idUnidad)
        {
            return racionElaboradaDetallesDAO.ListaXIdRacionELaboradaXUnidad(idRacionElaborada, idUnidad);
        }

        public void EliminarDetalles(int idRacionElaborada)
        {
            racionElaboradaDetallesDAO.EliminarRacionesCargadas(idRacionElaborada);

        }
    }
}
