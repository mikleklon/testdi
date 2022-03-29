using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.Models;

namespace TestDi3.Service
{
    public class Timer : ATask, ITimer
    {
        private INotesService _notesservice;
        public Timer(INotesService notesservice)
        {
            _notesservice = notesservice;
        }
        public override void _run()
        {
            chackNotify();
            while (DateTime.Now.Second != 0) { }
            while (true)
            {
                chackNotify();
                Task.WaitAll(Task.Delay(1000 * 60));
            }

        }

        private void chackNotify()
        {
            var zero = TimeSpan.FromSeconds(0);
            var now = _floorMin(DateTime.Now, zero);
            var notes = _notesservice.List(DateTime.Now, null);
            notes.ToList().ForEach(note =>
            {
                if (_floorMin(note.start, zero) == now)
                {
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("\nThe '{0}' event start!\n", note.name);
                    System.Console.ForegroundColor = ConsoleColor.White;
                }
                else if(_floorMin(note.start, note.notification) == now)
                {
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("\nThe '{0}' event will start in {1}(After {2} minutes)\n\n", note.name, note.start, note.notification.TotalMinutes );
                    System.Console.ForegroundColor = ConsoleColor.White;

                }
            });
        }

        private DateTime _floorMin(DateTime date, TimeSpan span)
        {
            double sec = date.Ticks / 10000000;
            sec -= span.TotalSeconds;
            var day = Math.Floor(sec / (60));
            sec = day * (60);
            return new DateTime((long)sec * 10000000);
        }
    }
}
