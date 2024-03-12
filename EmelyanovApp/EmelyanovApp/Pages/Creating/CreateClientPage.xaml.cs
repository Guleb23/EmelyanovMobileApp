using EmelyanovApp.Pages.Login;
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

namespace EmelyanovApp.Pages.Creating
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateClientPage : ContentPage
    {
        static HttpClient client;
        public CreateClientPage()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(txbPassword.Text.Length < 8)
            {
                DisplayAlert("Ошибка", "Длина пароля должна быть минимум 8 символов", "Ок");
            }
            else
            {
                Client newClient = new Client()
                {
                    FirstName = txbName.Text,
                    LastName = txbLastName.Text,
                    Phone = txbPhone.Text,
                    Login = txbLogin.Text,
                    Password = txbPassword.Text

                };
                string path = "http://10.0.2.2:5223/CreateClient";
                var json = JsonConvert.SerializeObject(newClient);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(path, content);
                if (resp.IsSuccessStatusCode)
                {
                    await DisplayAlert("Уведомление", $"{newClient.FirstName}, вы успешно зарегистрировались в качестве заказчика", "Ok");
                    await Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    await DisplayAlert("wqfewfe", resp.StatusCode.ToString(), "oK");
                }
            }
            

        }
    }
}