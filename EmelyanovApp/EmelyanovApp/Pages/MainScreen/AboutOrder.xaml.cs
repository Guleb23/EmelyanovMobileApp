using EmelyanovApp.Interfaces;
using EmelyanovDiplom.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmelyanovApp.Pages.MainScreen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutOrder : ContentPage
    {
        private readonly IFileInterface fileInterface;
        HttpClient client;
        Uslugi usl = new Uslugi();
        public AboutOrder(Uslugi uslugi)
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


        private void OpenPdf()
        {
            string pathToNewFolder = Path.Combine(fileInterface.StorageDirectory, "PdfFiles");
            string pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName($"http://10.0.2.2:5223/GetPDF/{Helper.ClientHelper.AutorizeUser.Id}/{usl.Id}"));
            fileInterface.openFile(pathToNewFile);
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            OpenPdf();
        }
    }
}