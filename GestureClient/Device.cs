using System.Collections;
using System.Collections.Generic;

namespace GestureClient
{
    class Device
    {
        public int id { get; set; }
        public string deviceName { get; set; }
        public string deviceOS { get; set; }
        public string deviceType { get; set; }
        public string deviceIP { get; set; }
        private const int maxDeviceSaved = 10;

        private Device get(int id)
        {
            foreach (Device device in this.getAll())
            {
                if (device.id == id)
                    return device;
            }
            return null;
        }

        public List<Device> getAll()
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
                List<Device> saved_Devices = this.getAll();
                saved_Devices.Add(this);
                Database.add("device", saved_Devices);
            }

        }

        public static List<Device> detect()
        {
            // Detects the devices around
            //@Apurv: Code krde ye kamine
            return null;
        }

        public void clean()
        {
            //Cleans the database
        }
    }
}
