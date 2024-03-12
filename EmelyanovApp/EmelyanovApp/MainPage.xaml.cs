using EmelyanovApp.Pages;
using EmelyanovApp.Pages.Creating;
using EmelyanovApp.Pages.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmelyanovApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void GoCreateProvider(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateProviderPage());
        }

        private void GoCreateClient(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateClientPage());
        }

        private void Login(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}
