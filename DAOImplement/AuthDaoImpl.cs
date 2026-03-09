using CapaDatos;
using CommonCache;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAOImplement
{
    public class AuthDaoImpl : IAuthDao
    {       
        //LOGIN
        public bool LoginUsuario(DLogin dataLogin)
        {
            //DUsuario dataUsuario = new DUsuario();

            //try
            //{
            //    using (HttpClient httpClient = new HttpClient())
            //    {
            //        // Crear el contenido de la solicitud HTTP
            //        StringContent content = new StringContent(dataLogin, Encoding.UTF8, "application/json");

            //        // Enviar la solicitud HTTP POST
            //        HttpResponseMessage httpResponse = await httpClient.PostAsync(url_base + "/auth/login-restriccion", content);

            //        if (httpResponse.IsSuccessStatusCode)
            //        {
            //            var contentRespuesta = await httpResponse.Content.ReadAsStringAsync();
            //            dataUsuario = JsonConvert.DeserializeObject<DUsuario>(contentRespuesta);
            //            dataLoginResponse = JsonConvert.DeserializeObject<DLoginResponse>(contentRespuesta);
            //            // Puedes procesar el token o el resultado adicional aquí.
            //            // Establecer el usuario actual
            //            CurrentUser.Instance.SetUser(dataUsuario.id_usuario, dataUsuario.apellido, dataUsuario.nombre, dataUsuario.organismo.organismo, dataUsuario.activo, dataUsuario.roles);
            //            SessionManager.Token = dataLoginResponse.token;
            //            return (true, null);
            //        }
            //        else
            //        {
            //            string errorMessage = await httpResponse.Content.ReadAsStringAsync();
            //            var mensaje = JObject.Parse(errorMessage)["message"]?.ToString();
            //            return (false, $"Error de autenticación: {mensaje}");
            //        }


            //    }
            //}
            //catch (HttpRequestException httpRequestException)
            //{
            //    // Capturar errores de la solicitud HTTP
            //    return (false, $"Error de conexión: {httpRequestException.Message}");
            //}
            //catch (Exception ex)
            //{
            //    // Manejo de errores (log, mensaje al usuario, etc.)
            //    Console.WriteLine($"Error: {ex.Message}");
            //    return (false, $"Error inesperado: {ex.Message}");
            //}
            return false;
        }
    }
}
