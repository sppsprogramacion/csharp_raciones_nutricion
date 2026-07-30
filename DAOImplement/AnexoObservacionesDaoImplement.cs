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
    public class AnexoObservacionesDaoImplement : IAnexoObservacionesDAO
    {
        //INSERTAR
        public void Insertar(DAnexoObservacion observacion)
        {
            observacion.usuario_id = 1;
            observacion.vigente = true;
            try
            {

                using (var db = new MiDbContext())
                using (var tran = db.Database.BeginTransaction())
                {
                    db.AnexosObservaciones.Add(observacion);
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
        //FIN INSERTAR..................................................................

        //EDITAR
        public void Editar(DAnexoObservacion observacion)
        {
            try
            {
                using (var db = new MiDbContext())
                {
                    // Verificar si existe
                    var existente = db.ObservacionesElaborada.Find(observacion.id_anexo_observacion);
                    if (existente == null)
                        throw new Exception("La observacion que intenta editar no existe.");

                    // Actualizar manualmente los campos
                    existente.observacion = observacion.observacion;                    
                    existente.vigente = observacion.vigente;
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
        //FIN EDITAR........................................................................

        //LISTA OBSERVACIONES X ID_ANEXO
        public (List<DAnexoObservacion> lista, string error) ListaTodosXIdAnexo(int idAnexo)
        {
            List<DAnexoObservacion> lista = new List<DAnexoObservacion>();

            try
            {
                using (var db = new MiDbContext())
                {
                    lista = db.AnexosObservaciones
                     .Include(s => s.anexo)
                     .Include(s => s.usuario)
                     .Where(s => s.anexo_id == idAnexo && s.vigente == true)
                     .OrderBy(s => s.id_anexo_observacion)   // Orden ascendente
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
        //FIN LISTA OBSERVACIONES X ID_ANEXO.........................................................

        //BUSCAR X ID_ANEXO_OBSERVACION
        public (DAnexoObservacion observacion, string error) ObtenerPorId(int idObservacion)
        {
            DAnexoObservacion observacion = new DAnexoObservacion();

            try
            {
                using (var db = new MiDbContext())
                {
                    observacion = db.AnexosObservaciones
                     .Include(s => s.anexo)
                     .Include(s => s.usuario)
                     .Where(s => s.id_anexo_observacion == idObservacion)
                     .OrderBy(s => s.id_anexo_observacion)   // Orden ascendente
                     .FirstOrDefault();

                    return (observacion, null);
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
    }
}
