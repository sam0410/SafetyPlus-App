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
using System.Diagnostics;
//using System.Windows.Forms;

//using System.Threading;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SafetyPlus1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
     public class PoliceTable
    {
       public string Id { get; set; }
        public string policestation { get; set; }
        public string userid { get; set; }
        public long mobile { get; set; }
        public string password { get; set; }
       

    }
public sealed partial class Police_reg : Page
    { 
        public Police_reg()
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
            if (string.IsNullOrWhiteSpace(mobile.Text) || string.IsNullOrWhiteSpace(policestation.Text) || string.IsNullOrWhiteSpace(userid.Text))
            {
                MessageDialog msgbox1 = new MessageDialog("Please enter all details");
                await msgbox1.ShowAsync();
                this.Frame.Navigate(typeof(Police_reg));
            }
            else
            {
                DataBaseController.insertData(long.Parse(mobile.Text), "police");
                IMobileServiceTable<PoliceTable> policetable = App.MobileService.GetTable<PoliceTable>();
                var police = new PoliceTable();
                police.policestation = policestation.Text;
                police.userid = userid.Text;
                police.mobile = long.Parse(mobile.Text);
                await policetable.InsertAsync(police);
                this.Frame.Navigate(typeof(PoliceHomexaml));
            }
         }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            InsertPolice();

        }
    }
 
}
    

