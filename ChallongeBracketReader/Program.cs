using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChallongeBracketReader
{
    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {


            //check url for errors such as not having challonge or .com or maybe even http(s) and www
            Console.WriteLine("Enter challonge tournament url: \n");
            string url = Console.ReadLine();
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                RunAsync(url).Wait();
                Console.ReadLine();
            }
            
        }

        static async Task RunAsync(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.challonge.com/v1/");
                //"https://PaulDSSB:FVkjpMlzZ3NUcPtc8WKLnRkBfa0PAdhdkmsAAb5c@api.challonge.com/v1/"
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("api/challonge");
                if (response.IsSuccessStatusCode)
                {
                    Product product = await response.Content.ReadAsAsync<Product>();
                    Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
                }
                else
                {
                    Console.WriteLine("\noops, something went wrong");
                    Console.ReadLine();
                }
                // HTTP POST
                var gizmo = new Product() { Name = "Gizmo", Price = 100, Category = "Widget" };
                response = await client.PostAsJsonAsync("api/products", gizmo);
                if (response.IsSuccessStatusCode)
                {
                    Uri gizmoUrl = response.Headers.Location;

                    // HTTP PUT
                    gizmo.Price = 80;   // Update price
                    response = await client.PutAsJsonAsync(gizmoUrl, gizmo);

                    // HTTP DELETE
                    response = await client.DeleteAsync(gizmoUrl);
                }
            }


        }
    }
}
