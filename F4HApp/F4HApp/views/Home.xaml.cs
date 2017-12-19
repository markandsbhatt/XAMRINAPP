using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace F4HApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        F4HApp.dataservice.DSLogin dsLogin;

        public Home()
        {
            InitializeComponent();

            

            lblHelp.GestureRecognizers.Add((new TapGestureRecognizer
            {
                Command = new Command(async o => {
                    await Navigation.PushModalAsync(new F4HApp.views.LoginHelp());
                })
            }));

            lblCreateAccount.GestureRecognizers.Add((new TapGestureRecognizer
            {
                Command = new Command(async o => {
                    await Navigation.PushModalAsync(new F4HApp.views.Registration());
                })
            }));
            lblAbt.GestureRecognizers.Add((new TapGestureRecognizer
            {


                Command = new Command(() => OnAboutUSMark())
            }));

            lblpatner.GestureRecognizers.Add((new TapGestureRecognizer
            {


                Command = new Command(() => OnAboutUSPatnersMark())
            }));

            lblPlans.GestureRecognizers.Add((new TapGestureRecognizer
            {


                Command = new Command(() => OnAboutUSPlansMark())
            }));


        }

        private async void OnButtonSubmit(object sender, EventArgs e)
        {
            //if (CrossConnectivity.Current.IsConnected)
            //{
                //await Navigation.PushModalAsync(new F4HApp.MP());
                var MobileNo = txtMobileNo.Text;
                var Password = txtPassword.Text;
                string MemberID = "";
                string MemberName = "";
                string IsLoggedIn = "Fail";
                string Msg = "Invalid Login";
                string LastLoginDate = "";

                if (MobileNo == null)
                {
                    await DisplayAlert("Validation Error", "Username cannot be blank", "Re-try");
                    return;
                }

                if (Password == null)
                {
                    await DisplayAlert("Validation Error", "Password cannot be blank", "Re-try");
                    return;
                }

                dsLogin = new F4HApp.dataservice.DSLogin();
                string result = await dsLogin.ChkLogin(MobileNo, Password);

                LoginResponseObject m = JsonConvert.DeserializeObject<LoginResponseObject>(result.Replace("[", "").Replace("]", ""));
                IsLoggedIn = m.IsLoggedIn.ToString();
                MemberID = m.MemberId;
                MemberName = m.Name;
                Msg = m.Msg;
                LastLoginDate = m.LastLoginDate;

                if (IsLoggedIn == "Success")
                {
                    App.IsLoggedIn = true;
                    App.MemberID = MemberID;
                    App.MemberName = MemberName;
                    App.LastLoginDate = LastLoginDate;
                    App.MobileNumber = txtMobileNo.Text.Trim();
                    await Navigation.PushModalAsync(new F4HApp.MP());
                }
                else
                {
                    await DisplayAlert("Validation Error", "Invalid login attempt", "Re-try");
                }


                //try
                //{
                //    var content = "";
                //    HttpClient client = new HttpClient();
                //    var RestURL = "http://www.khubisolutions.com/f4hrestapi/api/home";
                //    client.BaseAddress = new Uri(RestURL);
                //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //    HttpResponseMessage response = await client.GetAsync(RestURL);
                //    content = await response.Content.ReadAsStringAsync();
                //    //var Items = JsonConvert.DeserializeObject<List<ItemClass>>(content);
                //    //ListView1.ItemsSource = Items;
                //}
                //catch(Exception ex)
                //{
                //    string rr = ex.Message.ToString();
                //}
            //}
            //else
            //{
            //    await DisplayAlert("Conection Error", "Internet Conection Wifi/Mobile Data is currently inactive. Try again later.", "Re-try");
            //}

        }

        private async void OnAboutUSMark()
        {
            dsLogin = new F4HApp.dataservice.DSLogin();
            string result = await dsLogin.GetAboutUS("");
            string AboutUsMessage = "";
            try
            {
                AboutUsResponceObject m = JsonConvert.DeserializeObject<AboutUsResponceObject>(result.Replace("[", "").Replace("]", ""));
                AboutUsMessage = m.AboutUsMessage.ToString();

                await DisplayAlert("About Food4Helath", AboutUsMessage, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Conection Error", ex.ToString(), "Error");
            }
        }

        private async void OnAboutUSPatnersMark()
        {
            dsLogin = new F4HApp.dataservice.DSLogin();
            string result = await dsLogin.GetAboutUSPatners("");
            string AboutUsMessage = "";
            try
            {
                AboutPatnerResponceObject m = JsonConvert.DeserializeObject<AboutPatnerResponceObject>(result.Replace("[", "").Replace("]", ""));
                AboutUsMessage = m.AboutPatnerMessage.ToString();

                await DisplayAlert("Partners ", AboutUsMessage, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Conection Error", ex.ToString(), "Error");
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

                await DisplayAlert("Health Plans", AboutUsMessage, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Conection Error", ex.ToString(), "Error");
            }

        }

        private async void OnAboutUS(object sender, EventArgs e)
        {
            dsLogin = new F4HApp.dataservice.DSLogin();
            string result = await dsLogin.GetAboutUS("");
            string AboutUsMessage = "";
            try
            {
                AboutUsResponceObject m = JsonConvert.DeserializeObject<AboutUsResponceObject>(result.Replace("[", "").Replace("]", ""));
                AboutUsMessage = m.AboutUsMessage.ToString();

                await DisplayAlert("About Food4Helath", AboutUsMessage, "OK");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Conection Error", ex.ToString(), "Error");
            }

        }


        private async void OnAboutUSPatners(object sender, EventArgs e)
        {
            dsLogin = new F4HApp.dataservice.DSLogin();
            string result = await dsLogin.GetAboutUSPatners("");
            string AboutUsMessage = "";
            try
            {
                AboutPatnerResponceObject m = JsonConvert.DeserializeObject<AboutPatnerResponceObject>(result.Replace("[", "").Replace("]", ""));
                AboutUsMessage = m.AboutPatnerMessage.ToString();

                await DisplayAlert("About Partners ", AboutUsMessage, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Conection Error", ex.ToString(), "Error");
            }

        }

        private async void OnAboutUSPlans(object sender, EventArgs e)
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