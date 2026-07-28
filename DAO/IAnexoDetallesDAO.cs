using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IAnexoDetallesDAO
    {
        //void InsertarLista(List<DRacionElaboradaDetalles> listaDetalles);
        void InsertarUno(DAnexoDetalle anexoDetalle);
        void Editar(DAnexoDetalle anexoDetalle);
        DAnexoDetalle ObtenerPorId(int id);

        (List<DAnexoDetalle> lista, string error) ListaXIdAnexo(int idAnexo);        
        (List<DAnexoDetalle> lista, string error) ListaXFechaAnexo(string fechaInicio, string fechaFin);
        (List<DAnexoDetalle> lista, string error) ListaTodos();

        void EliminarAnexosCargados(int idAnexo);

    }
}
