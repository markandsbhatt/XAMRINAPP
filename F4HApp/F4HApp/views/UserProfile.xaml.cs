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
    public partial class UserProfile : ContentPage
    {
        F4HApp.dataservice.DSLogin dsLogin;

        public UserProfile()
        {
            InitializeComponent();
            lblMemberName.Text = App.MemberName.ToString();

            Task.Run(async () => {

                await GetMemberDetails();

            });

        }

        private async Task<string> GetMemberDetails()
        {
            string result = "Success";
            dsLogin = new F4HApp.dataservice.DSLogin();
            string Response = await dsLogin.GetMemberDetails(App.MemberID);

            try
            {
                MemberDetailResponseObject m = JsonConvert.DeserializeObject<MemberDetailResponseObject>(Response.Replace("[", "").Replace("]", ""));
                string Result = m.Status;                
                string MobileNo = "Mobile No : " + m.MobileNo;
                string EmailID = "Email ID : " + m.EmailID;
                string AddLine1 = m.AddLine1;
                string AddLine2 = m.AddLine2;
                string AddLine3 = m.AddLine3;
                string CityTown = m.CityTown;
                string StateArea = m.StateArea;
                string PostCode = m.PostCode;
                string Country = m.Country;
                string Address = "";

                if (AddLine1.Trim() != "")
                    Address += AddLine1;

                if (AddLine2.Trim() != "")
                    Address += " , " + AddLine2;

                if (AddLine3.Trim() != "")
                    Address += "\n" + AddLine3;

                if (CityTown.Trim() != "")
                    Address += " , " + CityTown;

                if (StateArea.Trim() != "")
                    Address += "\n" + StateArea;

                if (PostCode.Trim() != "")
                    Address += " " + PostCode;

                if (Country.Trim() != "")
                    Address += "\n" + Country;
               
                Device.BeginInvokeOnMainThread(() =>
                {
                    lblMobileNo.Text = MobileNo;
                    lblEmailAddress.Text = EmailID;

                    if (Address == "")
                    {
                        lblAddress.Text = "Pending";
                        btnChangeAddress.Text = "Enter Contact Address";
                    }
                    else
                    {
                        lblAddress.Text = Address;
                        btnChangeAddress.Text = "Change Contact Address";
                    }

                    
                });

            }
            catch (Exception ex)
            {
                string rr = ex.Message;
            }

            return result;
        }

        private async void OnbtnChangeAddress(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new F4HApp.views.ChangeAddress());
        }

        private async void OnbtnChangePassword(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new F4HApp.views.ChangePassword());
        }
    }
}