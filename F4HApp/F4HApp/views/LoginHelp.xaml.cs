using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace F4HApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginHelp : ContentPage
    {
        F4HApp.dataservice.DSLogin dsLogin;
        private int IntRandom;

        public LoginHelp()
        {
            InitializeComponent();

            lblBackToLogin.GestureRecognizers.Add((new TapGestureRecognizer
            {
                Command = new Command(async o => {
                    await Navigation.PushModalAsync(new F4HApp.views.Home());
                })
            }));
        }


        private async void OnbtnSendCode(object sender, EventArgs e)
        {
            var MobileNo = string.Empty;
            MobileNo = txtMobileNo.Text;
            string ValidationMsg = "";
            bool IsValid = true;

            if (MobileNo == null)
            {
                ValidationMsg += "Enter Mobile\n";
                IsValid = false;
            }
            else
            {
                if (!CheckValidMobile(MobileNo))
                {
                    ValidationMsg += "Enter 9 digit mobile no.\n";
                    IsValid = false;
                }
            }

            if (IsValid)
            {
                dsLogin = new F4HApp.dataservice.DSLogin();
                string result = await dsLogin.ChkMobileExists(MobileNo);

                RegistrationResponseObject m = JsonConvert.DeserializeObject<RegistrationResponseObject>(result.Replace("[", "").Replace("]", ""));
                string Status = m.Status.ToString();                
                string Msg = m.Msg;

                if (Status == "Success")
                {
                    IntRandom = RandomNumber(1, 9999);
                    string Message = @"Your OTP is " + IntRandom.ToString();

                    string SMSResult = await dsLogin.SendSms(MobileNo,Message);
                    SMSResponceObject S = JsonConvert.DeserializeObject<SMSResponceObject>(SMSResult.Replace("[", "").Replace("]", ""));
                   
                    string SmsStatusAlert = S.Status.ToString();
                    await DisplayAlert("Food4Health - OTP ", IntRandom.ToString() , "Done");
                    StackMobileNo.IsVisible = false;
                    StackReset.IsVisible = false;
                    StackOTP.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("Validation Error", "Mobile number not found", "Re-try");
                }

            }
            else
            {

                await DisplayAlert("Validation Error", ValidationMsg, "Re-try");
            }

        }

        private async void OnbtnCheckOTP(object sender, EventArgs e)
        {
            var OTP = string.Empty;
            OTP = txtOTP.Text;

            if (OTP == null)
            {
                await DisplayAlert("Validation Error", "Enter otp", "Re-try");
                return;
            }

            if (int.Parse(OTP) != IntRandom)
            {
                await DisplayAlert("Validation Error", "Invalid otp", "Re-try");
                return;
            }
            else
            {
                StackMobileNo.IsVisible = false;
                StackReset.IsVisible = true;
                StackOTP.IsVisible = false;
            }
            
        }

        private async void OnbtnResetPassword(object sender, EventArgs e)
        {
            var Pass = string.Empty;
            Pass = txtResetPassword.Text;

            if (Pass == null)
            {
                await DisplayAlert("Validation Error", "Enter new password", "Re-try");
                return;
            }

            dsLogin = new F4HApp.dataservice.DSLogin();
            string result = await dsLogin.ResetMemberPassword(txtMobileNo.Text, Pass);

            RegistrationResponseObject m = JsonConvert.DeserializeObject<RegistrationResponseObject>(result.Replace("[", "").Replace("]", ""));
            string Status = m.Status.ToString();
            string Msg = m.Msg;
            txtResetPassword.Text = "";

            await DisplayAlert("Food4Health", Msg, "Done");            
        }

        private bool CheckValidMobile(string value)
        {
            if (value == null)
            {
                return false;
            }

            //var str = value as string;
            Regex regex = new Regex(@"^[0-9]{9}$");
            Match match = regex.Match(value);

            return match.Success;
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

    }
}