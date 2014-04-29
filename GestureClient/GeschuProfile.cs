
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
namespace GestureClient
{
    public partial class GeschuProfile
    {
         public string name { get; set; }
         public int ownerId { get; set; }
         int id ;
         public string uri { get; set; } 
         Dictionary<Shape, List<double>> shapeMap = null;
         Dictionary<int,ShapeProperty> profileShapes = new Dictionary<int,ShapeProperty>();
         public Dictionary<int, ShapeProperty> commitShapes { get; set; }

         public GeschuProfile(string name, int owner_id, int id = -1)
         {
             this.shapeMap = new Dictionary<Shape, List<double>>();
             this.profileShapes = new Dictionary<int, ShapeProperty>();
             this.name = name;
             this.ownerId = owner_id;
             this.id = id;
         }

        public void AddShapes(Shape shape, Transform pos , string value)
        {
            profileShapes.Add(shape.GetHashCode() ,new 
                ShapeProperty(shape, pos, value));
            
        }
        public void CommitShapes(Shape shape, Transform pos, string value)
        {
            this.commitShapes.Add(shape.GetHashCode(), new
                ShapeProperty(shape, pos, value));
        }

        public void ClearCommits()
        {
            this.commitShapes = new Dictionary<int, ShapeProperty>();
        }

        public void IncludeCommits()
        {
            this.profileShapes = this.commitShapes;
        }

        public void UpdateShape(Shape shape, Point pos, char value)
        {
            this.shapeMap.Remove(shape);
            this.shapeMap.Add(shape, new List<double>(new double[]{pos.X, pos.Y}));
        }

        public List<ShapeProperty> GetProfileShapes()
        {
            List<ShapeProperty> items = new List<ShapeProperty>();
            foreach(ShapeProperty value in this.profileShapes.Values)
                items.Add(value);
            return items;
        }
        public ShapeProperty GetProfileShape(int index)
        {
            return this.profileShapes[index];
        }
        public void UpdateShape(int index, ShapeProperty shape)
        {
            this.profileShapes.Remove(index);
            this.profileShapes[shape.GetHashCode()] = shape;
        }
        

       public int GetProfileId()
       {
           return this.id;
       }

        public int GetOwnerId()
        {
            return this.ownerId;
        }

        public void Save()
        {
            List<GeschuProfile> profileSettings = null;
            this.name = Static.profile_name;
            object profileObj = Database.Get(this.ownerId.ToString());
            if (profileObj == null)
            {
                profileSettings = new List<GeschuProfile>();
                profileSettings.Add(this);
                Database.Add(this.ownerId.ToString(), profileSettings);
            }
            else
            {
                profileSettings = profileObj as List<GeschuProfile>;
                foreach (GeschuProfile prof in profileSettings)
                {
                    if (prof.id == this.id)
                    {
                        profileSettings.Remove(prof);
                        break;
                    }
                }
                profileSettings.Add(this);
                Database.Add(this.ownerId.ToString(), profileSettings);
            }
        }

        public static List<GeschuProfile> GetAllProfiles(int deviceId)
        {
            object profileObj = Database.Get(deviceId.ToString());
            if (profileObj == null)
                return new List<GeschuProfile>();
            else
                return profileObj as List<GeschuProfile>;
        }

        public static GeschuProfile GetProfile(int profileId, int deviceId)
        {
            foreach (GeschuProfile profile in GetAllProfiles(profileId))
            {
                if (profile.id == deviceId)
                    return profile;
            }
            return null;
        }

        public void Remove() { }


        
    }
}
