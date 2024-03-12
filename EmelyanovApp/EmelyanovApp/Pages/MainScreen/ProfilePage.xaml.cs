using EmelyanovDiplom.Models;
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
    public partial class ProfilePage : ContentPage
    {
        Client newClient = new Client();
        public ProfilePage()
        {
            InitializeComponent();
            newClient = Helper.ClientHelper.AutorizeUser;
            txbFirstName.Text = newClient.FirstName;
            txbLastName.Text = newClient.LastName;
            txbPhone.Text = newClient.Phone;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
            
        }
    }
}