
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
namespace GestureClient
{
    public partial class Profile
    {
         string name ;
         int ownerId ;
         int id ;
         ActionButton button;
         int priority;
         Dictionary<Shape, List<double>> shape_map = null;
         List<Shapes> profile_shapes;

        public void add_Shapes(Shape shape, Transform pos , char value)
        {
            profile_shapes.Add(new 
                Shapes(shape, pos, value));
            //this.shape_map.Add(shape,new List<double>(new double[]{pos.X, pos.Y}));
        }

        public void update_Shape(Shape shape, Point pos, char value)
        {
            this.shape_map.Remove(shape);
            this.shape_map.Add(shape, new List<double>(new double[]{pos.X, pos.Y}));
        }
        public List<Shapes> get_profile_shapes()
        {
            return this.profile_shapes;
        }
        public Profile(string name, int owner_id, int id=-1)
        {
            this.shape_map = new Dictionary<Shape, List<double>>();
            this.profile_shapes = new List<Shapes>();
            this.name = name;
            this.ownerId = owner_id;
            this.id = id;
        }

       

        public void save()
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

        public static List<Profile> getAllProfiles(int deviceId)
        {
            object profile_obj = Database.get(deviceId.ToString());
            if (profile_obj == null)
                return new List<Profile>();
            else
                return profile_obj as List<Profile>;
        }

        public static Profile get_profile(int profileId, int deviceId)
        {
            foreach (Profile profile in getAllProfiles(profileId))
            {
                if (profile.id == deviceId)
                    return profile;
            }
            return null;
        }

        public void Remove() { }

    }
}
