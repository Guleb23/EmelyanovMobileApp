using EmelyanovDiplom.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmelyanovApp.Helper
{
    public class OrderHelper
    {
        static HttpClient client;
        //public static List<Uslugi> Orders { get; set; }

        public OrderHelper()
        {
            client = new HttpClient();
        }


        public async Task<List<Uslugi>> GetGetCurrentOrders()
        {
            List<Order> allOrders = await GetOrders();
            List<Uslugi> allUslugi = await GetZayavks();
            List<Uslugi> newUslug = new List<Uslugi>();
            foreach (var uslug in allUslugi)
            {
                foreach (var order in allOrders)
                {
                    if (uslug.Id == order.IdUslugi)
                    {
                        newUslug.Add(uslug);
                    }
                }
            }
            return newUslug;
        }

        public async Task<List<Order>> GetOrders()
        {
            string url = "http://10.0.2.2:5223/GetAllOrders";
            var resp = await client.GetStringAsync(url);
            var orders = JsonConvert.DeserializeObject<List<Order>>(resp);
            return orders;
        }

        public async Task<List<Uslugi>> GetZayavks()
        {
            string url = $"http://10.0.2.2:5223/GetAllUslugi";
            string response = await client.GetStringAsync(url);
            var uslugi = JsonConvert.DeserializeObject<IEnumerable<Uslugi>>(response).ToList();
            return uslugi;
        }
    }
}
