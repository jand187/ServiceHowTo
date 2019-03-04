using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;

namespace ClientConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client1 = new InProcessServiceClient();
            var client2 = new WebApiServiceClient();

            Console.WriteLine(client1.SayHello().Result);
            Console.WriteLine(client2.SayHello().Result);

            Console.ReadLine();
        }
    }

    public class InProcessServiceClient : IServiceClient
    {
        private readonly IService service;

        public InProcessServiceClient()
        {
            this.service = this.service = new Server();
        }

        public async Task<string> SayHello()
        {
            var response = this.service.SayHello();
            return $"{GetType()} {response}";
        }
    }

    public class WebApiServiceClient : IServiceClient
    {
        public async Task<string> SayHello()
        {
            var serverResponse = "nothing yet";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56283/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method  
                var response = await client.GetAsync("api/Hello");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    serverResponse = JsonConvert.DeserializeObject<string>(content);
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }

                return $"{GetType()} {serverResponse}";
            }
        }
    }
}
