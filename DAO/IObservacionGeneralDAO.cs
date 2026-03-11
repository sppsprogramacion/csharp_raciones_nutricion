using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IObservacionGeneralDAO
    {
        void Insertar(DObservacionGeneral observacionGeneral);
        void Editar(DObservacionGeneral observacionGeneral);
        (DObservacionGeneral observacionGeneral, string error) ObtenerPorId(int idObservacion);
        (List<DObservacionGeneral> lista, string error) ListaTodos();
    }
}
