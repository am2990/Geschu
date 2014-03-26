using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace GestureClient
{
    public partial class ShapesProperties : PhoneApplicationPage
    {
        public ShapesProperties()
        {
            InitializeComponent();
            this.update_color_picker();
            this.update_shape_picker();
        }

        private void update_color_picker()
        {
            color_picker.Items.Add("Red");
            color_picker.Items.Add("Green");
        }

        private void update_shape_picker()
        {
            shape_picker.Items.Add("Circle");
            shape_picker.Items.Add("Rectangle");
        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Controller.xaml?profileid=-1",UriKind.Relative));
        }
    }
}