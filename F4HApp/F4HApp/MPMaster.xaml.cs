using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace F4HApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MPMaster : ContentPage
    {
        public ListView ListView;

        public MPMaster()
        {
            InitializeComponent();
            
            BindingContext = new MPMasterViewModel();
            ListView = MenuItemsListView;

            ListView.ItemTapped += ListView_ItemTapped;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MPMenuItem;

            if(item.Title == "Logout")
            {
                App.IsLoggedIn = false;
                App.MemberID = "";
                App.MemberName = "";
                App.MobileNumber = "";
                Navigation.PushModalAsync(new F4HApp.views.Home());
            }
        }

        class MPMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MPMenuItem> MenuItems { get; set; }

            public MPMasterViewModel()
            {
                MenuItems = new ObservableCollection<MPMenuItem>(new[]
                {
                    new MPMenuItem { Id = 0, Title = "Home", IconSource = "Icon_home24x24.png", TargetType = typeof(MPDetail) },
                    new MPMenuItem { Id = 1, Title = "Notifications", IconSource = "Icon_Noticiation24x24.png", TargetType = typeof(views.Notifications) },
                    new MPMenuItem { Id = 2, Title = "My Profile", IconSource = "Icon_MyProfile24x24.png", TargetType = typeof(views.UserProfile) },
                    new MPMenuItem { Id = 3, Title = "Transactions", IconSource = "icon_Transactions24x24.png", TargetType = typeof(views.PointTransactions) },
                    //new MPMenuItem { Id = 4, Title = "Refer Friends", IconSource = "Icon_Refere24x24.png", TargetType = typeof(views.ReferFriends) },
                    new MPMenuItem { Id = 4, Title = "Family & Friends", IconSource = "icon_family22x22.png", TargetType = typeof(views.ManageFF) },
                    new MPMenuItem { Id = 5, Title = "Customer Support", IconSource = "icon_support24x24.png", TargetType = typeof(views.CustomerSupport) },
                      new MPMenuItem { Id = 6, Title = "Enter Scratch Code", IconSource = "Icon_Barcode.png", TargetType = typeof(views.ScanCard) },
                          new MPMenuItem { Id = 7, Title = "Refer Friends", IconSource = "Icon_Refere24x24.png", TargetType = typeof(views.ShareLink) },
                               new MPMenuItem { Id = 8, Title = "Support Forum", IconSource = "icon_question_and_answer.png", TargetType = typeof(views.SupportForum) },
                      
                    new MPMenuItem { Id = 9, Title = "Logout", IconSource = "icon_logout24x24.png", TargetType = typeof(views.Home) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}