﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Shapes;
using System.Windows.Media;

namespace GestureClient
{
    public class ColorItem
    {
        public string Name { get; set; }
        
        public ColorItem()
        { }

        public ColorItem(string name)
        {
            Name = name;
        }
    }
    
    public partial class ShapesProperties : PhoneApplicationPage
    {
        private Profile connectedProfile = null;
        private Shapes shape = null;
        private int profile_id, shape_index, device_id;
        private string profile_id_s, shape_index_s, device_id_s;
  
        public ShapesProperties()
        {
            InitializeComponent();
            this.update_color_picker();
            this.update_shape_picker();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            NavigationContext.QueryString.TryGetValue("profileid", out profile_id_s);
            profile_id = Convert.ToInt32(profile_id_s);
            NavigationContext.QueryString.TryGetValue("shapeindex", out shape_index_s);
            shape_index = Convert.ToInt32(shape_index_s);
            NavigationContext.QueryString.TryGetValue("deviceid", out device_id_s);
            device_id = Convert.ToInt32(device_id_s);
            this.getProfile(profile_id, device_id);
            this.getShape(shape_index);
        }

        private void getProfile(int profileId, int deviceId)
        {
            this.connectedProfile = Profile.get_profile(profileId, deviceId);
        }

        private void getShape(int shapeIndex)
        {
<<<<<<< HEAD
            this.shape = this.connectedProfile.get_profile_shape(shapeIndex);
        }

        private void update_color_picker()
        { 
            color_picker.Items.Add("Red");
            color_picker.Items.Add("Green");
=======
            
            //color_picker.Items.Add("Red");
            //color_picker.Items.Add("Green");
>>>>>>> 41188321e277f954e53e97307dabda24ad3e1894
        }

        private void update_shape_picker()
        {
            //shape_picker.Items.Add("Circle");
            //shape_picker.Items.Add("Rectangle");
        }

        private void shapeGotFocus(Object sender, EventArgs e)
        {
            color_picker.IsEnabled = false;
        }

        private void shapeLostFocus(Object sender, EventArgs e)
        {
            color_picker.IsEnabled = true;
        }

        private void colorGotFocus(Object sender, EventArgs e)
        {
            shape_picker.IsEnabled = false;
        }

        private void colorLostFocus(Object sender, EventArgs e)
        {
            shape_picker.IsEnabled = true;
        }

        private void updateProfile()
        {
            string Color = color_picker.SelectedItem as string;
            string Shape = shape_picker.SelectedItem as string;
            string Char = character.Text;
            Shapes oldShape, newShape;
            Shape temporaryShape;
            oldShape = this.shape;
            if (Shape == "Circle")
            {
                temporaryShape = new Ellipse();
                if (Color == "Red")
                    temporaryShape.Fill = new SolidColorBrush(Colors.Red);
                else
                    temporaryShape.Fill = new SolidColorBrush(Colors.Blue);
                temporaryShape.Height = oldShape.load_shape().Height;
                temporaryShape.Width = oldShape.load_shape().Width;
                newShape = new Shapes(temporaryShape, oldShape.transform, Char);
            }
            else
            {
                temporaryShape = new Rectangle();
                if (Color == "Red")
                    temporaryShape.Fill = new SolidColorBrush(Colors.Red);
                else
                    temporaryShape.Fill = new SolidColorBrush(Colors.Blue);
                temporaryShape.Height = oldShape.load_shape().Height;
                temporaryShape.Width = oldShape.load_shape().Width;
                newShape = new Shapes(temporaryShape, oldShape.transform, Char);
            }
            this.connectedProfile.update_shape(this.shape_index, newShape);
            this.connectedProfile.save();
        }

        private void saveProperties(object sender, RoutedEventArgs e)
        {
            this.updateProfile();
            NavigationService.Navigate(new Uri("/Controller.xaml?profileid=" + profile_id_s, UriKind.Relative));
        }
         
    }
}