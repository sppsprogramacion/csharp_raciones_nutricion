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
    public class NAnexoDetalles
    {
        private readonly IAnexoDetallesDAO anexoDetallesDAO;
        public NAnexoDetalles()
        {
            anexoDetallesDAO = new AnexoDetallesDaoImplement();
        }
                

        public void InsertarUnDetalle(DAnexoDetalle detalle)
        {
            anexoDetallesDAO.InsertarUno(detalle);

        }

        public void EditarDetalle(DAnexoDetalle anexoDetalle)
        {
            anexoDetallesDAO.Editar(anexoDetalle);

        }

        public (List<DAnexoDetalle> lista, string error) ListarXIdAnexo(int idAnexo)
        {
            return anexoDetallesDAO.ListaXIdAnexo(idAnexo);
        }
        

        //LISTA POR fecha
        public (List<DAnexoDetalle> lista, string error) ListaXFechaInicioAnexo(string fechaInicio, string fechaFin)
        {
            return anexoDetallesDAO.ListaXFechaAnexo(fechaInicio, fechaFin);
        }
        //FIN LISTA POR FECHA..................................

        public void EliminarDetalles(int idAnexo)
        {
            anexoDetallesDAO.EliminarAnexosCargados(idAnexo);

        }
    }
}
