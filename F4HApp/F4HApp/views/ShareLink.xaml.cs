
using Plugin.Share;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace F4HApp.views
{
    public partial class ShareLink : ContentPage
    {
        public ShareLink()
        {
            InitializeComponent();
        }
     
        public void OnClickShare(object sender, EventArgs e)
        {
            try { 
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                Text = "Welcome to Food4Health",
                Title = "Welcome to Food4Health. Your friend with mobile no 9821346220 has invited you to download the F4H app to earn loyalty points. To download follow the link",
                Url= "https://play.google.com/store/apps/details?id=com.whatsapp&hl=en",

            });
            }
            catch(Exception ex)
            {
                DisplayAlert("ERROR", ex.ToString(),"OK");
            }

        }
      
    }
}
