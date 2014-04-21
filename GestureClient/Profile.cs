
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
namespace GestureClient
{
    public partial class Profile
    {
         public string name { get; set; }
         public int ownerId { get; set; }
         int id ;
         public string uri { get; set; }

<<<<<<< HEAD
         public string image_uri { get; set; }

=======
         
>>>>>>> cf77afef3fc358d7a7bd1082ec885cdaf1c24de2
         Dictionary<Shape, List<double>> shape_map = null;
         Dictionary<int,Shapes> profile_shapes = new Dictionary<int,Shapes>();

        public void add_Shapes(Shape shape, Transform pos , string value)
        {
            profile_shapes.Add(shape.GetHashCode() ,new 
                Shapes(shape, pos, value));
            //this.shape_map.Add(shape,new List<double>(new double[]{pos.X, pos.Y}));
        }
        public void commit_Shapes(Shape shape, Transform pos, string value)
        {
            this.commit_shapes.Add(shape.GetHashCode(), new
                Shapes(shape, pos, value));
            //this.shape_map.Add(shape,new List<double>(new double[]{pos.X, pos.Y}));
        }

        public void clear_commits()
        {
            this.commit_shapes = new Dictionary<int, Shapes>();
        }

        public void include_commits()
        {
            this.profile_shapes = this.commit_shapes;
        }

        public void update_Shape(Shape shape, Point pos, char value)
        {
            this.shape_map.Remove(shape);
            this.shape_map.Add(shape, new List<double>(new double[]{pos.X, pos.Y}));
        }
        public List<Shapes> get_profile_shapes()
        {
            List<Shapes> items = new List<Shapes>();
            foreach(Shapes value in this.profile_shapes.Values)
                items.Add(value);
            return items;
        }
        public Shapes get_profile_shape(int index)
        {
            return this.profile_shapes[index];
        }
        public void update_shape(int index, Shapes shape)
        {
            this.profile_shapes.Remove(index);
            this.profile_shapes[shape.GetHashCode()] = shape;
        }
        public Profile(string name, int owner_id, int id = -1, string image_uri = "Images/Icon/computer.png")
        {
            this.shape_map = new Dictionary<Shape, List<double>>();
            this.profile_shapes = new Dictionary<int,Shapes>();
            this.name = name;
            this.ownerId = owner_id;
            this.id = id;
            this.image_uri = image_uri;
        }

       

        public void save()
        {
            List<Profile> profile_settings = null;
            this.name = Static.profile_name;
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
                foreach (Profile prof in profile_settings)
                {
                    if (prof.id == this.id)
                    {
                        profile_settings.Remove(prof);
                        break;
                    }
                }
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


        public Dictionary<int, Shapes> commit_shapes { get; set; }
    }
}
