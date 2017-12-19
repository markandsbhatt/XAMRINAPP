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
    public partial class SupportForum : ContentPage
    {
        F4HApp.dataservice.DSMaster dsMaster;

        public SupportForum()
        {
            InitializeComponent();
            Task.Run(async () => {

                await GetNotifications();

            });
        }
        private async Task<string> GetNotifications()
        {
            string result = "Success";
            dsMaster = new F4HApp.dataservice.DSMaster();
            string Response = await dsMaster.GetNotificationListForMember();

            try
            {
                var tr = JsonConvert.DeserializeObject<List<SupportResponseObject>>(Response);
                ObservableCollection<SupportResponseObject> trends = new ObservableCollection<SupportResponseObject>(tr);

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
