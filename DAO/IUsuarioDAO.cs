using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public interface IUsuarioDAO
    {
        void Insertar(DUsuario usuario);
        void Editar(DUsuario usuario);
        void EditarContrasenia(DUsuario usuario);
        DUsuario ObtenerPorId(int id);
        List<DUsuario> ListaActivos();
        (List<DUsuario> lista, string error) ListaTodos();
    }
}
