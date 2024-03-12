using EmelyanovDiplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EmelyanovApp.Pages.Login;

namespace EmelyanovApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateProviderPage : ContentPage
    {
        static HttpClient client;
        public CreateProviderPage()
        {
            InitializeComponent();
            client = new HttpClient();
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Provider newProvider = new Provider()
            {
                Name = txbName.Text,
                INN = txbINN.Text,
                Login = txbLogin.Text,
                Password = txbPassword.Text
            };
            string path = "http://10.0.2.2:5223/CreateProvider";
            var json = JsonConvert.SerializeObject(newProvider);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await client.PostAsync(path, content);
            if(resp.IsSuccessStatusCode)
            {
                await DisplayAlert("Уведомление", $"{newProvider.Name}, вы успешно зарегистрировались в качестве поставщика", "Ok");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                 await DisplayAlert("wqfewfe", resp.StatusCode.ToString(), "oK");
            }



        }
    }
}