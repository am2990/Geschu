using System.IO.IsolatedStorage;

namespace GestureClient
{
    class Database
    {
        private static IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

        public enum Type { Device, Profile };

        public static void Add(string key, object value)
        {
            if (storage.Contains(key))
            {
                storage.Remove(key);
            }
            storage.Add(key, value);
        }

        public static void Remove(string key)
        {
            storage.Remove(key);
        }

        public static object Get(string key)
        {
            if (storage.Contains(key))
            {
                return storage[key];
            }
            return null;
        }
    }
}
