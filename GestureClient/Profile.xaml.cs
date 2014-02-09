using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace GestureClient
{
    public class User_Profile
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }
    }

    public partial class Profile : PhoneApplicationPage
    {

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e); 
            string msg;
            if(NavigationContext.QueryString.TryGetValue("Id",out msg))
            {  
              LoadProfiles(Convert.ToInt32(msg));
            }  
        }

        public Profile()
        {
            InitializeComponent();
        }

        private void LoadProfiles(int owner_id)
        {
            List<User_Profile> UserProfile = new List<User_Profile>();
            User_Profile profile = new User_Profile();
            profile.Name = "Profile 1";
            profile.OwnerId = owner_id;
            profile.Id = 0;
            UserProfile.Add(profile);
            userProfiles.ItemsSource = UserProfile;
        }
    }
}