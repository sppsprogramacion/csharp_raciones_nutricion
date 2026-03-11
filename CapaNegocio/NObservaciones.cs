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
    public class NObservaciones
    {
        private readonly IObservacionSolicitadaDAO observacionSolicitadaDAO;

        private readonly IObservacionElaboradaDAO observacionElaboradaDAO;

        private readonly IObservacionGeneralDAO observacionGeneralDAO;

        public NObservaciones()
        {
            observacionSolicitadaDAO = new ObservacionSolicitadaDaoImplement();

            observacionElaboradaDAO = new ObservacionElaboradaDaoImplement();

            observacionGeneralDAO = new ObservacionGeneralDaoImplement();
        }

        //METODOS OBSERVACIONES SOLICITADA
        public void CrearObservacionSolicitada(DObservacionSolicitada observacionSolicitada)
        {
            observacionSolicitadaDAO.Insertar(observacionSolicitada);

        }

        public void EditarObservacionSolicitada(DObservacionSolicitada observacionSolicitada)
        {
            observacionSolicitadaDAO.Editar(observacionSolicitada);

        }

        public (List<DObservacionSolicitada> lista, string error) ListarTodosXIdSolicitada(int idSolicitada)
        {
            return observacionSolicitadaDAO.ListaTodosXSolicitada(idSolicitada);
        }

        //buscar POR id
        public (DObservacionSolicitada observacionSolicitadaResponse, string error) BuscarXIdSolicitada(int idObservacion)
        {
            return observacionSolicitadaDAO.ObtenerPorId(idObservacion);
        }
        //FIN buscar POR id..................................

        //FIN METODOS OBSERVACIONES SOLICITADA.........................................................
        //.........................................................................................

        //METODOS OBSERVACIONES ELABORADA
        public void CrearObservacionElaborada(DObservacionElaborada observacionElaborada)
        {
            observacionElaboradaDAO.Insertar(observacionElaborada);

        }

        public void EditarObservacionElaborada(DObservacionElaborada observacionElaborada)
        {
            observacionElaboradaDAO.Editar(observacionElaborada);

        }

        public (List<DObservacionElaborada> lista, string error) ListarTodosXIdElaborada(int idElaborada)
        {
            return observacionElaboradaDAO.ListaTodosXElaborada(idElaborada);
        }

        //buscar POR id
        public (DObservacionElaborada observacionSolicitadaResponse, string error) BuscarXIdObservacion(int idObservacion)
        {
            return observacionElaboradaDAO.ObtenerPorId(idObservacion);
        }
        //FIN buscar POR id..................................

        //FIN METODOS OBSERVACIONES ELABORADA.........................................................
        //.........................................................................................

        //METODOS OBSERVACIONES GENERAL
        public void CrearObservacionGeneral(DObservacionGeneral observacionGeneral)
        {
            observacionGeneralDAO.Insertar(observacionGeneral);

        }

        public void EditarObservacionGeneral(DObservacionGeneral observacionGeneral)
        {
            observacionGeneralDAO.Editar(observacionGeneral);

        }

        public (List<DObservacionGeneral> lista, string error) ListarTodosGeneral()
        {
            return observacionGeneralDAO.ListaTodos();
        }

        //buscar POR id
        public (DObservacionGeneral observacionSolicitadaResponse, string error) BuscarXIdObservacionGeneral(int idObservacion)
        {
            return observacionGeneralDAO.ObtenerPorId(idObservacion);
        }
        //FIN buscar POR id..................................

        //FIN METODOS OBSERVACIONES GENERAL.........................................................
        //.........................................................................................
    }
}
