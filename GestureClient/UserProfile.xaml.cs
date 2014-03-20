using System;
using System.Collections.Generic;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;

namespace GestureClient
{

    public partial class UserProfile : PhoneApplicationPage
    {

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
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
            userProfiles.ItemsSource = UserProfile;
        }

        private void userProfiles_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Profile item = (Profile)userProfiles.SelectedItem;
            String uri = "/UserProfile.xaml?" + "Id=" + item.id;
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }
    }
}