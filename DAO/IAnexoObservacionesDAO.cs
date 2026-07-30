using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IAnexoObservacionesDAO
    {
        void Insertar(DAnexoObservacion observacion);
        void Editar(DAnexoObservacion observacion);
        (DAnexoObservacion observacion, string error) ObtenerPorId(int idObservacion);
        (List<DAnexoObservacion> lista, string error) ListaTodosXIdAnexo(int idAnexo);
    }
}
