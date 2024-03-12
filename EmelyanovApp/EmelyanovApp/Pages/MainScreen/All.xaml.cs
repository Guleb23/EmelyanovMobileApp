using EmelyanovApp.Models;
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

namespace EmelyanovApp.Pages.MainScreen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class All : ContentPage
    {
       
        public ObservableCollection<Uslugi> MyItems { get; set; }
        List<Uslugi> uslugis = new List<Uslugi>();
        static HttpClient client;

        public All()
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

        //public async Task GetCategory()
        //{
        //    string url = $"http://10.0.2.2:5223/GetAllCategory";
        //    string response = await client.GetStringAsync(url);
        //    var uslugi = JsonConvert.DeserializeObject<List<Category>>(response);
        //    category.BindingContext = uslugi;
        //    category.ItemsSource = uslugi;
        //}

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



        //public void Update()
        //{
        //    IEnumerable<Uslugi> list = new List<Uslugi>();
        //    list = uslugis.Where(u => u.Name.Contains(txbSearch.Text)).ToList();
        //    updateUslugisSearch = list.ToList();
        //    txbUslugis.Text = updateUslugisSearch.Count.ToString();
        //}

        //private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    Update();
        //}

        //private void txbPrice_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //    if (string.IsNullOrEmpty(txbPrice.Text))
        //    {
        //        DisplayAlert("Ошибка", "Цена не может быть нулем", "Ок");
        //    }
        //    else
        //    {
        //        updateUslugisSearchPrice = updateUslugisSearch.Where(u => u.Price == decimal.Parse(txbPrice.Text)).ToList();
        //        txbUslugis.Text = updateUslugisSearchPrice.Count.ToString();
        //    }


        //}

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    int search = updateUslugisSearch.Count;
        //    int searchPrice = updateUslugisSearchPrice.Count;
        //    if (search == int.Parse(txbUslugis.Text))
        //    {
        //        Navigation.PushAsync(new NextSearch(updateUslugisSearch));
        //    }
        //    else if (searchPrice == int.Parse(txbUslugis.Text))
        //    {
        //        Navigation.PushAsync(new NextSearch(updateUslugisSearchPrice));
        //    }
        //    else if (uslugis.Count == int.Parse(txbUslugis.Text))
        //    {
        //        Navigation.PushAsync(new NextSearch(uslugis));

        //    }
        //    else if (updateUslugisSearchPriceCategory.Count == int.Parse(txbUslugis.Text))
        //    {
        //        Navigation.PushAsync(new NextSearch(updateUslugisSearchPriceCategory));

        //    }
        //    else
        //    {
        //        DisplayAlert("Ошибка", "Не найдено", "Ок");
        //    }
        //}

        //private void category_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Category selCat = category.SelectedItem as Category;
        //    int categoryId = selCat.Id;
        //    updateUslugisSearchPriceCategory = uslugis.Where(u => u.Category == categoryId).ToList();
        //    txbUslugis.Text = updateUslugisSearchPriceCategory.Count.ToString();
        //}


    }
}