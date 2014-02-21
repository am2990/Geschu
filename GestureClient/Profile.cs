
using System.Collections.Generic;
namespace GestureClient
{
    public class Profile
    {
        public string name { get; set; }
        public int ownerId { get; set; }
        public int id { get; set; }
        private ActionButton button;
        private int priority;

        public void Add()
        {
            List<Profile> profile_settings = null;
            object profile_obj = Database.get(this.ownerId.ToString());
            if (profile_obj == null)
            {
                profile_settings = new List<Profile>();
                profile_settings.Add(this);
                Database.add(this.ownerId.ToString(), profile_settings);
            }
            else
            {
                profile_settings = profile_obj as List<Profile>;
                profile_settings.Add(this);
                Database.add(this.ownerId.ToString(), profile_settings);
            }
        }

        public List<Profile> getAllProfiles(int deviceId)
        {
            object profile_obj = Database.get(deviceId.ToString());
            if (profile_obj == null)
                return new List<Profile>();
            else
                return profile_obj as List<Profile>;
        }

        public Profile getProfile(int profileId, int deviceId)
        {
            foreach (Profile profile in this.getAllProfiles(deviceId))
            {
                if (profile.id == profileId)
                    return profile;
            }
            return null;
        }

        public void Remove() { }

    }
}
