

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Android.App;

namespace F4HApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        //private Context mContext;

        F4HApp.dataservice.DSLogin dsLogin;

        public Registration()
        {
            InitializeComponent();

            lblBackToLogin.GestureRecognizers.Add((new TapGestureRecognizer
            {
                Command = new Command(async o => {
                    await Navigation.PushModalAsync(new F4HApp.views.Home());
                })
            }));

        }

        private async void OnbtnRegister(object sender, EventArgs e)
        {
            var Title = string.Empty;
            var FirstName = string.Empty;
            var LastName = string.Empty;
            var MobileNo = string.Empty;
            var EmailID = string.Empty;
            var DOB = string.Empty;
            var Password = string.Empty;
            string ValidationMsg = "";
            bool IsValid = true;

            FirstName = txtFirstName.Text;
            LastName = txtLastName.Text;
            MobileNo = txtMobileNo.Text;
            EmailID = txtEmailID.Text;
            DOB = txtDOB.Text;
            Password = txtPassword.Text;

            if (ddlTitle.SelectedIndex > -1)
            {
                Title = ddlTitle.Items[ddlTitle.SelectedIndex];
                
            }

            if (Title == "")
            {
                ValidationMsg += "Title\n";
                IsValid = false;
            }

            if (FirstName == null)
            {
                ValidationMsg += "First Name\n";
                IsValid = false;
            }

            if (LastName == null)
            {
                ValidationMsg += "Last Name\n";
                IsValid = false;
            }

            if (MobileNo == null)
            {
                ValidationMsg += "Mobile\n";
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

            if (EmailID == null)
            {
                ValidationMsg += "Email address\n";
                IsValid = false;
            }
            else
            {
                if (!CheckValidEmail(EmailID))
                {
                    ValidationMsg += "Invalid email address\n";
                    IsValid = false;
                }
            }

            if (DOB == null)
            {
                ValidationMsg += "Date of birth\n";
                IsValid = false;
            }
            else
            {
                if (!CheckValidDOB(DOB))
                {
                    ValidationMsg += "Invalid date of birth format\n";
                    IsValid = false;
                }
            }

            if (Password == null)
            {
                ValidationMsg += "Password\n";
                IsValid = false;
            }

            if (IsValid)
            {

                string Status = "Fail";
                string Msg = "Technical Error";

                dsLogin = new F4HApp.dataservice.DSLogin();
                string result = await dsLogin.Registration(Title, FirstName, LastName, DOB, MobileNo, EmailID, Password);

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
                    string Message = @"Welcome to F4H, your gateway to earn loyalty points.

                                        Login credentials - 

                                        Mobile No.: " + MobileNo +@"

                                        Password:" + Password;

                    string SMSResult = await dsLogin.SendSms(MobileNo, Message);
                    SMSResponceObject S = JsonConvert.DeserializeObject<SMSResponceObject>(SMSResult.Replace("[", "").Replace("]", ""));

                    string SmsStatusAlert = S.Status.ToString();
                    await DisplayAlert("Congratulations " + SmsStatusAlert, "Your registration was successful", "Login");
                    await Navigation.PushModalAsync(new F4HApp.views.Home());
                }
                else
                {
                    await DisplayAlert("Food4Health", Msg, "Re-Try");
                }
            }
            else
            {

                await DisplayAlert("Validation Error", ValidationMsg, "Re-try");
            }
            //await Navigation.PushModalAsync(new F4HApp.MP());
        }

        private void txtDOB_Focused(object sender, FocusEventArgs e)
        {
           

            //DateTime today = DateTime.Today;
            //DatePickerDialog dialog = new DatePickerDialog(mContext.ApplicationContext, OnDateSet, today.Year, today.Month - 1, today.Day);
            //dialog.DatePicker.MinDate = today.Millisecond;
            //dialog.Show();
        }

        private bool CheckValidDOB(string value)
        {
            if (value == null)
            {
                return false;
            }

            //var str = value as string;
            Regex regex = new Regex(@"^(?=\d{2}([-.,\/])\d{2}\1\d{4}$)(?:0[1-9]|1\d|[2][0-8]|29(?!.02.(?!(?!(?:[02468][1-35-79]|[13579][0-13-57-9])00)\d{2}(?:[02468][048]|[13579][26])))|30(?!.02)|31(?=.(?:0[13578]|10|12))).(?:0[1-9]|1[012]).\d{4}$");
            Match match = regex.Match(value);

            return match.Success;
        }

        private bool CheckValidEmail(string value)
        {
            if (value == null)
            {
                return false;
            }

            //var str = value as string;
            Regex regex = new Regex(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$");
            Match match = regex.Match(value);

            return match.Success;
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

        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            txtDOB.Text = e.Date.Date.ToString();
        }


        private void txtMobileNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtMobileNo.Text.Length > 9 || txtMobileNo.Text.Length < 9)
           // if (!CheckValidMobile(txtMobileNo.Text.Trim()))
            {
                 DisplayAlert("Congratulations", "Enter 9 digit mobile no.\n", "Login");
            }
        }

    }
}