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

namespace EmelyanovApp.Pages.UslugiPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UslugiPage : ContentPage
    {
        static HttpClient client;
        List<Uslugi> uslugiList;
        public UslugiPage(List<Uslugi> uslugi)
        {
            InitializeComponent();
            client = new HttpClient();
            uslugiList = uslugi;
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetZayavks();


        }

        public async Task GetZayavks()
        {
            myColView.ItemsSource = uslugiList;

        }
    }
}