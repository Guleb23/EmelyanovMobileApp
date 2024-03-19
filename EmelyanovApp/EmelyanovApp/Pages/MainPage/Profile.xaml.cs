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

namespace EmelyanovApp.Pages.MainPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        static HttpClient client;
        public Profile()
        {
            InitializeComponent();
            client = new HttpClient();  
            txbName.Text = ProviderHelper.AutorizeProvider.Name;
            txbLogin.Text = ProviderHelper.AutorizeProvider.Login;
            txbPassword.Text = ProviderHelper.AutorizeProvider.Password;

            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<Uslugi> usl = await GetZayavks(ProviderHelper.AutorizeProvider.Id);
            int count = usl.Count();
            txbCount.Text = count.ToString();
        }

        public async Task<List<Uslugi>> GetZayavks(int id)
        {

            string url = $"http://10.0.2.2:5223/GetUslugiById/{id}";
            string response = await client.GetStringAsync(url);
            var uslugi = JsonConvert.DeserializeObject<IEnumerable<Uslugi>>(response).ToList();
            return uslugi;



        }
    }
}