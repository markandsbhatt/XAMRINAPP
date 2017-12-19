using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace F4HApp
{
    public partial class App : Application
    {
        public static bool IsLoggedIn { get; set; }
        public static string MemberID { get; set; }
        public static string MemberName { get; set; }
        public static string LastLoginDate { get; set; }
        public static string MobileNumber { get; set; }
        public App()
        {
            InitializeComponent();

            //var isLoggedIn = IsLoggedIn ? (bool)Properties["IsLoggedIn"] : false;

            // we remember if they're logged in, and only display the login page if they're not
            if (IsLoggedIn)
                MainPage = new F4HApp.MP();
            else
                MainPage = new F4HApp.views.Home();

            //MainPage = new F4HApp.views.Home();
            //MainPage = new F4HApp.MP();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
