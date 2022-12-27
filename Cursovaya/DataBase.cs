using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
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
                string json = JsonConvert.SerializeObject(_events);
                System.IO.File.WriteAllText(@"path.txt", json);
            }
        }
        
        public List<Event> Get()
        {
            if (System.IO.File.ReadAllText(@"path.txt") != "")
            _events = JsonConvert.DeserializeObject<List<Event>>(System.IO.File.ReadAllText(@"path.txt"));
            return _events;
        }

        

        
    }
}
