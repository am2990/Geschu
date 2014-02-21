using System.IO.IsolatedStorage;

namespace GestureClient
{
    class Database
    {
        private static IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

        public enum Type { Device, Profile };

        public static void add(string key, object value)
        {
            if (storage.Contains(key))
            {
                storage.Remove(key);
            }
            storage.Add(key, value);
        }

        public static void remove(string key)
        {
            storage.Remove(key);
        }

        public static object get(string key)
        {
            if (storage.Contains(key))
            {
                return storage[key];
            }
            return null;
        }
    }
}
