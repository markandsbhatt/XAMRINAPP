using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace F4HApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MPDetail : ContentPage
    {
        F4HApp.dataservice.DSTransactions dsTransactions;

        public MPDetail()
        {
            InitializeComponent();

            lblMemberName.Text = "Welcome " + App.MemberName.ToString();
            

            Task.Run(async () => {

                await SetMemberPoints();

            });

            

        }

        private async Task<string> SetMemberPoints()
        {
            string result = "Success";
            dsTransactions = new F4HApp.dataservice.DSTransactions();
            string Response = await dsTransactions.GetMemberPoints(App.MemberID);

            try
            {
                MemberPointsResponseObject m = JsonConvert.DeserializeObject<MemberPointsResponseObject>(Response.Replace("[", "").Replace("]", ""));
                string Status = m.Status.ToString();
                string Msg = m.Msg;
                int Earned = m.Earned;
                int Redeemed = m.Redeemed;
                int Transfered = m.Transfered;

                Device.BeginInvokeOnMainThread(() =>
                {
                    lblEarnedPoints.Text = Earned.ToString();
                    lblRedeemPoints.Text = Redeemed.ToString();
                    lblTransferedPoints.Text = Transfered.ToString();
                });

                //lblEarnedPoints.Text = Status;
            }
            catch(Exception ex)
            {
                string rr = ex.Message;
            }

            return result;

        }

        protected  async void OnStart(EventArgs e)
        {
            // stuff
            string Status = "Fail";
            string Msg = "Technical Error";

            dsTransactions = new F4HApp.dataservice.DSTransactions();
            string result = await dsTransactions.GetMemberPoints(App.MemberID);

            try
            {
                RegistrationResponseObject m = JsonConvert.DeserializeObject<RegistrationResponseObject>(result.Replace("[", "").Replace("]", ""));
                Status = m.Status.ToString();
                Msg = m.Msg;
            }
            catch
            {

            }

            if (Status == "Success")
            {
                await DisplayAlert("Congratulations", "Your registration was successful", "Login");
                await Navigation.PushModalAsync(new F4HApp.views.Home());
            }
            else
            {
                await DisplayAlert("Food4Health", Msg, "Login");
            }
            // stuff
        }

        private async void OnbtnTransferPoints(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new views.TransferPoints());            
        }

        private async void OnbtnScanScratchCard(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new views.ScanCard());
        }

        private async void OnbtnRedeemPoints(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new views.RedeemPoints());
        }
    }
}