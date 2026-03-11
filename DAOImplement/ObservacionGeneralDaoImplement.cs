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
    public class ObservacionGeneralDaoImplement : IObservacionGeneralDAO
    {
        public void Insertar(DObservacionGeneral observacionGeneral)
        {
            observacionGeneral.usuario_id = 1;
            observacionGeneral.vigente = true;

            try
            {

                using (var db = new MiDbContext())
                using (var tran = db.Database.BeginTransaction())
                {
                    db.ObservacionesGeneral.Add(observacionGeneral);
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

        //EDITAR
        public void Editar(DObservacionGeneral observacionGeneral)
        {
            try
            {
                using (var db = new MiDbContext())
                {
                    // Verificar si existe
                    var existente = db.ObservacionesGeneral.Find(observacionGeneral.id_observacion_general);
                    if (existente == null)
                        throw new Exception("La observacion que intenta editar no existe.");

                    // Actualizar manualmente los campos
                    existente.observacion = observacionGeneral.observacion;
                    existente.historial = existente.historial + " modificado en fecha: " + DateTime.Now.ToString("dd/MM/yyyy") + " //";
                    existente.vigente = observacionGeneral.vigente;
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
        //FIN EDITAR....................................................

        //LISTA TODOS GENERAL
        public (List<DObservacionGeneral> lista, string error) ListaTodos()
        {
            List<DObservacionGeneral> lista = new List<DObservacionGeneral>();

            try
            {
                using (var db = new MiDbContext())
                {
                    lista = db.ObservacionesGeneral
                     .Include(s => s.usuario)
                     .Where(s => s.vigente == true)
                     .OrderBy(s => s.id_observacion_general)   // Orden ascendente
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
        //FIN LISTA TODOS GENERAL........................................

        //BUSCAR POR ID OBSERVACION
        public (DObservacionGeneral observacionGeneral, string error) ObtenerPorId(int idObservacion)
        {
            DObservacionGeneral observacionGeneral = new DObservacionGeneral();

            try
            {
                using (var db = new MiDbContext())
                {
                    observacionGeneral = db.ObservacionesGeneral
                     .Include(s => s.usuario)
                     .OrderBy(s => s.id_observacion_general)   // Orden ascendente
                     .FirstOrDefault();

                    return (observacionGeneral, null);
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
        //FIN BUSCAR POR ID OBSERVACION.............................................
    }
}
