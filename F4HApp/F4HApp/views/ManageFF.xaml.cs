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
    public partial class ManageFF : ContentPage
    {
        F4HApp.dataservice.DSMaster dsMaster;
        F4HApp.dataservice.DSLogin dsLogin;

        public ManageFF()
        {
            InitializeComponent();

            Task.Run(async () => {

                await GetFamilyList();
                await GetFriendList();

            });
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
                        Family.IsVisible = false;
                    }
                    else
                    {
                        FamilyMsg.IsVisible = false;
                        Family.IsVisible = true;

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

        private async void OnbtnAdd(object sender, EventArgs e)
        {
            var FName = txtFName.Text;
            var LName = txtLName.Text;
            var MobileNo = txtMobileNo.Text;
            

            string Status = "Fail";
            string Msg = "Technical Error";

            if (FName == null)
            {
                await DisplayAlert("Validation Error", "First name cannot be blank", "Re-try");
                return;
            }

            if (LName == null)
            {
                await DisplayAlert("Validation Error", "Last name cannot be blank", "Re-try");
                return;
            }

            if (MobileNo == null)
            {
                await DisplayAlert("Validation Error", "Mobile No cannot be blank", "Re-try");
                return;
            }
            if (ddlCategory.SelectedIndex == -1)
            {
                await DisplayAlert("Validation Error", "Category cannot be blank", "Re-try");
                return;
            }

            var FFCategory = ddlCategory.Items[ddlCategory.SelectedIndex];

            dsLogin = new F4HApp.dataservice.DSLogin();
            string result = await dsLogin.MemberFF(App.MemberID, FName, LName, MobileNo, FFCategory);

            RegistrationResponseObject m = JsonConvert.DeserializeObject<RegistrationResponseObject>(result.Replace("[", "").Replace("]", ""));
            Status = m.Status.ToString();
            Msg = m.Msg;


            if (Status == "Success")
            {
                await DisplayAlert("Food4Health", "Member added to your list", "Done");

                string Response = await FamilyList();

                Response = await FriendList();
                
                txtFName.Text = "";
                txtLName.Text = "";
                txtMobileNo.Text = "";
                
            }
            else
            {
                await DisplayAlert("Food4Health Error", Msg, "Re-try");
            }

        }
    }
}