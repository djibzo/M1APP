using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

public class WassengerClient
{
    private static readonly string apiUrl = "https://api.wassenger.com/v1/messages";
    private static readonly string token = ConfigurationManager.AppSettings["WassengerApiKey"];
    /// <summary>
    /// Cette fonction envoie un message whatsapp au numero donne en parametre
    /// </summary>
    /// <param name="numero">Le numero de telephone du destinataire</param>
    /// <param name="message">Le message a envoyer</param>
    /// <returns></returns>
    public bool sendMessage(string numero,string message)
    {
        var client2 = new RestClient(apiUrl);
        var request = new RestRequest("", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Token", token);
        request.AddParameter("application/json",
            $"{{\"phone\":\"{numero}\",\"message\":\"{message}\"}}",
            ParameterType.RequestBody);
        RestResponse response = client2.Execute(request);
        return response.IsSuccessStatusCode;
    }
}
