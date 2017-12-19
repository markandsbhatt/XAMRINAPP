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
    public partial class ChangeAddress : ContentPage
    {
        F4HApp.dataservice.DSLogin dsLogin;

        public ChangeAddress()
        {
            InitializeComponent();

            Task.Run(async () => {

                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf("KENYA");
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
                string AddLine1 = m.AddLine1;
                string AddLine2 = m.AddLine2;
                string CityTown = m.CityTown;
                string StateArea = m.StateArea;
                string PostCode = m.PostCode;
                string Country = m.Country;
                
                
                Device.BeginInvokeOnMainThread(() =>
                {
                    txtAddress1.Text = AddLine1;
                    txtAddress2.Text = AddLine2;

                    txtCityTown.Text = CityTown;
                    txtStateArea.Text = StateArea;
                    txtPostcode.Text = PostCode;
                    if (Country != "")
                        ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(Country);
                    else
                        ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf("KENYA");



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
            var AddLine1 = txtAddress1.Text;
            var AddLine2 =txtAddress2.Text;
            var CityTown = txtCityTown.Text;
            var StateArea = txtStateArea.Text;
            var PostCode = txtPostcode.Text;
            var Country = ddlCountry.Items[ddlCountry.SelectedIndex]; 

            string Status = "Fail";
            string Msg = "Technical Error";

            if (AddLine1 == "")
            {
                await DisplayAlert("Validation Error", "Address Line 1 cannot be blank", "Re-try");
                return;
            }

            if (CityTown == "")
            {
                await DisplayAlert("Validation Error", "City / Town cannot be blank", "Re-try");
                return;
            }

            //if (Country == "")
            //{
            //    await DisplayAlert("Validation Error", "Country cannot be blank", "Re-try");
            //    return;
            //}

            dsLogin = new F4HApp.dataservice.DSLogin();
            string result = await dsLogin.UpdateMemberDetails(App.MemberID, AddLine1, AddLine2, "", CityTown, StateArea, PostCode, Country);

            RegistrationResponseObject m = JsonConvert.DeserializeObject<RegistrationResponseObject>(result.Replace("[", "").Replace("]", ""));
            Status = m.Status.ToString();
            Msg = m.Msg;


            if (Status == "Success")
            {
                await DisplayAlert("Food4Health", "Contact Address updated successfully", "Done");

                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtCityTown.Text = "";
                txtStateArea.Text = "";
                txtPostcode.Text = "";
                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf("KENYA");
            }
            else
            {
                await DisplayAlert("Food4Health Error", "Technical Error", "Re-try");
            }
        }
    }
}