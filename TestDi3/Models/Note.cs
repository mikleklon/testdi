using System;
using System.Collections.Generic;
using System.Text;

namespace TestDi3.Models
{
    public class Note
    {
        public Note() {
            name = "";
            start = _floorMin(DateTime.Now, TimeSpan.FromSeconds(0));
            end = _floorMin(DateTime.Now, TimeSpan.FromHours(1));
            notification = TimeSpan.FromMinutes(0);
        }
        public void edit(Note note)
        {
            name = note.name;
            start = note.start;
            end = note.end;
            notification = note.notification;
        }
        public int? id { get; set; }
        public string name { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public TimeSpan notification { get; set; }

        private DateTime _floorMin(DateTime date, TimeSpan span)
        {
            double sec = date.Ticks / 10000000;
            sec += span.TotalSeconds;
            var day = Math.Floor(sec / (60));
            sec = day * (60);
            return new DateTime((long)sec * 10000000);
        }
    }
}
