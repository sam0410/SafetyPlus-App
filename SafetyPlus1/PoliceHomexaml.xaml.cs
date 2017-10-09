using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using System.Device.Location;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Storage.Streams;
using Microsoft.WindowsAzure.MobileServices;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SafetyPlus1
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public class UserLocation
    {
        public string Id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public long aadhar { get; set; }
    }
    public sealed partial class PoliceHomexaml : Page
    {
        private DependencyObject CreatePushPin()
        {
            Polygon polygon = new Polygon();
            polygon.Points.Add(new Point(0, 0));
            polygon.Points.Add(new Point(0, 50));
            polygon.Points.Add(new Point(25, 0));
            polygon.Fill = new SolidColorBrush(Colors.Red);

            return polygon;

        }
        private async void MessageBox(string message)
        {
            var dialog = new MessageDialog(message.ToString());
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () => await dialog.ShowAsync());
        }
        public PoliceHomexaml()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            MyMap.MapServiceToken = "AugP_vdoWVImJJtP0xKPiCV67t05F56dykjeZiJYgX6ZEuXbmSmewn8kSrbNuGtk";
            /// <summary>
            var geolocator = new Geolocator();
            geolocator.DesiredAccuracy = Windows.Devices.Geolocation.PositionAccuracy.High;

            try
            {

                Geoposition position = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));


                /*M0yMap.Center = position.Coordinate.Point;
                MyMap.ZoomLevel = 15;
                */
                var pushpin = CreatePushPin();
                MyMap.Children.Add(pushpin);
                var location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = position.Coordinate.Latitude,
                    Longitude = position.Coordinate.Longitude
                });
                MapControl.SetLocation(pushpin, location);
                MapControl.SetNormalizedAnchorPoint(pushpin, new Point(0.0, 1.0));
                await MyMap.TrySetViewAsync(location, 18D, 0, 0, MapAnimationKind.Bow);


                this.MyMap.ZoomLevel = 13;



                //get table values

                List<UserLocation> z = await App.MobileService.GetTable<UserLocation>().ToListAsync();
                foreach (UserLocation x in z)
                {

                    Geopoint myPoint = new Geopoint(new BasicGeoposition() { Latitude = x.latitude, Longitude = x.longitude });
                    //create POI
                    long aid = x.aadhar;
                    MapIcon myPOI = new MapIcon { Location = myPoint, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = aid.ToString(), ZIndex = 0 };
                    MyMap.MapElements.Add(myPOI);
                }
                mySlider.Value = MyMap.ZoomLevel;

            }
            catch (UnauthorizedAccessException)
            {
                MessageBox("Location serices Turned off");
            }

            //   MyMap.Center = new GeoCoordinate(23, 23);

            //base.OnNavigatedTo(e);

        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void MyMap_ZoomLevelChanged(MapControl sender, object args)
        {
            if (MyMap != null)
                mySlider.Value = sender.ZoomLevel;
        }

        private void ZoomValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (MyMap != null)
                MyMap.ZoomLevel = e.NewValue;
        }
        private async void UpdateMap()
        {
            List<UserLocation> z = await App.MobileService.GetTable<UserLocation>().ToListAsync();
            foreach (UserLocation x in z)
            {

                Geopoint myPoint = new Geopoint(new BasicGeoposition() { Latitude = x.latitude, Longitude = x.longitude });
                //create POI
                long aid = x.aadhar;
                MapIcon myPOI = new MapIcon { Location = myPoint, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = aid.ToString(), ZIndex = 0 };
                MyMap.MapElements.Add(myPOI);
               
            }
            this.Frame.Navigate(typeof(PoliceHomexaml));

        }
        private async void appBarButton_Click(object sender, RoutedEventArgs e)
        {
            IMobileServiceTable<UserLocation> user = App.MobileService.GetTable<UserLocation>();
            List<UserLocation> z = await App.MobileService.GetTable<UserLocation>().ToListAsync();
            foreach (UserLocation x in z)
            {
                if (x.aadhar == long.Parse(aid.Text))
                {
                    await user.DeleteAsync(x);
                    UpdateMap();
                }

            }
        }

    }
}
