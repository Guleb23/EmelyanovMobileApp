using EmelyanovApp.Helper;
using EmelyanovDiplom.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmelyanovApp.Pages.MainScreen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Catalog : ContentPage
    {
        public static List<Uslugi> newUslug = new List<Uslugi>();
        HttpClient client;
        public Catalog()
        {
            InitializeComponent();
            client = new HttpClient();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //List<Order> allOrders = await GetOrders();
            //List<Uslugi> allUslugi = await GetZayavks();
            //List<Uslugi> newUslug = new List<Uslugi>();
            //foreach (var uslug in allUslugi)
            //{
            //    foreach (var order in allOrders)
            //    {
            //        if(uslug.Id == order.IdUslugi)
            //        {
            //           newUslug.Add(uslug);
            //        }
            //    }
            //}
            OrderHelper orderHelper = new OrderHelper();
            newUslug = await orderHelper.GetGetCurrentOrders();
            myColView.ItemsSource = newUslug.ToList();
            //if(newUslug.Count > 0)
            //{
            //    Helper.OrderHelper.Orders = newUslug;
            //}
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

        private void btn_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var uslg = (Uslugi)btn.BindingContext;
            Navigation.PushAsync(new AboutOrder(uslg));
        }
    }
}