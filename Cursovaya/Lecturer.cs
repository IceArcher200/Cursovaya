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
        List<string> _subjects;
        List<Group> _groups;
        DataBase dataStore = DataBase.GetInstance();
        public string FullName
        {
            get { return _FIO; }
            private set { }
        }
        public List<Group> Groups
        {
            get { return _groups; }
            private set { }
        }

        public List<string> Subjects
        {
            get { return _subjects; }
            private set { }
        }
        public Lecturer(string FIO, List<Group> groups, List<string> subjects)
        {
            _FIO = FIO;
            _groups = groups;
            _subjects = subjects;
        }

        public void SetExam(DateTime date, string subject, List<Group> groups, Room room)
        {
            Exam ex1 = new Exam(date, groups, subject, _FIO, room);
            Group answer = CheckNearestExam(date, groups);
            if (answer == null && CheckFreeRoom(ex1) && CheckCapacity(groups,room))
            {
                dataStore.AddEvent(ex1);
                
            }
            else if (answer != null) throw new Exception($"У группы {answer.Name} на ближайшее время уже назначен экзамен");
            else if (CheckCapacity(groups, room) == false) throw new Exception("Кол-во студентов превосходит вместимость выбранной аудитории");
            else throw new Exception("Данная аудитория уже занята на это время");
        }

        public void SetConsult(DateTime date, string subject, List<Group> groups, Room room)
        {
            Consult c1 = new Consult(date, groups, subject, _FIO, room);
            Group answer = CheckNearestExam(date, groups);
            if (answer == null && CheckFreeRoom(c1) && CheckCapacity(groups, room))
            {
                dataStore.AddEvent(c1);

            }
            else if (answer != null) throw new Exception($"У группы {answer.Name} на ближайшее время уже назначен экзамен");
            else if (CheckCapacity(groups, room) == false) throw new Exception("Кол-во студентов превосходит вместимость выбранной аудитории");
            else throw new Exception($"Данная аудитория уже занята на это время");
        }

        public void RemoveEvent(DateTime date, string subject, List<string> groups)
        {
            List<Event> events = dataStore.Get();
            
            foreach (Event ev in events)
            {
                List<string> gr = new List<string>();
                foreach (Group g in ev.Groups)
                {
                    gr.Add(g.Name);
                }
                if (ev.Date.ToString() == date.ToString() && ev.FullName == _FIO && gr.SequenceEqual(groups))
                {
                    dataStore.DeleteEvent(ev);
                    
                    break;
                }
            }
        }

        private Group CheckNearestExam(DateTime date, List<Group> groups)
        {
            
            List<Event> events = dataStore.Get();
            foreach (Event e in events)
            {
                foreach (Group group in groups)
                {
                    foreach (Group groupE in e.Groups)
                    {
                        if (group.Name == groupE.Name)
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
            return null;
        }
        private bool CheckFreeRoom(Event ev)
        {
            List<Event> events = dataStore.Get();
            foreach (Event e in events)
            {
                if (e.Room.Number == ev.Room.Number)
                {
                    if (e.GetEndTime() >= ev.Date && e.Date <= ev.GetEndTime())
                        return false;
                }
            }
            return true;
        }

        private bool CheckCapacity(List<Group> groups, Room room)
        {
            int totalSize = 0;
            foreach (Group group in groups)
            {
                totalSize += group.Count;
            }
            if (totalSize > room.Capacity) return false;
            else return true;
        }

    }
}
