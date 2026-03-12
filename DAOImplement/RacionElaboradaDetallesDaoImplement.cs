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
    public class RacionElaboradaDetallesDaoImplement: IRacionElaboradaDetallesDAO
    {
        //INSERTAR DETALLES
        public void InsertarLista(List<DRacionElaboradaDetalles> listaDetalles)
        {
            int idUnidad = listaDetalles[0].unidad_id;
            int idElaborada = listaDetalles[0].racion_elaborada_id;
            (List<DRacionElaboradaDetalles> listaDetallesResponse, string errorResponse) = this.ListaXIdRacionELaboradaXUnidad(idElaborada, idUnidad);
            if(listaDetallesResponse.Count > 0)
            {
                throw new Exception("Ya se encuentra cargado raciones para esta unidad");
            }

            try
            {

                using (var db = new MiDbContext())
                {
                    db.RacionesElaboradasDetalles.AddRange(listaDetalles);
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
        public void InsertarUno(DRacionElaboradaDetalles racionElaborada)
        {
            (List<DRacionElaboradaDetalles> listaDetallesResponse, string errorResponse) = this.ListaXIdRacionELaboradaXUnidad(racionElaborada.racion_elaborada_id, racionElaborada.unidad_id);

            if (listaDetallesResponse == null)
            {
                throw new Exception("Error: " + errorResponse);
            }

            if (listaDetallesResponse.Count > 0)
            {
                foreach (DRacionElaboradaDetalles detalle in listaDetallesResponse)
                {
                    if (racionElaborada.tipo_menu_id == detalle.tipo_menu_id)
                    {
                        throw new Exception("Ya se encuentra cargado este menu en esta elaborada.");
                    }
                }
            }

            try
            {

                using (var db = new MiDbContext())
                using (var tran = db.Database.BeginTransaction())
                {
                    db.RacionesElaboradasDetalles.Add(racionElaborada);
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
        //FIN INSERTAR UNO......................................................

        //EDITAR
        public void Editar(DRacionElaboradaDetalles racionElaborada)
        {
            try
            {
                using (var db = new MiDbContext())
                {
                    // Verificar si existe
                    var existente = db.RacionesElaboradasDetalles.Find(racionElaborada.id_racion_elaboradas_detalle);
                    if (existente == null)
                        throw new Exception("El detalle que intenta editar no existe.");

                    // Actualizar manualmente los campos
                    existente.almuerzo = racionElaborada.almuerzo;
                    existente.cena = racionElaborada.cena;
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
        //FIN EDITAR...........................

        public DRacionElaboradaDetalles ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public (List<DRacionElaboradaDetalles> lista, string error) ListaTodos()
        {
            throw new NotImplementedException();
        }

        //LISTA POR IDRACIONSELABORADA
        public (List<DRacionElaboradaDetalles> lista, string error) ListaXIdRacionELaborada(int idRacionElaborada)
        {
            List<DRacionElaboradaDetalles> lista = new List<DRacionElaboradaDetalles>();
            try
            {
                using (var db = new MiDbContext())
                {

                    lista = db.RacionesElaboradasDetalles
                    .Include(s => s.racion_elaborada)
                    .Include(s => s.sap)
                    .Include(s => s.unidad)
                    .Include(s => s.tipo_menu)
                    .Include(s => s.usuario)
                    .Where(s => s.racion_elaborada_id == idRacionElaborada)
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
        //FIN LISTA POR IDRACIONSELABORADA................................................

        //LISTA POR IDRACIONSELABORADA Y UNIDAD
        public (List<DRacionElaboradaDetalles> lista, string error) ListaXIdRacionELaboradaXUnidad(int idRacionElaborada, int idUnidad)
        {
            List<DRacionElaboradaDetalles> lista = new List<DRacionElaboradaDetalles>();
            try
            {
                using (var db = new MiDbContext())
                {

                    lista = db.RacionesElaboradasDetalles
                    .Include(s => s.racion_elaborada)
                    .Include(s => s.sap)
                    .Include(s => s.unidad)
                    .Include(s => s.tipo_menu)
                    .Include(s => s.usuario)
                    .Where(s => s.racion_elaborada_id == idRacionElaborada && s.unidad_id == idUnidad)
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
        //FIN LISTA POR IDRACIONSELABORADA Y UNIDAD........................................

        public void EliminarRacionesCargadas(int idRacionElaborada)
        {
            try
            {
                using (var db = new MiDbContext())
                {
                    db.Database.ExecuteSqlCommand(
                        "DELETE FROM raciones_elaboradas_detalles WHERE racion_elaborada_id = @id",
                        new MySqlParameter("@id", idRacionElaborada)
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
