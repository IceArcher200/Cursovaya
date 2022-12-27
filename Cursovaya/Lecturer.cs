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

        public void SetExam(string date, string subject, List<string> groups, string room)
        {
            string answer = CheckNearestExam(date, groups);
            if (answer == "")
            {
                dataStore.AddEvent(new Event(date, groups, subject, _FIO, room));
                
            }
            else throw new Exception($"У группы {answer} на ближайшее время уже назначен экзамен");
        }

        public void RemoveExam(string date, string subject, List<string> groups)
        {
            List<Event> events = dataStore.Get();
            foreach (Event ev in events)
            {
                if (ev.Date == date && ev.FullName == _FIO && ev.Groups == groups)
                {
                    events.Remove(ev);
                    string json = JsonConvert.SerializeObject(dataStore.Get());
                    System.IO.File.WriteAllText(@"path.txt", json);
                    break;
                }
            }
        }

        private string CheckNearestExam(string date, List<string> groups)
        {
            string[] date1 = date.Split(' ')[0].Split('.');
            DateTime d1 = new DateTime(int.Parse(date1[2]), int.Parse(date1[1]), int.Parse(date1[0]));
            List<Event> events = dataStore.Get();
            foreach (Event e in events)
            {
                foreach (string group in groups)
                {
                    foreach (string groupE in e.Groups)
                    {
                        if (group == groupE)
                        {
                            string[] date2 = e.Date.Split(' ')[0].Split('.');
                            DateTime d2 = new DateTime(int.Parse(date2[2]), int.Parse(date2[1]), int.Parse(date2[0]));
                            if (d1 < d2) (d1, d2) = (d2, d1);
                            if (d2.AddDays(2) >= d1) return group;
                            
                        }
                    }
                }
            }
            return "";
            
        }

    }
}
