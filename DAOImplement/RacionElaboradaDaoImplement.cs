using CapaDatos;
using CommonCache;
using DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mysqlx.Crud;

namespace DAOImplement
{
    public class RacionElaboradaDaoImplement: IRacionElaboradaDAO
    {
        //NUEVO
        public void Insertar(DRacionElaborada racionElaborada)
        {
            racionElaborada.usuario_id = 1;

            try
            {

                using (var db = new MiDbContext())
                using (var tran = db.Database.BeginTransaction())
                {
                    db.RacionesElaboradas.Add(racionElaborada);
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
        //FIN NUEVO.................................................

        public void Editar(DRacionElaborada racionElaborada)
        {
            try
            {
                using (var db = new MiDbContext())
                {
                    // Verificar si existe
                    var existente = db.RacionesElaboradas.Find(racionElaborada.id_racion_elaborada);
                    if (existente == null)
                        throw new Exception("La racion elaborada que intenta editar no existe.");

                    // Actualizar manualmente los campos
                    existente.fecha_elaborada = racionElaborada.fecha_elaborada;
                    existente.detalles = racionElaborada.detalles;
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
        public DRacionElaborada ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        //LISTAR TODOS
        public (List<DRacionElaborada> lista, string error) ListaTodos()
        {
            List<DRacionElaborada> lista = new List<DRacionElaborada>();
            try
            {
                using (var db = new MiDbContext())
                {
                    lista = db.RacionesElaboradas
                     .Include(s => s.usuario)
                     .OrderByDescending(s => s.fecha_elaborada)   // Orden ascendente
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
        //FIN LISTAR TODOS

        //LISTA POR FECHA
        public (List<DRacionElaborada> lista, string error) ListaXFecha(string fechaInicio, string fechaFin)
        {
            List<DRacionElaborada> lista = new List<DRacionElaborada>();

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
                    lista = db.RacionesElaboradas
                     .Include(s => s.usuario)
                     .Include(s => s.raciones_elaboradas_detalles)
                     .Where(s => s.fecha_elaborada >= fechaInicioX && s.fecha_elaborada <= fechaFinX)
                     .OrderByDescending(s => s.fecha_elaborada)   // Orden ascendente
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
        //FIN LISTA POR FECHA
    }
}
