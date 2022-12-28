using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursovaya
{
    public class Room
    {
        string _number;
        int _capacity;

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        public Room(string number, int capacity)
        {
            _number = number;
            _capacity = capacity;
        }
    }
}
