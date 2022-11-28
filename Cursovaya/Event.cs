using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursovaya
{

    public class Event
    {
        string _subject, _FIO, _date;
        List<string> _groups;

        public string Subject
        {   
            get {return _subject; }
            set { _subject = value; }
        }

        public string FullName
        {
            get { return _FIO; }
            private set { }
        }
        public string Date
        {
            get { return _date; }
            private set { }
        }
        public string Groups
        {
            get {
                string allgroups = "";
                foreach (string group in _groups)
                {
                    allgroups += group + ", ";
                }
                return allgroups; }
            private set { }
        }
        public Event(string date, List<string> groups, string subject, string FIO)
        {
            _date = date;
            _groups = groups;
            _subject = subject;
            _FIO = FIO;
        }
    }
}
