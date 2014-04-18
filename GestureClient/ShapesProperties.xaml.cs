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
            
            //color_picker.Items.Add("Red");
            //color_picker.Items.Add("Green");
        }

        private void update_shape_picker()
        {
            //shape_picker.Items.Add("Circle");
            //shape_picker.Items.Add("Rectangle");
        }

        private void save_properties()
        {
            Static.Shape_info.color = Static.Shape_info.Color.green;
            Static.Shape_info._type = Static.Shape_info.Type.circle;
            Static.Shape_info._char = character.Text;
        }
      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.save_properties();
            NavigationService.Navigate(new Uri("/Controller.xaml?profileid=-1",UriKind.Relative));
        }
        private void Add_Button(object sender, System.EventArgs e)
        {
        }
        private void Save_Click(object sender, System.EventArgs e)
        {
        }
    }
}