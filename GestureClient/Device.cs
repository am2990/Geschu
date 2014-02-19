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

        private void get(int id) { }

        public List<Device> getAll()
        {
            return null;
            //returns all saved Devices
        }

        public void save()
        {
            // saves the device in local database
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
