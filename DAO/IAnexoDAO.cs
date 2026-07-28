using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IAnexoDAO
    {
        void Insertar(DAnexo anexo);
        void Editar(DAnexo anexo);
        DAnexo ObtenerPorId(int id);
        (List<DAnexo> lista, string error) ListaTodos();
        (List<DAnexo> lista, string error) ListaXFecha(string fechaInicio, string fechaFin);
    }
}
