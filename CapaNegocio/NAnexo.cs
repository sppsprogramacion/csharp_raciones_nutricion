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
    public class NAnexo
    {
        private readonly IAnexoDAO anexoDAO;
        public NAnexo()
        {
            anexoDAO = new AnexoDaoImplement();
        }

        public void CrearAnexo(DAnexo anexo)
        {
            anexoDAO.Insertar(anexo);

        }

        public void Editar(DAnexo anexo)
        {
            anexoDAO.Editar(anexo);

        }

        public (List<DAnexo> lista, string error) ListarTodos()
        {
            return anexoDAO.ListaTodos();
        }

        //LISTA POR fecha
        public (List<DAnexo> lista, string error) ListaXFecha(string fechaInicio, string fechaFin)
        {
            return anexoDAO.ListaXFecha(fechaInicio, fechaFin);
        }
        //FIN LISTA POR FECHA..................................
    }
}
