using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursovaya
{
    public class Group
    {
        string _name;
        int _studentCount;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Count
        {
            get { return _studentCount; }
            set { _studentCount = value; }
        }

        public Group(string name, int count)
        {
            _name = name;
            _studentCount = count;
        }

    }
}
