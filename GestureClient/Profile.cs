using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestureClient
{
    public class Profile
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int Id { get; set; }

        private ActionButton button;

        private int priority;
    }
}
