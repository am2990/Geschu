using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace GestureClient
{
    public class Device
    {
        public int id { get; set; }
        public string deviceName { get; set; }
        //public string deviceOS { get; set; }
        //public string deviceType { get; set; }
        public string deviceIP { get; set; }
        public string devicePort { get; set; }
        private const int maxDeviceSaved = 10;
        static List<Device> devices =  new List<Device>();

        private Device get(int id)
        {
            foreach (Device device in getAll())
            {
                if (device.id == id)
                    return device;
            }
            return null;
        }

        public static List<Device> getAll()
        {
            //returns all saved Devices
            object devices_obj = Database.get("device");
            if (devices_obj == null)
            {
                return new List<Device>();
            }
            List<Device> devices = devices_obj as List<Device>;
            return devices;
        }

        private bool check()
        {
            return true;
        }

        public void save()
        {
            if (this.check())
            {
                List<Device> saved_Devices = getAll();
                saved_Devices.Add(this);
                Database.add("device", saved_Devices);
            }

        }


        public void clean()
        {
            //Cleans the database
        }
    }
}
