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
        public NObservaciones()
        {
            observacionSolicitadaDAO = new ObservacionSolicitadaDaoImplement();
        }

        public void CrearObservacionSolicitada(DObservacionSolicitada observacionSolicitada)
        {
            observacionSolicitadaDAO.Insertar(observacionSolicitada);

        }

        public void Editar(DObservacionSolicitada observacionSolicitada)
        {
            observacionSolicitadaDAO.Editar(observacionSolicitada);

        }

        public (List<DObservacionSolicitada> lista, string error) ListarTodosXIdSolicitada(int idSolicitada)
        {
            return observacionSolicitadaDAO.ListaTodosXSolicitada(idSolicitada);
        }

        //buscar POR id
        public (DObservacionSolicitada observacionSolicitadaResponse, string error) BuscarXId(int idObservacion)
        {
            return observacionSolicitadaDAO.ObtenerPorId(idObservacion);
        }
        //FIN buscar POR id..................................
    }
}
