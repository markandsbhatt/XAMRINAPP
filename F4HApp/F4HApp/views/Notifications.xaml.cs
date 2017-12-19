using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace F4HApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notifications : ContentPage
    {
        F4HApp.dataservice.DSMaster dsMaster;

        public List<models.NotificationPageItem> FilteredNotifications = new List<models.NotificationPageItem>
        {
            new models.NotificationPageItem
            {
                Notification = "Todays offer buy 5 Kg of sicon sugar to earn 100 point offer valid for today.",
                NotificationDate = "08 Nov 2017"
            },
            new models.NotificationPageItem
            {
                Notification = "Launching New Health insurance plan you can avail by redeeming 2000 points.",
                 NotificationDate = "09 Nov 2017"
            },
            new models.NotificationPageItem
            {
                Notification = "Our website will be under maintainence from 01 nov 2017 - 02 nov 2017",
                 NotificationDate = "10 Nov 2017"
            }
        };


        public Notifications()
        {
            InitializeComponent();
            //NotificationListView.ItemsSource = FilteredNotifications;

            Task.Run(async () => {

                await GetNotifications();

            });
        }

        private async Task<string> GetNotifications()
        {
            string result = "Success";
            dsMaster = new F4HApp.dataservice.DSMaster();
            string Response = await dsMaster.GetNotificationList();

            try
            {
                var tr = JsonConvert.DeserializeObject<List<NotificationtResponseObject>>(Response);
                ObservableCollection<NotificationtResponseObject> trends = new ObservableCollection<NotificationtResponseObject>(tr);

                Device.BeginInvokeOnMainThread(() =>
                {
                    NotificationListView.ItemsSource = trends;
                });

            }
            catch (Exception ex)
            {
                string rr = ex.Message;
            }

            return result;
        }

    }
}