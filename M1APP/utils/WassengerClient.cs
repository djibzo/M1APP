using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;

public class WassengerClient
{
    private static readonly string apiUrl = "https://api.wassenger.com/v1/messages";
    private static readonly string apiKey = ConfigurationManager.AppSettings["WassengerApiKey"];

    public static async Task<bool> SendWhatsAppMessage(string recipient, string message)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Token", apiKey);

            var payload = new
            {
                phone = recipient,
                message = message
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Message envoyé avec succès !");
                return true;
            }
            else
            {
                Console.WriteLine("Échec de l'envoi : " + response.ReasonPhrase);
                return false;
            }
        }
    }
}
