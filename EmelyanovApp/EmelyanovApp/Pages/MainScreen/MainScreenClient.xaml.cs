using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmelyanovApp.Pages.MainScreen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainScreenClient : TabbedPage
    {
        public MainScreenClient()
        {
            InitializeComponent();
        }
    }
}