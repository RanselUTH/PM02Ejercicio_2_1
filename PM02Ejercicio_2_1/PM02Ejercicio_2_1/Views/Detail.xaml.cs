
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM02Ejercicio_2_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detail : ContentPage
    {
        public Detail(Moldes.Country country)
        {
            InitializeComponent();

            string detalle = country.Population + " | ";

            for (int i = 0; i < country.Languages.Count; i++)
            {
                detalle += country.Languages[i] + ((i < country.Languages.Count - 1) ? ", " : " | ");
            }

            for (int i = 0; i < country.Currencies.Count; i++)
            {
                detalle += country.Currencies[i] + ((i < country.Currencies.Count - 1) ? ", " : "");
            }

            Map map = new Map(new MapSpan(new Position(country.Lat, country.Lng), 10, 10));
            map.Pins.Add(new Pin
            {
                Label = "Detalle",
                Address = detalle,
                Type = PinType.Generic,
                Position = new Position(country.Lat, country.Lng)
            });
            sltMain.Children.Add(map);
        }
    }
}