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
    public partial class ChangePassword : ContentPage
    {

        F4HApp.dataservice.DSLogin dsLogin;

        public ChangePassword()
        {
            InitializeComponent();
        }

        private async void OnbtnChangePassword(object sender, EventArgs e)
        {
            var ExistingPassword = txtExistingPassword.Text;
            var NewPassword = txtNewPassword.Text;
            var ConfirmPassword = txtConfirmPassword.Text;

            string Status = "Fail";
            string Msg = "Technical Error";

            if (ExistingPassword == "")
            {
                await DisplayAlert("Validation Error", "Existing Password cannot be blank", "Re-try");
                return;
            }

            if (NewPassword == "")
            {
                await DisplayAlert("Validation Error", "New Password cannot be blank", "Re-try");
                return;
            }

            if (ConfirmPassword == "")
            {
                await DisplayAlert("Validation Error", "Confirm Password cannot be blank", "Re-try");
                return;
            }

            if (ConfirmPassword != NewPassword)
            {
                await DisplayAlert("Validation Error", "Confirm password and New password doesnot match", "Re-try");
                return;
            }

            dsLogin = new F4HApp.dataservice.DSLogin();
            string result = await dsLogin.UpdateMemberPassword(App.MemberID, ExistingPassword, NewPassword);

            RegistrationResponseObject m = JsonConvert.DeserializeObject<RegistrationResponseObject>(result.Replace("[", "").Replace("]", ""));
            Status = m.Status.ToString();
            Msg = m.Msg;


            if (Status == "Success")
            {
                await DisplayAlert("Food4Health", "Password Changed", "Done");

                txtExistingPassword.Text = "";
                txtNewPassword.Text = "";
                txtConfirmPassword.Text = "";
            }
            else
            {
                await DisplayAlert("Food4Health Error", Msg, "Re-try");
            }


        }
    }
}