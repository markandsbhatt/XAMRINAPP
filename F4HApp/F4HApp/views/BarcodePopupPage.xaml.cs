using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace F4HApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodePopupPage : ContentPage
    {
        F4HApp.dataservice.DSTransactions dsTransactions;
        public BarcodePopupPage()
        {
            InitializeComponent();
        }

        public  void OnClickScan(object sender, EventArgs e)
        {
            ScanAsync();
        }

        public async void ScanAsync()
        {

            var scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += (result) => {
                // Stop scanning
                scanPage.IsScanning = false;
               

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async  () => {
                    await Navigation.PopAsync();
                    var ScratchCode = result.Text;
                    string Status = "";
                    int Points = 0;
                    string Msg = "Technical Problem";

                    if (ScratchCode == null)
                    {
                        await DisplayAlert("Validation Error", "Scratch code cannot be blank", "Re-try");
                        return;
                    }

                    dsTransactions = new F4HApp.dataservice.DSTransactions();
                    string result2 =  await dsTransactions.EarnPoints(App.MemberID, ScratchCode);
                    EarnResponseObject m = JsonConvert.DeserializeObject<EarnResponseObject>(result2.Replace("[", "").Replace("]", ""));
                    Status = m.Status.ToString();
                    Points = m.Points;
                    Msg = m.Msg;

                    if (Status == "Success")
                    {
                        var msg = "Congratulations you have earned " + Points + " points.";
                        await DisplayAlert("Congratulations", msg, "OK");
                        
                      
                    }
                    else
                    {
                        await DisplayAlert("Validation Error", Msg, "Re-try");
                    }
                   // await DisplayAlert("Scanned Barcode", result.Text, "OK");
                });
            };

            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);

        }
    }
}
