using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.Models;

namespace TestDi3.BL.StepConsole
{
    public class Edit : AStep
    {
        private Note _note;

        public Edit(Note note){
            _note = note;
        }
        public override void Menu()
        {
            string str;
            
            var now = DateTime.Now;
            var nowStr = _dateToString(now);
            Console.Clear();
            if (_note != null)
                Console.WriteLine("Edit Note:");
            else
                Console.WriteLine("Create Note:");
            _note = new();
            Console.Write("Name ({0}):", _note.name);
            _note.name = Console.ReadLine();
            Console.Write("Date Start({0}): ", _dateToString(_note.start));
            str = Console.ReadLine();

            _note.start = _tryDate(str, _note.start);
            
            Console.Write("Date End({0}):", _dateToString(_note.end));
            str = Console.ReadLine();
            _note.end = _tryDate(str, _note.end);
            Console.Write("How much to warn in minutes?({0}) ", _note.notification.TotalMinutes);
            str = Console.ReadLine();
            _note.notification = _trySpan(str, _note.notification);

            Console.WriteLine("\nCheck info:");
            Console.WriteLine("Name: {0}", _note.name);
            Console.WriteLine("Date Start: {0}: ", _dateToString(_note.start));
            Console.WriteLine("Date End {0}:", _dateToString(_note.end));
            Console.WriteLine("Notification (min): {0} ", _note.notification.TotalMinutes);

            Console.Write("Is everything right? (y/n)");
        }

        public override bool Process(string str)
        {
            switch (str)
            {
                case "y":
                case "Y":
                    try
                    {
                        _notesService.Set(_note);
                        return false;
                    }catch(Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error create note: {0}", e.Message);
                    }
                    return false;
                case "n":
                case "N":
                    return true;

                default:
                    return false;
            }
        }

    }
}
