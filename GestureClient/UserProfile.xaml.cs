using System;
using System.Collections.Generic;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Shapes;

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

        private List<GeschuProfile> LoadStaticProfiles(int owner_id, List<GeschuProfile> userProfiles)
        {
            GeschuProfile profile = new GeschuProfile("VLC Profile", owner_id, 0);
            profile.uri = "/Profile_VLC.xaml";
            GeschuProfile profile_hawx = new GeschuProfile("HAWX Profile", owner_id, 0);
            profile_hawx.uri = "/Profile_hawx2.xaml";
            GeschuProfile profile_ppt = new GeschuProfile("PPT Profile", owner_id, 0);
            profile_ppt.uri = "/Profile_PowerPoint.xaml";
            userProfiles.Add(profile);
            userProfiles.Add(profile_hawx);
            userProfiles.Add(profile_ppt);
            return userProfiles;
        }

        private List<GeschuProfile> LoadDynamicProfiles(int owner_id, List<GeschuProfile> userProfiles)
        {
            foreach (GeschuProfile profilep in GeschuProfile.GetAllProfiles(owner_id))
            {
                userProfiles.Add(profilep);
            }
            return userProfiles;
        }

        private void LoadProfiles(int owner_id)
        {
            List<GeschuProfile> userProfiles = new List<GeschuProfile>();
            userProfiles = this.LoadStaticProfiles(owner_id, userProfiles);
            userProfiles = this.LoadDynamicProfiles(owner_id, userProfiles);
            profilesList.ItemsSource = userProfiles;
        }


        private void ProfilesListSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            GeschuProfile selected = profilesList.SelectedItem as GeschuProfile;
            if (profilesList.SelectedIndex > 2)
            {
                // it is a dynamic page; redirect to ActiveProfile.xml
                NavigationService.Navigate(new Uri(
                    "/ActiveProfile.xaml?profileid=" + selected.GetProfileId().ToString()
                    + "&ownerid=" + selected.GetOwnerId().ToString(), UriKind.Relative));
            }
            else
            {
                string navigateTo = selected.uri + "?Id=" + selected.ownerId as string;
                //String uri = "/Profile_VLC.xaml";
                NavigationService.Navigate(new Uri(navigateTo, UriKind.Relative));
            }
            }

        private void AddProfile(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Controller.xaml",UriKind.Relative));
        }
    }
}