using EmelyanovDiplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EmelyanovApp.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace EmelyanovApp.Pages.MainScreen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        static HttpClient client;
        Client newClient = new Client();
        public ProfilePage()
        {
            InitializeComponent();
            client = new HttpClient();
            newClient = Helper.ClientHelper.AutorizeUser;
            txbFirstName.Text = newClient.FirstName;
            txbLastName.Text = newClient.LastName;
            txbPhone.Text = newClient.Phone;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int count = await GetCount(newClient.Id);
            txbCount.Text = count.ToString();
            OrderHelper orderHelper = new OrderHelper();
            List<Uslugi> uslugis = new List<Uslugi>();
            uslugis = await orderHelper.GetGetCurrentOrders();
            decimal sum = GetAllPrice(uslugis);
            
            txbPrice.Text = sum.ToString("N0");
            int newSum =(int) Math.Round(sum, 2);
            txbName.Text = newClient.FirstName + " " + newClient.LastName;
        }

        private async void  Button_Clicked(object sender, EventArgs e)
        {
            string url = "http://10.0.2.2:5223/UpdateClient";
            ClientHelper.AutorizeUser.FirstName = txbFirstName.Text;
            ClientHelper.AutorizeUser.LastName = txbLastName.Text;
            ClientHelper.AutorizeUser.Phone = txbPhone.Text;
            var json = JsonConvert.SerializeObject(ClientHelper.AutorizeUser);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await client.PutAsync(url, data);
            txbName.Text = newClient.FirstName + " " + newClient.LastName;

        }

        public async Task<int> GetCount(int userId)
        {
            string url = $"http://10.0.2.2:5223/api/Order/GetCountOrders/{userId}";
            var resp = await client.GetAsync(url);
            if(resp.IsSuccessStatusCode)
            {
                var result = await resp.Content.ReadAsStringAsync();
                return int.Parse(result);
            }
            else
            {
                return 0;
            }
        }


        public decimal GetAllPrice(List<Uslugi> orders)
        {
            decimal sum = 0;
            foreach (var order in orders)
            {
                sum += order.Price;
            }
            return sum;
        }

    }
}