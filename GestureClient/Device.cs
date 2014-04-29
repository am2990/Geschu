using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace GestureClient
{
    public class Device
    {
        public int id { get; set; }
        public string deviceName { get; set; }
        public string deviceIP { get; set; }
        public string devicePort { get; set; }
        private const int maxDeviceSaved = 10;
        static List<Device> devices =  new List<Device>();

        public Device() { }

        public Device(int id, string deviceName, string deviceIP)
        {
            this.id = id; 
            this.deviceName = deviceName; 
            this.deviceIP = deviceIP;
        }


        private Device Get(int id)
        {
            foreach (Device device in GetSavedDevices())
            {
                if (device.id == id)
                    return device;
            }
            return null;
        }

        public static List<Device> GetSavedDevices()
        {
            //returns all saved Devices
            List<Device> devices = null;
            object devicesObj = Database.Get("device");
            if (devicesObj == null)
            {
                return new List<Device>();
            }
            devices = devicesObj as List<Device>;
            return devices;
        }

        private bool Check()
        {
            return true;
        }

        public void Save()
        {
            if (this.Check())
            {
                List<Device> savedDevices = GetSavedDevices();
                savedDevices.Add(this);
                Database.Add("device", savedDevices);
            }

        }


        public void Clean()
        {
            //Cleans the database
        }
    }
}
