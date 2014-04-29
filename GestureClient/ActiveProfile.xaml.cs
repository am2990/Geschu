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
        private GeschuProfile profile = null;

        public ActiveProfile()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string profileIds, ownerIds;
            int profileId, ownerId;
            NavigationContext.QueryString.TryGetValue("profileid",
                out profileIds);
            NavigationContext.QueryString.TryGetValue("ownerid",
                out ownerIds);
            profileId = Convert.ToInt32(profileIds);
            ownerId = Convert.ToInt32(ownerIds);
            this.LoadProfile(profileId, ownerId);
        }

        private Shape ShapeClone(ShapeProperty originalShapeProperty)
        {
            Shape clonedShape = null;
            if (originalShapeProperty.GetType() == ShapeProperty.ShapeType.Rectangle)
                clonedShape = new Rectangle();
            else
                clonedShape = new Ellipse();
            Shape originalShape = originalShapeProperty.GetShape();
            clonedShape.Fill = originalShape.Fill;
            clonedShape.Height = originalShape.Height;
            clonedShape.Width = originalShape.Width;
            clonedShape.RenderTransform = originalShapeProperty.transform;
            return clonedShape;
        }

        private void LoadProfile(int profileId, int ownerId)
        {
            Shape cloneShape = null;
            this.profile = GeschuProfile.GetProfile(ownerId, profileId);
            foreach (ShapeProperty profileShapes in this.profile.GetProfileShapes())
            {
                cloneShape = this.ShapeClone(profileShapes);
                list_box.Items.Add(cloneShape);
            }
        }
    }
}
