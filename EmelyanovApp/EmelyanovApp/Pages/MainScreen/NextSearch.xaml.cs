using EmelyanovApp.Models;
using EmelyanovDiplom.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmelyanovApp.Pages.MainScreen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NextSearch : ContentPage
    {
        static HttpClient client;
        List<Uslugi> uslugiList;
        public NextSearch(List<Uslugi> uslugi)
        {
            InitializeComponent();
            client = new HttpClient();
            uslugiList = uslugi;
        }



        protected override async void OnAppearing()
        {
            base.OnAppearing();
            GetZayavks();
           
            




        }

        public async Task GetZayavks()
        {
            myColView.ItemsSource = uslugiList;
        }




        private void btn_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var uslg = (Uslugi)btn.BindingContext;
            Navigation.PushAsync(new AboutPage(uslg));
        }


    }
}
