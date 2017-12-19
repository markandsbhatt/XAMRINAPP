using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace F4HApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerSupport : ContentPage
    {
        F4HApp.dataservice.DSMaster dsMaster;

        public CustomerSupport()
        {
            InitializeComponent();
        }

        private async void OnButtonSubmit(object sender, EventArgs e)
        {
            var Query = txtQuery.Text;

            if (Query == "")
            {
                await DisplayAlert("Validation Error", "Please enter your query", "Re-try");
                return;
            }

            dsMaster = new F4HApp.dataservice.DSMaster();
            string result = await dsMaster.SupportQuery(App.MemberID, txtQuery.Text);

            RegistrationResponseObject m = JsonConvert.DeserializeObject<RegistrationResponseObject>(result.Replace("[", "").Replace("]", ""));
            string Status = m.Status.ToString();
            string Msg = m.Msg;

            if (Status == "Success")
            {
                await DisplayAlert("Food4Health", "Your query has been successfuly submitted. You will be able to check our response in the Support Forum section. Thank you", "Done");
                txtQuery.Text = "";
            }
            else
            {
                await DisplayAlert("Food4Health Error", Msg, "Re-try");
            }
                
        }
    }
}