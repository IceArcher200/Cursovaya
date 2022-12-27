using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursovaya
{
    class Student
    {
        string _FIO;
        string _group;

        public string FullName
        {
            get { return _FIO; }
            private set { }
        }
        public string Group
        {
            get { return _group; }
            private set { }
        }

        public Student(string fio,string group)
        {
            _FIO = fio;
            _group = group;
        }
    }


}
