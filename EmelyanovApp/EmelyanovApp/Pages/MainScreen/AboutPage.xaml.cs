using EmelyanovApp.Models;
using EmelyanovDiplom.Models;

using System;

using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Xamarin.Forms.PlatformConfiguration;
using System.Drawing;
using EmelyanovApp.Interfaces;
using EmelyanovApp.Models.Events;
using EmelyanovApp.Pages.Login;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

namespace EmelyanovApp.Pages.MainScreen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        private readonly IFileInterface fileInterface;
        HttpClient client;
        Uslugi usl = new Uslugi();
        public AboutPage(Uslugi uslugi)
        {

            InitializeComponent();

            client = new HttpClient();
            Random random = new Random();
            fileInterface = DependencyService.Get<IFileInterface>();

            usl = uslugi;
            string id = "";
            int uslugiId = usl.Category;

            for (int i = 0; i <= 10; i++)
            {
                int a = random.Next(1, 10);
                id += a.ToString();
            }

            txbId.Text = id;
            txbDescription.Text = uslugi.Description;
            txbPrice.Text = uslugi.Price.ToString();

        }

        protected override void OnAppearing()
        {

            base.OnAppearing();



        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Order newOrder = new Order()
            {
                IdClient = Helper.ClientHelper.AutorizeUser.Id,
                IdUslugi = usl.Id,
                DateOformleniya = DateTime.Now
            };
            string path = "http://10.0.2.2:5223/CreateOrder";
            var json = JsonConvert.SerializeObject(newOrder);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await client.PostAsync(path, content);
            if (resp.IsSuccessStatusCode)
            {
                await DisplayAlert("Уведомление", "Вы успешно приебрели услугу", "Ok");
            }
            else
            {
                await DisplayAlert("wqfewfe", resp.StatusCode.ToString(), "oK");
            }
            DownLoadPdfFile();
        }


        private void DownLoadPdfFile()
        {
           
            fileInterface.DownloadFile($"http://10.0.2.2:5223/GetPDF/{Helper.ClientHelper.AutorizeUser.Id}/{usl.Id}", "PdfFiles");
            fileInterface.OnFileDownloaded += FileServices_OnFileDownloaded;
        }


        private void FileServices_OnFileDownloaded(object sender, DownLoadEventArgs e)
        {
            e.FileSaved = true;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}