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
            else
            {
                LoadProfiles(0);
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        public UserProfile()
        {
            InitializeComponent();
        }

        private void LoadProfiles(int owner_id)
        {
            List<Profile> UserProfile = new List<Profile>();
            Profile profile = new Profile("VLC Profile", owner_id, 0, "Images/Icon/appbar.cone.png");
            profile.uri = "/Profile_VLC.xaml";
            Profile profile_hawx = new Profile("HAWX Controller", owner_id, 0);
            profile_hawx.uri = "/Profile_hawx2.xaml";
            Profile profile_ppt = new Profile("PPT Profile", owner_id, 0, "Images/Icon/ppt.png");
            profile_ppt.uri = "/Profile_PowerPoint.xaml";
            Profile lego_profile = new Profile("LEGO Racers", owner_id, 0, "Images/Icon/legoracer.png");
            lego_profile.uri = "/Profile_Lego.xaml";
            UserProfile.Add(profile);
            UserProfile.Add(profile_hawx);
            UserProfile.Add(profile_ppt);
            UserProfile.Add(lego_profile);
            foreach (Profile profilep in Profile.getAllProfiles(owner_id))
            {
                UserProfile.Add(profilep);
            }
            profilesList.ItemsSource = UserProfile;
        }

        private void profilesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Profile selected = profilesList.SelectedItem as Profile;
            string navigateTo = selected.uri + "?Id=" + selected.ownerId as string;
          

            //String uri = "/Profile_VLC.xaml";
            NavigationService.Navigate(new Uri(navigateTo, UriKind.Relative));
        }

        private void Add_Profile(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Controller.xaml",UriKind.Relative));
        }
    }
}