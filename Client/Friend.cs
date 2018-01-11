using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiaChat_Client
{
    
    class Friend
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public Friend(string name,string number)
        {
            Name = name;
            Number = number;
        }
    }
}
