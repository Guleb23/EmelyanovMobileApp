using EmelyanovApp.Helper;
using EmelyanovApp.Models;
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

namespace EmelyanovApp.Pages.MainPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateUsluga : ContentPage
    {
        Category selectedCategoriya = new Category();
        static HttpClient client;
        int categoryId;
        public CreateUsluga()
        {
            InitializeComponent();
            client = new HttpClient();

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetCategory();
        }

        public async Task GetCategory()
        {

            string url = $"http://10.0.2.2:5223/GetAllCategory";
            string response = await client.GetStringAsync(url);
            var uslugi = JsonConvert.DeserializeObject<List<Category>>(response);
            category.BindingContext = uslugi;
            category.ItemsSource = uslugi;






        }

        private void category_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCategoriya = category.SelectedItem as Category;
            categoryId = selectedCategoriya.Id;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Uslugi newUsluga = new Uslugi()
            {
                Name = txbName.Text,
                Description = txbDescription.Text,
                Price =decimal.Parse(txbPrice.Text),
                Provider = ProviderHelper.AutorizeProvider.Id,
                Category = categoryId

            };
            string path = "http://10.0.2.2:5223/CreateUslugu";
            var json = JsonConvert.SerializeObject(newUsluga);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await client.PostAsync(path, content);
            if (resp.IsSuccessStatusCode)
            {
                await DisplayAlert("Уведомление", $"{newUsluga.Name}, вы успешно создали услугу", "Ok");
                
            }
            else
            {
                await DisplayAlert("wqfewfe", resp.StatusCode.ToString(), "oK");
            }



        }
    }
}