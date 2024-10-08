using System.Net.Http;
using System;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using ApiStore.ModelsDTO;
using AppFerreteria.ViewModels;

namespace AppFerreteria
{
    public class ApiService
    {
        private static readonly string BASE_URL = "https://localhost:5154/api/";
        static HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(60) };

        public async Task<UsuarioLoginDTO> ValidarLogin(string _email, string _contraseña)
        {
            string FINAL_URL = BASE_URL + "usuarios/ValidarCredencial";
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(new
                    {
                        email = _email,
                        contraseña = _contraseña,
                        // contraseña = Encriptar.GetSHA256(_contraseña), // Descomentar si se quiere encriptar la contraseña
                    }),
                    Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

                var jsonData = await result.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    var responseObject = JsonSerializer.Deserialize<UsuarioLoginDTO>(jsonData,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        });
                    return responseObject!;
                }
                else
                {
                    throw new Exception("Resource Not Found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static async Task<List<Producto>> ObtenerTodos()
        {
            string FINAL_URL = BASE_URL + "productos";

            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Producto>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}