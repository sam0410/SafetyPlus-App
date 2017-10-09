using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using Microsoft.WindowsAzure.MobileServices;
using Windows.Services.Maps;
using System.Collections.ObjectModel;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SafetyPlus1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PeopleHome : Page
    {
        ObservableCollection<Student> x;
        public class UserLocation
        {
            public string Id { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public long aadhar { get; set; }

        }
     
        public PeopleHome()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        private async void InsertPolice(double Latitude,double Longitude)
        {
            x = DataBaseController.getValues();
            /*if (x.Count != 0){ }*/
            long aadhar = x[0].Id;
            IMobileServiceTable<UserLocation> userlocation = App.MobileService.GetTable<UserLocation>();
            var user = new UserLocation();
            user.latitude = Latitude;
            user.longitude = Longitude;
            user.aadhar = aadhar;
            await userlocation.InsertAsync(user);
           
        }
        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var geolocator = new Geolocator();
            geolocator.DesiredAccuracy = Windows.Devices.Geolocation.PositionAccuracy.High;

            try
            {
                Geoposition position = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));

                // reverse geocoding
                BasicGeoposition myLocation = new BasicGeoposition
                {
                    Longitude = position.Coordinate.Longitude,
                    Latitude = position.Coordinate.Latitude
                };
                var lat = myLocation.Latitude;
                var lon = myLocation.Longitude;
                
                InsertPolice(lat, lon);
                note.Text = "Your emergency has been notified";

            }
            catch (UnauthorizedAccessException)
            {
                MessageDialog msgbox1 = new MessageDialog("Location services are off");
                await msgbox1.ShowAsync();
            }
         
            
      

        }

    }
}
