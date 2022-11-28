using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursovaya
{
    public sealed class DataBase
    {
        private static object syncRoot = new Object();
        private List<Event> _events = new List<Event>();
        private static DataBase instance;
        public static DataBase GetInstance()
        {
            if (instance == null)
                lock (syncRoot)
                {
                    instance = new DataBase();
                }
            return instance;
        }
        private void ConcurrentDataBase()
        {
            this._events = new List<Event>();
        }

        public void AddEvent(Event event1)
        {
            lock (syncRoot)
            {
                this._events.Add(event1);
            }
        }
        
        public List<Event> Get()
        {
            return _events;
        }
    }
}
