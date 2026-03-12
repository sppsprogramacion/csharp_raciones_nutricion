using CapaDatos;
using DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOImplement
{
    public class RacionSolicitadaDetallesDaoImplement : IRacionSolicitadaDetallesDAO
    {
        //INSERTAR DETALLES
        public void InsertarLista(List<DRacionesSolicitadasDetalles> listaDetalles)
        {
            int idUnidad = listaDetalles[0].unidad_id;
            int idSolicitada = listaDetalles[0].racion_solicitada_id;
            
            (List<DRacionesSolicitadasDetalles> listaDetallesResponse, string errorResponse) = this.ListaXIdRacionSolicitadaXUnidad(idSolicitada, idUnidad);
            if (listaDetallesResponse == null)
            {
                throw new Exception("Error: " + errorResponse);
            }

            if (listaDetallesResponse.Count > 0)
            {
                throw new Exception("Ya se encuentra cargado raciones para esta unidad");
            }

            try
            {

                using (var db = new MiDbContext())
                {
                    db.RacionesSolicitadasDetalles.AddRange(listaDetalles);
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
                throw new Exception("Error al insertar: " + mensaje);
            }
        }
        //FIN INSERTAR DETALLES.....................................................

        //INSERTAR UNO
        public void InsertarUno(DRacionesSolicitadasDetalles racionSolicitada)
        {
            
            (List<DRacionesSolicitadasDetalles> listaDetallesResponse, string errorResponse) = this.ListaXIdRacionSolicitadaXUnidad(racionSolicitada.racion_solicitada_id, racionSolicitada.unidad_id);

            if (listaDetallesResponse == null)
            {
                throw new Exception("Error: " + errorResponse);
            }

            if (listaDetallesResponse.Count > 0)
            {
                foreach (DRacionesSolicitadasDetalles detalle in listaDetallesResponse)
                {
                    if (racionSolicitada.tipo_menu_id == detalle.tipo_menu_id)
                    {
                        throw new Exception("Ya se encuentra cargado este menu en esta solicitada");
                    }
                }
            }

            try
            {

                using (var db = new MiDbContext())
                using (var tran = db.Database.BeginTransaction())
                {
                    db.RacionesSolicitadasDetalles.Add(racionSolicitada);
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
        //INSERTAR UNO...............................................................


        //EDITAR
        public void Editar(DRacionesSolicitadasDetalles racionSolicitada)
        {
            try
            {
                using (var db = new MiDbContext())
                {
                    // Verificar si existe
                    var existente = db.RacionesSolicitadasDetalles.Find(racionSolicitada.id_racion_solicitada_detalle);
                    if (existente == null)
                        throw new Exception("El detalle que intenta editar no existe.");

                    // Actualizar manualmente los campos
                    existente.almuerzo = racionSolicitada.almuerzo;
                    existente.cena = racionSolicitada.cena;
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
        //FIN EDITAR......................................................

        public DRacionesSolicitadasDetalles ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public (List<DRacionesSolicitadasDetalles> lista, string error) ListaTodos()
        {
            throw new NotImplementedException();
        }

        //LISTA POR IDRACIONSOLICITADA Y UNIDAD
        public (List<DRacionesSolicitadasDetalles> lista, string error) ListaXIdRacionSolicitadaXUnidad(int idRacionSolicitada, int idUnidad)
        {
            List<DRacionesSolicitadasDetalles> lista = new List<DRacionesSolicitadasDetalles>();
            try
            {
                using (var db = new MiDbContext())
                {

                    lista = db.RacionesSolicitadasDetalles
                    .Include(s => s.racion_solicitada)
                    .Include(s => s.sap)
                    .Include(s => s.unidad)
                    .Include(s => s.tipo_menu)
                    .Include(s => s.usuario)
                    .Where(s => s.racion_solicitada_id == idRacionSolicitada && s.unidad_id == idUnidad)
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
                Console.WriteLine(ex);
                return (null, "Error inesperado: " + ex.Message);
            }
        }
        //FIN LISTA POR IDRACIONSOLICITADA Y UNIDAD........................................


        //LISTA POR IDRACIONSOLICITADA
        public (List<DRacionesSolicitadasDetalles> lista, string error) ListaXIdRacionSolicitada(int idRacionSolicitada)
        {
            List<DRacionesSolicitadasDetalles> lista = new List<DRacionesSolicitadasDetalles>();
            try
            {
                using (var db = new MiDbContext())
                {

                    lista = db.RacionesSolicitadasDetalles
                    .Include(s => s.racion_solicitada)
                    .Include(s => s.sap)
                    .Include(s => s.unidad)
                    .Include(s => s.tipo_menu)
                    .Include(s => s.usuario)
                    .Where(s => s.racion_solicitada_id == idRacionSolicitada)
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
                Console.WriteLine(ex);
                return (null, "Error inesperado: " + ex.Message);
            }
        }
        //FIN LISTA POR IDRACIONSOLICITADA................................................

        public void EliminarRacionesCargadas(int idRacionSolicitada)
        {
            try
            {
                using (var db = new MiDbContext())
                {
                    db.Database.ExecuteSqlCommand(
                        "DELETE FROM raciones_solicitadas_detalles WHERE racion_solicitada_id = @id",
                        new MySqlParameter("@id", idRacionSolicitada)
                    );
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar los registros: " + ex.Message);
            }
        }
                
    }
}
