using Android.Content;
using Contacts;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace F4HApp.views
{
    public partial class TestContact : ContentPage
    {
        public TestContact()
        {
            InitializeComponent();
         

        }

        public void OnClickShare(object sender, EventArgs e)
        {
            try
            {
        //        var contactPickerIntent = new Intent(Intent.ActionPick,
        //Android.Provider.ContactsContract.Contacts.ContentUri);
        //        StartActivityForResult(contactPickerIntent, 101);
        //        // Create predicate to locate requested contact
                //var predicate = CNContact.GetPredicateForContacts("Appleseed");

                //// Define fields to be searched
                //var fetchKeys = new NSString[] { CNContactKey.GivenName, CNContactKey.FamilyName };

                //// Grab matching contacts
                //var store = new CNContactStore();
                //NSError error;
                //var contacts = store.GetUnifiedContacts(predicate, fetchKeys, out error);

                //// Found?
                //if (contacts.Length > 0)
                //{
                //    // Get the first matching contact
                //    var contact = contacts[0];

                //    // Display it
                //    txtExistingPassword.Text = string.Format("{0} {1}", contact.GivenName, contact.FamilyName);
                //}
                //else
                //{
                //    // Not found
                //    txtExistingPassword.Text = "";
                //}

            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", ex.ToString(), "OK");
            }

        }
    }
}
