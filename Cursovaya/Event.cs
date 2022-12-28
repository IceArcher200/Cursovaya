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
        private string _subject, _FIO;
        private Room _room;
        private DateTime _date;
        private List<Group> _groups;

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
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        
        public List<Group> Groups
        {
            get { return _groups; }
            set { _groups = value; }
        }

        public Room Room
        {
            get { return _room; }
            set { _room = value; }
        }
        public Event(DateTime date, List<Group> groups, string subject, string FIO, Room room)
        {
            _date = date;
            _groups = groups;
            _subject = subject;
            _FIO = FIO;
            _room = room;
        }

        public virtual DateTime GetEndTime() { return this.Date; }
        public virtual string GetName() { return "Событие"; }
        
        
    }
}
