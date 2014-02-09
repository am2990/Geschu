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

    public class Devices
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string DeviceOS { get; set; }
        public string DeviceType { get; set; }
        public string DeviceIP { get; set; }
    }

    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            //DataContext = App.ViewModel;
            //this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            this.LoadData();
        }

        private void LoadData()
        {
            List<Devices> OnlineDevices = new List<Devices>();
            Devices Device = new Devices();
            Device.Id = 0;
            Device.DeviceName = "Apurv di lappy";
            Device.DeviceOS = "Windows 8";
            Device.DeviceType = "Computer";
            OnlineDevices.Add(Device);
            Devices Device_1 = new Devices();
            Device_1.Id = 1;
            Device_1.DeviceName = "Apurv di lappy";
            Device_1.DeviceOS = "Windows 8";
            Device_1.DeviceType = "Computer";
            OnlineDevices.Add(Device_1);
            DevicesList.ItemsSource = OnlineDevices;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddDevice.xaml",UriKind.Relative));
        }

       

    }
}