using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P6EF_APP_RebecaR.Models
{
    public class User
    {
     [JsonIgnore]

        public RestRequest Request { get; set; }
        public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string UserPassword { get; set; } = null!;

    public int StrikeCount { get; set; }

    public string BackUpEmail { get; set; } = null!;

    public string? JobDescription { get; set; }

    public int UserStatusId { get; set; }

    public int CountryId { get; set; }

    public int UserRoleId { get; set; }

        public async Task<bool> AddUserAsync()
        {
            try
            {
                //Este es el sufijo que completa la ruta de consumo del API
                string RouteSufix = string.Format("Users/AddUserFromApp");

                string URL = Services.WebAPIConnection.BaseURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Post);

                //Agregamos la info de seguridad api key
                Request.AddHeader(Services.WebAPIConnection.ApiKeyName, Services.WebAPIConnection.ApiKeyValue);

                //Cuando enviamos objetos hacia el API debemos serializarlos antes

                string SerializedModel = JsonConvert.SerializeObject(this);

                Request.AddBody(SerializedModel);

                //Se ejecuta la llamada 
                RestResponse response = await client.ExecuteAsync(Request);

                //Validamos el resultado del llamado al API
                HttpStatusCode statusCode = response.StatusCode;

                if (response != null && statusCode == HttpStatusCode.Created)
                {

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }

        public async Task<User?> ValidateUserCredentialsAsync(string username, string userpassword)
        {
            try
            {
                string RouteSufix = $"Users/ValidateUser";
                string URL = Services.WebAPIConnection.BaseURL + RouteSufix;

                RestClient client = new RestClient(URL);
                Request = new RestRequest(URL, Method.Post);

                var credentials = new { UserName = username, UserPassword = userpassword };
                string SerializedModel = JsonConvert.SerializeObject(credentials);
                Request.AddBody(SerializedModel);

                Request.AddHeader(Services.WebAPIConnection.ApiKeyName, Services.WebAPIConnection.ApiKeyValue);

                RestResponse response = await client.ExecuteAsync(Request);

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    var user = JsonConvert.DeserializeObject<User>(response.Content);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar las credenciales del usuario.", ex);
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                string RouteSufix = string.Format("Users");

                string URL = Services.WebAPIConnection.BaseURL + RouteSufix;

                Debug.WriteLine($"Request URL: {URL}");

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                Request.AddHeader(Services.WebAPIConnection.ApiKeyName, Services.WebAPIConnection.ApiKeyValue);

                RestResponse response = await client.ExecuteAsync(Request);

                HttpStatusCode statusCode = response.StatusCode;

                if (response != null && statusCode == HttpStatusCode.OK)
                {
                    var list = JsonConvert.DeserializeObject<List<User>>(response.Content);

                    return list;
                }
                else
                {
                    return new List<User>();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }

        public async Task<User?> ConsultarUsuarioPorIDAsync(int idUsuario)
        {
            try
            {
                string RouteSufix = $"Users/{idUsuario}";
                string URL = Services.WebAPIConnection.BaseURL + RouteSufix;
                RestClient client = new RestClient(URL);
                Request = new RestRequest(URL, Method.Get);
                Request.AddHeader(Services.WebAPIConnection.ApiKeyName, Services.WebAPIConnection.ApiKeyValue);

                RestResponse response = await client.ExecuteAsync(Request);
                HttpStatusCode statusCode = response.StatusCode;

                if (response != null && statusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<User>(response.Content);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
