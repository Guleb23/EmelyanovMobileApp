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
    public partial class My : ContentPage
    {
        static HttpClient client;
        public int Id;
        public My()
        {
            InitializeComponent();
            client = new HttpClient();
            Id = ProviderHelper.AutorizeProvider.Id;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetZayavks(Id);


        }

        public async Task GetZayavks(int id)
        {

            string url = $"http://10.0.2.2:5223/GetUslugiById/{id}";
            string response = await client.GetStringAsync(url);
            var uslugi = JsonConvert.DeserializeObject<IEnumerable<Uslugi>>(response);
            myColView.ItemsSource = uslugi;
           


        }
    }
}