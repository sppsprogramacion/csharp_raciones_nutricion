using CapaDatos;
using DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOImplement
{
    public class AnexoDaoImplement : IAnexoDAO
    {
        //NUEVO
        public void Insertar(DAnexo anexo)
        {
            anexo.usuario_id = 1;

            try
            {

                using (var db = new MiDbContext())
                using (var tran = db.Database.BeginTransaction())
                {
                    db.Anexos.Add(anexo);
                    db.SaveChanges();
                    tran.Commit();
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
                throw new Exception("Error al insertar: " + mensaje);
            }
        }
        // FIN NUEVO..............................

        //EDITAR
        public void Editar(DAnexo anexo)
        {
            try
            {
                using (var db = new MiDbContext())
                {
                    // Verificar si existe
                    var existente = db.Anexos.Find(anexo.id_anexo);
                    if (existente == null)
                        throw new Exception("El anexo que intenta editar no existe.");

                    // Actualizar manualmente los campos
                    existente.fecha_inicio = anexo.fecha_inicio;
                    existente.descripcion = anexo.descripcion;
                    // Agregar acá todos los campos que quieras actualizar

                    db.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                string mensaje = ex.InnerException?.Message ?? ex.Message;
                string msg = ex.ToString();


                throw new Exception("Error al actualizar el registro: " + mensaje);
            }
        }
        //FIN EDITAR............................................

        public DAnexo ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        // LISTA TODOS
        public (List<DAnexo> lista, string error) ListaTodos()
        {
            List<DAnexo> lista = new List<DAnexo>();
            try
            {
                using (var db = new MiDbContext())
                {
                    lista = db.Anexos
                     .Include(s => s.usuario)
                     .Include(s => s.anexo_detalles)
                     .Include(x => x.anexo_detalles.Select(d => d.anexo_menu))
                     .Include(x => x.anexo_detalles.Select(d => d.anexo_menu.anexo_menu_tipo))
                     .Include(s => s.anexo_observaciones)
                     .Include(t => t.anexo_observaciones.Select(d => d.usuario))
                     .OrderByDescending(s => s.fecha_inicio)   // Orden ascendente
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
        // FIN LISTA TODOS

        //LISTA X FECHA
        public (List<DAnexo> lista, string error) ListaXFecha(string fechaInicio, string fechaFin)
        {
            List<DAnexo> lista = new List<DAnexo>();

            DateTime fechaInicioX;
            DateTime fechaFinX;

            if (!DateTime.TryParse(fechaInicio, out fechaInicioX))
            {
                return (null, "Fecha inicio inválida");
            }

            if (!DateTime.TryParse(fechaFin, out fechaFinX))
            {
                return (null, "Fecha fin inválida");
            }

            try
            {
                using (var db = new MiDbContext())
                {
                    lista = db.Anexos
                     .Include(s => s.usuario)
                     .Include(s => s.anexo_detalles)
                     .Include(x => x.anexo_detalles.Select(d => d.anexo_menu))
                     .Include(x => x.anexo_detalles.Select(d => d.anexo_menu.anexo_menu_tipo))
                     .Include(s => s.anexo_observaciones)
                     .Include(t => t.anexo_observaciones.Select(d => d.usuario))
                     .Where(s => s.fecha_inicio >= fechaInicioX && s.fecha_inicio <= fechaFinX)
                     .OrderByDescending(s => s.fecha_inicio)   // Orden ascendente
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
        //FIN LISTA X FECHA.........................................................
        
    }
}
