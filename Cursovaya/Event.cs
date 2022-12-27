using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursovaya
{
    [Serializable]
    public class Event
    {
        private string _subject, _FIO, _date, _room;
        private List<string> _groups;

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public string FullName
        {
            get { return _FIO; }
            set { _FIO = value; }
        }
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }
        
        public List<string> Groups
        {
            get { return _groups; }
            set { _groups = value; }
        }

        public string Room
        {
            get { return _room; }
            set { _room = value; }
        }
        public Event(string date, List<string> groups, string subject, string FIO, string room)
        {
            _date = date;
            _groups = groups;
            _subject = subject;
            _FIO = FIO;
            _room = room;
        }
    }
}
