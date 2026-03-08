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
    public class NRacionSolicitada
    {
        private readonly IRacionSolicitadaDAO racionSolicitadaDAO;
        public NRacionSolicitada()
        {
            racionSolicitadaDAO = new RacionSolicitadaDaoImplement();
        }

        public void CrearRacionSolicitada(DRacionSolicitada racionSolicitada)
        {
            racionSolicitadaDAO.Insertar(racionSolicitada);

        }

        public void Editar(DRacionSolicitada solicitada)
        {
            racionSolicitadaDAO.Editar(solicitada);

        }

        public (List<DRacionSolicitada> lista, string error) ListarTodos()
        {
            return racionSolicitadaDAO.ListaTodos();
        }

        //LISTA POR fecha
        public (List<DRacionSolicitada> lista, string error) ListaXFecha(string fechaInicio, string fechaFin)
        {
            return racionSolicitadaDAO.ListaXFecha(fechaInicio, fechaFin);
        }
        //FIN LISTA POR FECHA..................................
    }
}
