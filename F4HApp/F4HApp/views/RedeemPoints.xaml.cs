using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace F4HApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RedeemPoints : ContentPage
    {
        F4HApp.dataservice.DSLogin dsLogin;
        F4HApp.dataservice.DSMaster dsMaster;
        F4HApp.dataservice.DSTransactions dsTransactions;

        public RedeemPoints()
        {
            InitializeComponent();

            lblMemberName.Text = App.MemberName.ToString();

            Task.Run(async () => {

                await SetMemberPoints();
                await GetRedeemList();

            });

            lblPlans.GestureRecognizers.Add((new TapGestureRecognizer
            {


                Command = new Command(() => OnAboutUSPlansMark())
            }));

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
                int BalancePoint = Earned - (Redeemed + Transfered);

                Device.BeginInvokeOnMainThread(() =>
                {
                    lblAvailPoints.Text = BalancePoint.ToString();
                });

                //lblEarnedPoints.Text = Status;
            }
            catch (Exception ex)
            {
                string rr = ex.Message;
            }

            return result;

        }

        private async Task<string> GetRedeemList()
        {
            string Response = await RedeemList();
            return Response;
        }

        private async Task<string> RedeemList()
        {
            string result = "Success";
            dsMaster = new F4HApp.dataservice.DSMaster();
            string Response = await dsMaster.GetRedeemSchemeList();

            try
            {
                var tr = JsonConvert.DeserializeObject<List<RedeemResponseObject>>(Response);
                ObservableCollection<RedeemResponseObject> trends = new ObservableCollection<RedeemResponseObject>(tr);

                Device.BeginInvokeOnMainThread(() =>
                {
                    LVRedeem.ItemsSource = trends;

                    if (trends.Count == 0)
                    {
                        SchemeMsg.IsVisible = true;
                    }
                    else
                    {
                        SchemeMsg.IsVisible = false;
                    }

                });



            }
            catch (Exception ex)
            {
                string rr = ex.Message;
            }

            return result;
        }
        public async void Math_Clicked(object sender, EventArgs e)
        {
        
            await DisplayAlert("asd", "asd", "Ok");
        }
        public async void SelectClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            string EPointID = item.CommandParameter.ToString();
            int AvailablePoints = 0;
            bool APoints = int.TryParse(lblAvailPoints.Text, out AvailablePoints);

            dsTransactions = new F4HApp.dataservice.DSTransactions();
            string result = await dsTransactions.RedeemPoints(App.MemberID, EPointID, AvailablePoints);

            EarnResponseObject m = JsonConvert.DeserializeObject<EarnResponseObject>(result.Replace("[", "").Replace("]", ""));
            string Status = m.Status.ToString();            
            string Msg = m.Msg;

            if (Status == "Success")
            {
                await SetMemberPoints();
                await DisplayAlert("Food4Health", "Congratulations! Points redeemed successfully", "Done");
            }
            else
            {
                await DisplayAlert("Food4Health", Msg, "Done");
            }
        }

        private async void OnAboutUSPlansMark()
        {
            dsLogin = new F4HApp.dataservice.DSLogin();
            string result = await dsLogin.GetAboutUSPlans("");
            string AboutUsMessage = "";
            try
            {
                AboutHelathPlanResponceObject m = JsonConvert.DeserializeObject<AboutHelathPlanResponceObject>(result.Replace("[", "").Replace("]", ""));
                AboutUsMessage = m.AboutHelathPlanMessage.ToString();

                await DisplayAlert("About Health Plans", AboutUsMessage, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Conection Error", ex.ToString(), "Error");
            }

        }
    }
}