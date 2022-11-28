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
        List<string> _groups, _subjects;
        DataBase dataStore = DataBase.GetInstance();
        public string FullName
        {
            get { return _FIO; }
            private set { }
        }
        public List<string> Groups
        {
            get { return _groups; }
            private set { }
        }

        public List<string> Subjects
        {
            get { return _subjects; }
            private set { }
        }
        public Lecturer(string FIO, List<string> groups, List<string> subjects)
        {
            _FIO = FIO;
            _groups = groups;
            _subjects = subjects;
        }

        public void SetExam(string date, string subject, List<string> groups)
        {
            dataStore.AddEvent(new Event(date, groups, subject, _FIO));
        }

    }
}
