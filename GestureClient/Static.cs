using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace GestureClient
{
    class Static
    {
        
            public enum Type {rectangle, circle};
            public enum Color { red, green, blue };
            public static string _char;
            public static Type _type;
            public static Color color;
            public static Shape _shape;
            public static UserProfile profile = null;
            public static string profile_name = "";
        
            public static char _value { get; set; }
    }
}
