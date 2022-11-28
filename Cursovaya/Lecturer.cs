using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursovaya
{

    class Lecturer
    {
        string _FIO;
        List<string> _groups;
        DataBase dataStore = DataBase.GetInstance();
        public string FullName
        {
            get { return _FIO; }
            private set { }
        }
        public Lecturer(string FIO, List<string> groups)
        {
            _FIO = FIO;
            _groups = groups;
        }

        public void SetExam(string date, string subject, List<string> groups)
        {
            dataStore.AddEvent(new Event(date, groups, subject, _FIO));
        }

    }
}
