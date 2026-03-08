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
    public class NUsuario
    {
        private readonly IUsuarioDAO usuarioDAO;
        public NUsuario()
        {
            usuarioDAO = new UsuarioDAOImplement();
        }

        public void CrearUsuario(DUsuario usuario)
        {
            usuarioDAO.Insertar(usuario);
            
        }

        public void EditarUsuario(DUsuario usuario)
        {
            usuarioDAO.Editar(usuario);
            
        }

        public void EditarContrasenia(DUsuario usuario)
        {
            usuarioDAO.EditarContrasenia(usuario);

        }

        public (List<DUsuario> lista, string error) ListarTodos()
        {
            return usuarioDAO.ListaTodos();
        }
    }
}
