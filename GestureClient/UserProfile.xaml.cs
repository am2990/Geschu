using System;
using System.Collections.Generic;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Windows.Controls;

namespace GestureClient
{

    public partial class UserProfile : PhoneApplicationPage
    {

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            InitializeComponent();
            string msg;
            if (NavigationContext.QueryString.TryGetValue("Id", out msg))
            {
                LoadProfiles(Convert.ToInt32(msg));
            }
        }

        public UserProfile()
        {
            InitializeComponent();
        }

        private void LoadProfiles(int owner_id)
        {
            List<Profile> UserProfile = new List<Profile>();
            Profile profile = new Profile();
            profile.name = "Profile 1";
            profile.ownerId = owner_id;
            profile.id = 0;
            UserProfile.Add(profile);
            profilesList.ItemsSource = UserProfile;
        }

        private void profilesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ListBox item = (ListBox)profilesList.SelectedItem;
            String uri = "/UserProfile.xaml?" + "Id=" + item.SelectedValue.ToString();
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        private void Add_Profile(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Controller.xaml",UriKind.Relative));
        }
    }
}