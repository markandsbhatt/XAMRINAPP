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
    public partial class PointTransactions : ContentPage
    {
        F4HApp.dataservice.DSTransactions dsTransactions;
        private int IntEarned = 0;
        private int IntRedeemed = 0;
        private int IntTransfered = 0;

        public PointTransactions()
        {
            InitializeComponent();
            Earned.IsVisible = false;
            Redeemed.IsVisible = false;
            Transferred.IsVisible = false;

           

            Task.Run(async () => {

                await SetMemberPoints();
                await GetAllTransactionList("All");
                //await GetEarnedTransactionList();
                //await GetRedeemTransactionList();
                //await GetTransferTransactionList();

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
                IntEarned = m.Earned;
                IntRedeemed = m.Redeemed;
                IntTransfered = m.Transfered;

                Device.BeginInvokeOnMainThread(() =>
                {
                    //lblTransPoints.Text = "Total Points Earned : " + IntEarned;
                    lblEarnPoints.Text = "Total Points Earned : " + IntEarned.ToString();
                    lblRedeemPoints.Text = "Total Points Redeemed : " + IntRedeemed.ToString();
                    lblTransferPoints.Text = "Total Points Transferred : " + IntTransfered.ToString();
                });

                //lblEarnedPoints.Text = Status;
            }
            catch (Exception ex)
            {
                string rr = ex.Message;
            }

            return result;

        }

        private async Task<string> GetAllTransactionList(string Mode)
        {
            string result = "Success";
            dsTransactions = new F4HApp.dataservice.DSTransactions();
            string Response = await dsTransactions.AllPointsList(App.MemberID, Mode);

            try
            {
                var tr = JsonConvert.DeserializeObject<List<AllPointListResponseObject>>(Response);
                ObservableCollection<AllPointListResponseObject> trends = new ObservableCollection<AllPointListResponseObject>(tr);

                Device.BeginInvokeOnMainThread(() =>
                {
                    LVAll.ItemsSource = trends;

                });

            }
            catch (Exception ex)
            {
                string rr = ex.Message;
            }

            return result;
        }

        private async Task<string> GetEarnedTransactionList()
        {
            string result = "Success";
            dsTransactions = new F4HApp.dataservice.DSTransactions();
            string Response = await dsTransactions.EarnPointsList(App.MemberID);

            try
            {
                var tr = JsonConvert.DeserializeObject<List<EarnPointListResponseObject>>(Response);
                ObservableCollection<EarnPointListResponseObject> trends = new ObservableCollection<EarnPointListResponseObject>(tr);
                
                Device.BeginInvokeOnMainThread(() =>
                {
                    LVEarned.ItemsSource = trends;
                });

            }
            catch (Exception ex)
            {
                string rr = ex.Message;
            }

            return result;
        }

        private async Task<string> GetRedeemTransactionList()
        {
            string result = "Success";
            dsTransactions = new F4HApp.dataservice.DSTransactions();
            string Response = await dsTransactions.RedeemPointsList(App.MemberID);

            try
            {
                var tr = JsonConvert.DeserializeObject<List<RedeemPointListResponseObject>>(Response);
                ObservableCollection<RedeemPointListResponseObject> trends = new ObservableCollection<RedeemPointListResponseObject>(tr);

                Device.BeginInvokeOnMainThread(() =>
                {
                    LVRedeem.ItemsSource = trends;
                });

            }
            catch (Exception ex)
            {
                string rr = ex.Message;
            }

            return result;
        }

        private async Task<string> GetTransferTransactionList()
        {
            string result = "Success";
            dsTransactions = new F4HApp.dataservice.DSTransactions();
            string Response = await dsTransactions.TransferPointsList(App.MemberID);

            try
            {
                var tr = JsonConvert.DeserializeObject<List<TransferPointListResponseObject>>(Response);
                ObservableCollection<TransferPointListResponseObject> trends = new ObservableCollection<TransferPointListResponseObject>(tr);

                Device.BeginInvokeOnMainThread(() =>
                {
                    LVTransfer.ItemsSource = trends;

                });

            }
            catch (Exception ex)
            {
                string rr = ex.Message;
            }

            return result;
        }

        private async void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = ddlTransactionType.Items[ddlTransactionType.SelectedIndex];

            await GetAllTransactionList(selectedValue);

            //lblTitle.Text = "";
            ////lblTransPoints.Text = "";
            //Earned.IsVisible = false;
            //Redeemed.IsVisible = false;
            //Transferred.IsVisible = false;

            ////DisplayAlert("Notification", selectedValue, "Yes", "No");

            //if (selectedValue == "Earned Transactions")
            //{
            //    lblTitle.Text = "Points Earned Transactions";
            //    //lblTransPoints.Text = "Total Points Earned : " + IntEarned;
            //    Earned.IsVisible = true;
            //}

            //if (selectedValue == "Redeemed Transactions")
            //{
            //    lblTitle.Text = "Points Redeemed Transactions";
            //    //lblTransPoints.Text = "Total Points Redeemed : " + IntRedeemed;
            //    Redeemed.IsVisible = true;
            //}

            //if (selectedValue == "Transfer Transactions")
            //{
            //    lblTitle.Text = "Points Transferred Transactions";
            //    //lblTransPoints.Text = "Total Points Transferred  : " + IntTransfered;
            //    Transferred.IsVisible = true;
            //}
        }
    }
}