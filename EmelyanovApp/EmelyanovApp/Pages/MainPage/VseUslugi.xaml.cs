using EmelyanovApp.Pages.MainScreen;
using EmelyanovDiplom.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmelyanovApp.Pages.MainPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VseUslugi : ContentPage
    {

        public ObservableCollection<Uslugi> MyItems { get; set; }
        List<Uslugi> uslugis = new List<Uslugi>();
        static HttpClient client;

        public VseUslugi()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            uslugis = await GetZayavks();
            MyItems = new ObservableCollection<Uslugi>(uslugis);
            BindingContext = this;
            myColView.ItemsSource = MyItems;
        }
        public async Task<List<Uslugi>> GetZayavks()
        {
            string url = $"http://10.0.2.2:5223/GetAllUslugi";
            string response = await client.GetStringAsync(url);
            var uslugi = JsonConvert.DeserializeObject<IEnumerable<Uslugi>>(response).ToList();
            return uslugi;
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTest = e.NewTextValue;
            if (string.IsNullOrEmpty(searchTest))
            {
                searchTest = string.Empty;
            }
            searchTest = searchTest.ToLowerInvariant();
            var filteredItems = uslugis.Where(u => u.Name.ToLowerInvariant().Contains(searchTest)
                                                            || u.Description.ToLowerInvariant().Contains(searchTest)).ToList();
            if (string.IsNullOrEmpty(searchTest))
            {
                filteredItems = uslugis;
            }
            foreach (var item in uslugis)
            {
                if (!filteredItems.Contains(item))
                {
                    MyItems.Remove(item);
                }
                else if (!MyItems.Contains(item))
                {

                    MyItems.Add(item);
                }
            }

        }

        private void btn_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var uslg = (Uslugi)btn.BindingContext;
            Navigation.PushAsync(new AboutPage(uslg));
        }
    }
}