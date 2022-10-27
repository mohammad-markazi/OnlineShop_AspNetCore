using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Infrastructure.ZarinPal
{
    public class ZarinPalFactory : IZarinPalFactory
    {
        private readonly IConfiguration _configuration;

        public string Prefix { get; set; }
        private string MerchantId { get;}

        public ZarinPalFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            Prefix = _configuration.GetSection("payment")["method"];
            MerchantId= _configuration.GetSection("payment")["merchant"];
        }

        public async Task<PaymentResponse> CreatePaymentRequest(string amount, string mobile, string email, string description,
             long orderId)
        {
            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);
            var siteUrl = _configuration.GetSection("payment")["siteUrl"];

            using (var client = new HttpClient())
            {
                var parameters = new PaymentRequest
                {
                    Mobile = "09033163381",
                    CallbackURL = $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}",
                    Description = description,
                    Email = "mhmmdmrz@gmail.com",
                    Amount = finalAmount,
                    MerchantID = MerchantId
                };

                var json = JsonConvert.SerializeObject(parameters);

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"https://{Prefix}.zarinpal.com/pg/rest/WebGate/PaymentRequest.json", content);

                string responseBody = await response.Content.ReadAsStringAsync();

                JObject jodata = JObject.Parse(responseBody);

                return JsonConvert.DeserializeObject<PaymentResponse>(responseBody);
            
                

            }

        }

        public async Task<VerificationResponse> CreateVerificationRequest(string authority, string amount)
        {

            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);

            var parameters = new VerificationRequest
            {
                Amount = finalAmount,
                MerchantID = MerchantId,
                Authority = authority
            };

            using (HttpClient client = new HttpClient())
            {

                var json = JsonConvert.SerializeObject(parameters);

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"https://{Prefix}.zarinpal.com/pg/rest/WebGate/PaymentVerification.json", content);

                string responseBody = await response.Content.ReadAsStringAsync();

                
                return JsonConvert.DeserializeObject<VerificationResponse>(responseBody);

            }


            // var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/rest/WebGate/PaymentVerification.json");
            // var request = new RestRequest();
            // request.Method = Method.Post;
            //
            // request.AddHeader("Content-Type", "application/json");
            //
            // amount = amount.Replace(",", "");
            // var finalAmount = int.Parse(amount);
            //
            // request.AddJsonBody(new VerificationRequest
            // {
            //     Amount = finalAmount,
            //     MerchantID = MerchantId,
            //     Authority = authority
            // });
            // var response = client.Execute(request);
            // var jsonSerializer = new RestSharp.Serializers.Json.SystemTextJsonSerializer();
            // return jsonSerializer.Deserialize<VerificationResponse>(response);
        }
    }
}