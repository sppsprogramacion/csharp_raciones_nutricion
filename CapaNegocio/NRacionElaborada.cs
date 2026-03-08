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
    public class NRacionElaborada
    {
        private readonly IRacionElaboradaDAO racionElaboradaDAO;
        public NRacionElaborada()
        {
            racionElaboradaDAO = new RacionElaboradaDaoImplement();
        }

        public void CrearRacionSolicitada(DRacionElaborada racionelaborada)
        {
            racionElaboradaDAO.Insertar(racionelaborada);

        }

        public void Editar(DRacionElaborada elaborada)
        {
            racionElaboradaDAO.Editar(elaborada);

        }

        public (List<DRacionElaborada> lista, string error) ListarTodos()
        {
            return racionElaboradaDAO.ListaTodos();
        }

        //LISTA POR fecha
        public (List<DRacionElaborada> lista, string error) ListaXFecha(string fechaInicio, string fechaFin)
        {
            return racionElaboradaDAO.ListaXFecha(fechaInicio, fechaFin);
        }
        //FIN LISTA POR FECHA..................................
    }
}
