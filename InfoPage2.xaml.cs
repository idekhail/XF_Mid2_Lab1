using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF_Mid2_Lab1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage2 : ContentPage
    {
        public InfoPage2(Address address)
        {
            InitializeComponent();
            Logout.Clicked  += (s, e) => Navigation.PushAsync(new MainPage());
            Control.Clicked += (s, e) => Navigation.PushAsync(new ControlPage(address));

            Show.Text = address.Id + "\t" + address.Name + "\t" + address.HomeNumber + "\t" + address.City;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            HN.Focus();
        }

        private async void AllPeople_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HN.Text) && (!string.IsNullOrEmpty(City.Text)))
            {
                var addressPeople = await App.AddressSQLite.GetAddressPeopleAsync(HN.Text, City.Text);
                myListView.ItemsSource = addressPeople;
            }
            else
                await DisplayAlert("Error", "HomeNumber or City is empty", "Ok");
        }

        private async void AllAddress_Clicked(object sender, EventArgs e)
        {
           
            string data = "";
            var addressPeople = await App.AddressSQLite.GetAllAddressAsync();
            if (addressPeople != null)
            {
                foreach (var a in addressPeople)
                    data += a.Id + "\t" + a.Name + "\t" + a.HomeNumber + "\t" + a.City + "\n";
                Show.Text = data;
            }
            else
                await DisplayAlert("Error", "Address is null", "Ok");
                      
        }     
    }
}