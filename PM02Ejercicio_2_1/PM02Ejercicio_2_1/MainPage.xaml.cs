using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PM02Ejercicio_2_1.Moldes;


// Africa, Americas, Asia, Europe, Oceania
// https://restcountries.com/v3.1/region/{region}

namespace PM02Ejercicio_2_1
{
    public partial class MainPage : ContentPage
    {

        private List<Moldes.Country> countries;
        private string region;
        public MainPage()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            pckRegion.SelectedIndex = 0;
        }

        public async Task GetJsonCountries()
        {
            countries = new List<Moldes.Country>();
            var uri = new Uri("https://restcountries.com/v3.1/region/" + region);
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var jsonArray = JArray.Parse(json);

                foreach (var item in jsonArray)
                {
                    string name = item["name"]["common"].ToString();
                    string flag = item["flags"]["png"].ToString();
                    double lat = Double.Parse(item["latlng"].First.ToString());
                    double lng = Double.Parse(item["latlng"].Last.ToString());
                    string population = item["population"].ToString();

                    List<string> currencies = new List<string>();
                    foreach (var c in item["currencies"])
                    {
                        currencies.Add(c.First["name"].ToString());
                    }

                    List<string> languages = new List<string>();
                    foreach (var l in item["languages"])
                    {
                        languages.Add(l.First.ToString());
                    }

                    Moldes.Country country = new Moldes.Country();
                    country.Name = name;
                    country.Flag = flag;
                    country.Lat = lat;
                    country.Lng = lng;
                    country.Population = population;
                    country.Currencies = currencies;
                    country.Languages = languages;

                    countries.Add(country);
                }

                lsvCountries.ItemsSource = countries;
            }
        }

        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsvCountries.BeginRefresh();
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex > -1)
            {
                this.region = (string)picker.ItemsSource[selectedIndex];
                await GetJsonCountries();
            }
            lsvCountries.EndRefresh();
        }

        private async void lsvCountries_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Moldes.Country c = e.SelectedItem as Moldes.Country;
            Views.Detail d = new Views.Detail(c);
            await Navigation.PushAsync(d);
        }
    }
}

