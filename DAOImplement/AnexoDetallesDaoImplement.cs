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
    public class AnexoDetallesDaoImplement : IAnexoDetallesDAO
    {
        //INSERTAR UNO
        public void InsertarUno(DAnexoDetalle anexoDetalle)
        {
            
            try
            {

                using (var db = new MiDbContext())
                using (var tran = db.Database.BeginTransaction())
                {
                    db.AnexosDetalles.Add(anexoDetalle);
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
        //FIN INSERTAR UNO...............................................


        //EDITAR
        public void Editar(DAnexoDetalle anexoDetalle)
        {
            try
            {
                using (var db = new MiDbContext())
                {
                    // Verificar si existe
                    var existente = db.AnexosDetalles.Find(anexoDetalle.id_anexo_detalle);
                    if (existente == null)
                        throw new Exception("El detalle que intenta editar no existe.");

                    // Actualizar manualmente los campos
                    existente.cantidad = anexoDetalle.cantidad;
                    existente.factor = anexoDetalle.factor;
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
        //FIN EDITAR..........................................................
        

        public (List<DAnexoDetalle> lista, string error) ListaTodos()
        {
            throw new NotImplementedException();
        }

        //LISTA X FECHA ANEXO
        public (List<DAnexoDetalle> lista, string error) ListaXFechaAnexo(string fechaInicio, string fechaFin)
        {
            List<DAnexoDetalle> lista = new List<DAnexoDetalle>();

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

                    lista = db.AnexosDetalles
                    .Include(s => s.anexo)
                    .Include(s => s.anexo_menu)
                    .Include(s => s.usuario)
                    .Where(s => s.anexo.fecha_inicio >= fechaInicioX && s.anexo.fecha_inicio <= fechaFinX)
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
        //FIN LISTA X FECHA ANEXO

        //LISTA X ID_ANEXO
        public (List<DAnexoDetalle> lista, string error) ListaXIdAnexo(int idAnexo)
        {
            List<DAnexoDetalle> lista = new List<DAnexoDetalle>();
            try
            {
                using (var db = new MiDbContext())
                {

                    lista = db.AnexosDetalles
                    .Include(s => s.anexo)
                    .Include(s => s.anexo_menu)
                    .Include(s => s.usuario)
                    .Where(s => s.anexo_id == idAnexo)
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
        //FIN LISTA X ID_ANEXO

        public (DAnexoDetalle anexoDetalle, string error) ObtenerPorId(int id)
        {
            DAnexoDetalle anexoDetalle = new DAnexoDetalle();

            try
            {
                using (var db = new MiDbContext())
                {
                    anexoDetalle = db.AnexosDetalles
                     .Include(s => s.anexo)
                     .Include(s => s.anexo_menu)
                     .Include(s => s.usuario)
                     .Where(s => s.anexo_id == id)
                     .OrderBy(s => s.id_anexo_detalle)   // Orden ascendente
                     .FirstOrDefault();

                    return (anexoDetalle, null);
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

        public void EliminarAnexosCargados(int idAnexo)
        {
            try
            {
                using (var db = new MiDbContext())
                {
                    db.Database.ExecuteSqlCommand(
                        "DELETE FROM anexos_detalles WHERE anexo_id = @id",
                        new MySqlParameter("@id", idAnexo)
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
