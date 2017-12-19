using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XLabs.Forms.Controls;

namespace F4HApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransferPoints : ContentPage
    {
        F4HApp.dataservice.DSMaster dsMaster;

        F4HApp.dataservice.DSLogin dsLogin;
        List<SelectedModel> obj = new List<SelectedModel>();
        F4HApp.dataservice.DSTransactions dsTransactions;

        public TransferPoints()
        {
            InitializeComponent();

            lblMemberName.Text = App.MemberName.ToString();

            Task.Run(async () => {

                await SetMemberPoints();
                await GetFamilyList();
                await GetFriendList();

            });

            lblAddFamily.GestureRecognizers.Add((new TapGestureRecognizer
            {
                Command = new Command(async o => {
                    await Navigation.PushAsync(new F4HApp.views.ManageFF());
                })
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

        private async Task<string> GetFamilyList()
        {
            string Response = await FamilyList();
            return Response;
        }

        private async Task<string> GetFriendList()
        {
            string Response = await FriendList();
            return Response;
        }

        private async Task<string> FamilyList()
        {
            string result = "Success";
            dsMaster = new F4HApp.dataservice.DSMaster();
            string Response = await dsMaster.GetMemberFFList("Family");

            try
            {
                var tr = JsonConvert.DeserializeObject<List<MemberFFResponseObject>>(Response);
                ObservableCollection<MemberFFResponseObject> trends = new ObservableCollection<MemberFFResponseObject>(tr);

                Device.BeginInvokeOnMainThread(() =>
                {
                    LVFamily.ItemsSource = trends;

                    if (trends.Count == 0)
                    {
                        FamilyMsg.IsVisible = true;
                    }
                    else
                    {
                        FamilyMsg.IsVisible = false;

                        LVFamily.HeightRequest = 50 * trends.Count;
                    }

                });



            }
            catch (Exception ex)
            {
                string rr = ex.Message;
            }

            return result;
        }

        private async Task<string> FriendList()
        {
            string result = "Success";
            dsMaster = new F4HApp.dataservice.DSMaster();
            string Response = await dsMaster.GetMemberFFList("Friends");

            try
            {
                var tr = JsonConvert.DeserializeObject<List<MemberFFResponseObject>>(Response);
                ObservableCollection<MemberFFResponseObject> trends = new ObservableCollection<MemberFFResponseObject>(tr);

                Device.BeginInvokeOnMainThread(() =>
                {
                    LVFriend.ItemsSource = trends;

                    if (trends.Count == 0)
                    {
                        FriendMsg.IsVisible = true;
                        Friend.IsVisible = false;
                    }
                    else
                    {
                        FriendMsg.IsVisible = false;
                        Friend.IsVisible = true;

                        LVFriend.HeightRequest = 50 * trends.Count;
                    }

                });



            }
            catch (Exception ex)
            {
                string rr = ex.Message;
            }

            return result;
        }        

        public void SelectClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            string tt = item.CommandParameter.ToString();
            

            if (item.Image == "unchecked_checkbox26x26.png")
            {
                item.Image = "checked_checkbox26x26.png";
                SelectedModel newObj = new SelectedModel();
                newObj.FFMemberID = tt;
                obj.Add(newObj);
            }
            else
            {
                item.Image = "unchecked_checkbox26x26.png";
                var itemRemove = obj.First(x => x.FFMemberID == tt);
                obj.Remove(itemRemove);

            }

        }

        private async void btnTransfer_Clicked(object sender, EventArgs e)
        {
            int SelectedCount = obj.Count;
            int AvailablePoints = 0;
            bool AP = int.TryParse(lblAvailPoints.Text, out AvailablePoints);
            int TransferPoints = 0;
            bool TP = int.TryParse(txtPoints.Text, out TransferPoints);
            int TotalTransferPoint = SelectedCount * TransferPoints;
            
            if (AvailablePoints < TotalTransferPoint)
            {
                await DisplayAlert("Validation Error", "Sorry you donot have enough points to transfer", "Re-try");
                return;
            }

            if (TransferPoints == 0)
            {
                await DisplayAlert("Validation Error", "Please enter points to transfer", "Re-try");
                return;
            }


            if (SelectedCount == 0)
            {
                await DisplayAlert("Validation Error", "Please select atleast one member from the list", "Re-try");
                return;
            }

            foreach (var items in obj)
            {
                var MemberID = items.FFMemberID;
              
                string Status = "Fail";
                string Msg = "Technical Error";
                int Points = 0;

                dsTransactions = new F4HApp.dataservice.DSTransactions();
                string result = await dsTransactions.TransferPoints(App.MemberID, MemberID, TransferPoints);

                EarnResponseObject m = JsonConvert.DeserializeObject<EarnResponseObject>(result.Replace("[", "").Replace("]", ""));
                Status = m.Status.ToString();
                Points = m.Points;
                Msg = m.Msg;

                //string Message = @"You have received 10 loyalty points in your F4H app sent by member - " + App.MobileNumber + ".Please show members list only in Transfer Points.";

                //string SMSResult = await dsLogin.SendSms(items., Message);
                //SMSResponceObject S = JsonConvert.DeserializeObject<SMSResponceObject>(SMSResult.Replace("[", "").Replace("]", ""));

                //string SmsStatusAlert = S.Status.ToString();



                //if (Status == "Success")
                //{

                //}
                //else
                //{
                //    await DisplayAlert("Validation Error", Msg, "Re-try");
                //}

            }

            await SetMemberPoints();

            await DisplayAlert("Food4Health", "Points Transfer done", "Done");
        }
    }

    public class SelectedModel
    {
        public string FFMemberID { get; set; }
    }
}