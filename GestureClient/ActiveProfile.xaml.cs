using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Shapes;

namespace GestureClient
{
    public partial class ActiveProfile : PhoneApplicationPage
    {
        private Profile profile = null;
        public ActiveProfile()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string profile_id_s, owner_id_s;
            int profile_id, owner_id;
            NavigationContext.QueryString.TryGetValue("profileid", out profile_id_s);
            NavigationContext.QueryString.TryGetValue("ownerid", out owner_id_s);
            profile_id = Convert.ToInt32(profile_id_s);
            owner_id = Convert.ToInt32(owner_id_s);
            this.loadProfile(profile_id,owner_id);
        }

        private Shape shape_clone(Shapes original)
        {

            Shape clone_shape = null;
            if (original.get_type() == Shapes.ShapeType.Rectangle)
                clone_shape = new Rectangle();
            else
                clone_shape = new Ellipse();
            Shape original_shape = original.load_shape();
            clone_shape.Fill = original_shape.Fill;
            clone_shape.Height = original_shape.Height;
            clone_shape.Width = original_shape.Width;
            //clone_shape.SetValue(Grid.RowProperty, (int) original.get_row());
            //clone_shape.SetValue(Grid.ColumnProperty, (int) original.get_column());
            //clone_shape = set_shape_transform(clone_shape);
            //clone_shape = set_shape_listener(clone_shape);
            clone_shape.RenderTransform = original.transform;
            return clone_shape;
        }

        private void loadProfile(int profileId, int ownerId)
        {
            Shape clone_shape = null;
            this.profile = Profile.get_profile(ownerId,profileId);
            foreach (Shapes profile_shapes in this.profile.get_profile_shapes())
            {
                clone_shape = shape_clone(profile_shapes);
                list_box.Items.Add(clone_shape);
                //this.controller_profile.commit_Shapes(clone_shape, clone_shape.RenderTransform, "A");
            }
        }
    }
}
