using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
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

        public void SetExam(DateTime date, string subject, List<string> groups, string room)
        {
           
            string answer = CheckNearestExam(date, groups);
            if (answer == "")
            {
                dataStore.AddEvent(new Exam(date, groups, subject, _FIO, room));
                
            }
            else throw new Exception($"У группы {answer} на ближайшее время уже назначен экзамен");
        }

        public void SetConsult(DateTime date, string subject, List<string> groups, string room)
        {

            string answer = CheckNearestExam(date, groups);
            if (answer == "")
            {
                dataStore.AddEvent(new Consult(date, groups, subject, _FIO, room));

            }
            else throw new Exception($"У группы {answer} на ближайшее время уже назначен экзамен");
        }

        public void RemoveExam(DateTime date, string subject, List<string> groups)
        {
            List<Event> events = dataStore.Get();
            foreach (Event ev in events)
            {
                if (ev.Date.ToString() == date.ToString() && ev.FullName == _FIO && ev.Groups.SequenceEqual(groups))
                {
                    dataStore.DeleteEvent(ev);
                    
                    break;
                }
            }
        }

        private string CheckNearestExam(DateTime date, List<string> groups)
        {
            
            List<Event> events = dataStore.Get();
            foreach (Event e in events)
            {
                foreach (string group in groups)
                {
                    foreach (string groupE in e.Groups)
                    {
                        if (group == groupE)
                        {
                            DateTime d2 = e.Date;
                            
                            if (date < d2) 
                                (date, d2) = (d2, date);
                            if (d2.AddDays(2) >= date) 
                                return group;
                            
                        }
                    }
                }
            }
            return "";
            
        }

    }
}
