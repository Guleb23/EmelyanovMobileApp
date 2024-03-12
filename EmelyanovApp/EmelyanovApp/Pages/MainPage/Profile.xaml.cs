using EmelyanovApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmelyanovApp.Pages.MainPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            txbName.Text = ProviderHelper.AutorizeProvider.Name;
            txbInn.Text = ProviderHelper.AutorizeProvider.INN;
            
        }
    }
}