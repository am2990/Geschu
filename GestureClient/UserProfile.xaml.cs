using System;
using System.Collections.Generic;
using Microsoft.Phone.Controls;

namespace GestureClient
{

    public partial class UserProfile : PhoneApplicationPage
    {

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
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
            profile.Name = "Profile 1";
            profile.OwnerId = owner_id;
            profile.Id = 0;
            UserProfile.Add(profile);
            userProfiles.ItemsSource = UserProfile;
        }
    }
}