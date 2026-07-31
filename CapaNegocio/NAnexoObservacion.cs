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
    public class NAnexoObservacion
    {
        private readonly IAnexoObservacionesDAO anexoObservacionDAO;

        public NAnexoObservacion()
        {

            anexoObservacionDAO = new AnexoObservacionesDaoImplement();
        }


        public void CrearObservacion(DAnexoObservacion observacion)
        {
            anexoObservacionDAO.Insertar(observacion);

        }

        public void EditarObservacion(DAnexoObservacion observacion)
        {
            anexoObservacionDAO.Editar(observacion);

        }

        public (List<DAnexoObservacion> lista, string error) ListarTodosXIdAnexo(int idAnexo)
        {
            return anexoObservacionDAO.ListaTodosXIdAnexo(idAnexo);
        }

        //buscar POR id
        public (DAnexoObservacion observacionResponse, string error) BuscarXIdObservacion(int idObservacion)
        {
            return anexoObservacionDAO.ObtenerPorId(idObservacion);
        }
        //FIN buscar POR id..................................
    }
}
