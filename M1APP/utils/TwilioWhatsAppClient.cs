using System;
using System.Configuration;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

public class TwilioWhatsAppClient
{
    private static readonly string accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
    private static readonly string authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
    private static readonly string twilioNumber = ConfigurationManager.AppSettings["TwilioWhatsAppNumber"];

    public static async Task<bool> SendWhatsAppMessage(string recipient, string message)
    {
        try
        {
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(new PhoneNumber("whatsapp:" + recipient))
            {
                From = new PhoneNumber(twilioNumber),
                Body = message
            };

            var sentMessage = await MessageResource.CreateAsync(messageOptions);

            Console.WriteLine("Message envoyé avec SID : " + sentMessage.Sid);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur Twilio : " + ex.Message);
            return false;
        }
    }
}
