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
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SafetyPlus1
{
    public class UserTable
    {
        public string Id { get; set; }
        public string name { get; set; }
        public long adhaar { get; set; }
        public long mobile { get; set; }
        public string email { get; set; }
        public string address { get; set; }
    }
        /// <summary>
        /// An empty page that can be used on its own or navigated to within a Frame.
        /// </summary>
        public sealed partial class people_reg : Page
    {
        public people_reg()
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
        private async void InsertPolice()
        {
            if (string.IsNullOrWhiteSpace(mobile.Text) || string.IsNullOrWhiteSpace(name.Text) ||
                string.IsNullOrWhiteSpace(userid.Text) || string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrWhiteSpace(address.Text))
            {
                MessageDialog msgbox1 = new MessageDialog("Please enter all details");
                await msgbox1.ShowAsync();
                this.Frame.Navigate(typeof(people_reg));
            }
            else
            {
                DataBaseController.insertData(long.Parse(userid.Text), "people");
                IMobileServiceTable<UserTable> usertable = App.MobileService.GetTable<UserTable>();
                var user = new UserTable();
                user.name = name.Text;
                user.adhaar = long.Parse(userid.Text);
                user.mobile = long.Parse(mobile.Text);
                user.email = email.Text;
                user.address = address.Text;
                await usertable.InsertAsync(user);
                this.Frame.Navigate(typeof(PeopleHome));
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            InsertPolice();
        }
    }
}
