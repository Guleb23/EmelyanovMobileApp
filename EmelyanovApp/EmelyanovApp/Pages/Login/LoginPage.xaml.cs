using EmelyanovApp.Pages.MainPage;
using EmelyanovDiplom.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EmelyanovApp.Pages.MainPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EmelyanovApp.Helper;

namespace EmelyanovApp.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        static HttpClient client;
        public LoginPage()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string login = txbLog.Text;
            string pass = txbPass.Text;
            string path = $"http://10.0.2.2:5223/api/Provider/LoginUser/{login}/{pass}";
            var autoriseProvider = await client.GetAsync(path);
            if (autoriseProvider.StatusCode == System.Net.HttpStatusCode.NotFound)
            {


                string newPath = $"http://10.0.2.2:5223/api/Client/LoginClient/{login}/{pass}";
                var autoriseUser = await client.GetAsync(newPath);
                if (autoriseUser.IsSuccessStatusCode)
                {
                    var user = await client.GetStringAsync(newPath);
                    var res = JsonConvert.DeserializeObject<Client>(user);
                    ClientHelper.AutorizeUser = res;
                    DisplayAlert("wawe", res.FirstName, "Ok");
                    Navigation.PushAsync(new MainScreen.MainScreenClient());
                }
                else
                {
                    DisplayAlert("Ошибка", "Неправильный логин или пароль", "Ок");
                }

            }
            else
            {

                var response = await client.GetStringAsync(path);
                var result = JsonConvert.DeserializeObject<Provider>(response);
                DisplayAlert("wawe", result.Name, "Ok");
                ProviderHelper.AutorizeProvider = result;
                Navigation.PushAsync(new MainPage.MainPage());

            }
        }
    }
}