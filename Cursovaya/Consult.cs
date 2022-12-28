using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursovaya
{
    class Consult : Event
    {
        DateTime duration = new DateTime(1, 1, 1, 1, 30, 0);
        public DateTime Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public Consult(DateTime date, List<string> groups, string subject, string FIO, string room) : base(date, groups, subject, FIO, room) { }

        public override DateTime GetEndTime()
        {
            return this.Date.Add(duration.TimeOfDay);
        }

        public override string GetName()
        {
            return "Консультация";
        }
    }
}
