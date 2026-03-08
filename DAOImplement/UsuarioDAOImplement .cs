using CapaDatos;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using Org.BouncyCastle.Crypto.Generators;

namespace DAOImplement
{
    public class UsuarioDAOImplement : IUsuarioDAO
    {
        //INSERTAR
        public void Insertar(DUsuario usuario)
        {
            try
            {

                // ----- HASH DE CONTRASEÑA ANTES DE GUARDAR -----
                if (!string.IsNullOrWhiteSpace(usuario.contrasenia))
                {
                    usuario.contrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.contrasenia);

                }


                using (var db = new MiDbContext())
                {
                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                string mensaje = ex.InnerException?.Message ?? ex.Message;
                string msg = ex.ToString();
                // Detectar error de duplicado buscando "Duplicate entry" o "errno 1062"
                if (msg.Contains("Duplicate entry") || msg.Contains("errno 1062"))
                {
                    string campo = "desconocido";
                    string valor = "desconocido";

                    // Extraer valor duplicado
                    int startValue = msg.IndexOf("Duplicate entry '") + "Duplicate entry '".Length;
                    int endValue = msg.IndexOf("'", startValue);
                    if (startValue >= 0 && endValue > startValue)
                    {
                        valor = msg.Substring(startValue, endValue - startValue);
                    }

                    // Extraer nombre del índice
                    int indexKey = msg.IndexOf("for key '") + "for key '".Length;
                    int endIndex = msg.IndexOf("'", indexKey);
                    if (indexKey >= 0 && endIndex > indexKey)
                    {
                        campo = msg.Substring(indexKey, endIndex - indexKey);

                        if (campo.EndsWith("_UNIQUE"))
                            campo = campo.Substring(0, campo.Length - "_UNIQUE".Length);
                    }

                    throw new Exception($"No se puede insertar: el campo '{campo}' ya existe con el valor '{valor}'.");
                }

                // Otros errores
                throw new Exception("Error al insertar usuario: " + mensaje);
            }
        }
        //FIN INSERTAR.............................................

        //EDITAR
        public void Editar(DUsuario usuario)
        {
            try
            {
                using (var db = new MiDbContext())
                {
                    // Verificar si existe
                    var existente = db.Usuarios.Find(usuario.id_usuario);
                    if (existente == null)
                        throw new Exception("El usuario que intenta editar no existe.");

                    // Actualizar manualmente los campos
                    existente.nombre = usuario.nombre;
                    existente.apellido = usuario.apellido;
                    existente.dni = usuario.dni;
                    existente.legajo = usuario.legajo;
                    existente.rol_id = usuario.rol_id;
                    existente.is_activo = usuario.is_activo;
                    // Agregar acá todos los campos que quieras actualizar

                    db.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                string mensaje = ex.InnerException?.Message ?? ex.Message;
                string msg = ex.ToString();

                // Detectar duplicado MySQL (errno 1062)
                if (msg.Contains("Duplicate entry") || msg.Contains("errno 1062"))
                {
                    string campo = "desconocido";
                    string valor = "desconocido";

                    // Extraer valor duplicado
                    int startValue = msg.IndexOf("Duplicate entry '") + "Duplicate entry '".Length;
                    int endValue = msg.IndexOf("'", startValue);
                    if (startValue >= 0 && endValue > startValue)
                    {
                        valor = msg.Substring(startValue, endValue - startValue);
                    }

                    // Extraer índice responsable
                    int indexKey = msg.IndexOf("for key '") + "for key '".Length;
                    int endIndex = msg.IndexOf("'", indexKey);
                    if (indexKey >= 0 && endIndex > indexKey)
                    {
                        campo = msg.Substring(indexKey, endIndex - indexKey);

                        if (campo.EndsWith("_UNIQUE"))
                            campo = campo.Substring(0, campo.Length - "_UNIQUE".Length);
                    }

                    throw new Exception($"No se puede actualizar: el campo '{campo}' ya existe con el valor '{valor}'.");
                }

                throw new Exception("Error al actualizar usuario: " + mensaje);
            }
        }
        //FIN EDITAR................................................

        //EDITAR CONTRASEÑA
        public void EditarContrasenia(DUsuario usuario)
        {
            try
            {
                using (var db = new MiDbContext())
                {
                    // Verificar si existe
                    var existente = db.Usuarios.Find(usuario.id_usuario);
                    if (existente == null)
                        throw new Exception("El usuario que intenta editar no existe.");


                    // ----- HASH DE CONTRASEÑA ANTES DE GUARDAR -----
                    if (!string.IsNullOrWhiteSpace(usuario.contrasenia))
                    {
                        existente.contrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.contrasenia);

                    }
                    //existente.contrasenia = usuario.contrasenia;                    
                    // Agregar acá todos los campos que quieras actualizar

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // 🟦 Detecta si realmente es error de conexión MySQL
                if (ErrorHelper.EsErrorDeConexion(ex))
                {
                    throw new Exception("No hay conexión con el servidor de base de datos. ");
                }

                // Si no es mysqlEx → error inesperado
                throw new Exception("Error al actualizar usuario: " + ex.Message);
            }
        }
        //FIN EDITAR CONTRASEÑA

        public List<DUsuario> ListaActivos()
        {
            throw new NotImplementedException();
        }

        //LISTAR TODOS
        public (List<DUsuario> lista, string error) ListaTodos()
        {
            List<DUsuario> lista = new List<DUsuario>();
            try
            {
                using (var db = new MiDbContext())
                {
                    lista = db.Usuarios
                     .Include(u => u.rol)
                     .OrderBy(u => u.apellido)   // Orden ascendente
                     .ToList();

                    return (lista, null);
                }
            }
            catch (Exception ex)
            {
                // 🟦 Detecta si realmente es error de conexión MySQL
                if (ErrorHelper.EsErrorDeConexion(ex))
                {
                    return (null, "No hay conexión con el servidor de base de datos.");
                }

                // Si no es mysqlEx → error inesperado
                return (null, "Error inesperado: " + ex.Message);
            }

        }
        //FIN LISTAR TODOS....................................................
        public DUsuario ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    
    }
}
