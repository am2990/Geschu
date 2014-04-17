using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestureClient
{
    class Static
    {
        public static struct Shape_info
        {
            public static enum Type {rectangle, circle};
            public static enum Color { red, green, blue };
            public static string _char;
            public static Type _type;
            public static Color color;
        }
        public static UserProfile profile = null;
        public static string profile_name = "";
        
    }
}
