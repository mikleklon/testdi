using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.Models;

namespace TestDi3.BL.StepConsole
{
    public class List : AStep
    {
        private DateTime _start;
        private DateTime _end;

        public List()
        {
            _start = _floorDay(DateTime.Now, TimeSpan.FromDays(0));
            _end = _floorDay(DateTime.Now, TimeSpan.FromDays(1));

        }
        public override void Menu()
        {
            Console.Clear();
            Console.WriteLine("List notes for {0} - {1}:", _dateToString(_start), _dateToString(_end));
            var _notes = _notesService.List(_start, _end);
            Console.WriteLine("ID\tName\tStart\tEnd\tNotification (min)");
            _notes.ToList().ForEach(note => {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", note.id, note.name, _dateToString(note.start), _dateToString(note.end), note.notification.TotalMinutes);
            });
            Console.WriteLine("\nCommand:");
            Console.WriteLine("create - Create note");
            Console.WriteLine("edit [id] - Edit note");
            Console.WriteLine("delete [id] - Delete note");
            Console.WriteLine("filter [start] [end] - Edit filter");
            Console.WriteLine("export - Export this notes to text file");
            Console.WriteLine("main - Exit to main meny");
        }

        public override bool Process(string str)
        {
            var ar = str.Split(" ");
            var com = ar[0];
            switch (com)
            {
                case "export":
                    _stackService.Push(new Export(_start, _end));
                    return true;
                case "create":
                    _stackService.Push(new Edit(new Note()));
                    return true;
                case "edit":
                    if (ar.Length > 1)
                    {
                        var id = _strToId(ar[1]);
                        if (id > 0)
                        {
                            var note = _notesService.Get(id);
                            if (note != null)
                            {
                                _stackService.Push(new Edit(note));
                                return true;
                            }
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uncown ID");
                    return true;
                case "delete":
                    if (ar.Length > 1)
                    {
                        var id = _strToId(ar[1]);
                        if (id > 0)
                        {
                            try
                            {
                                _notesService.Remove(id);
                                return true;
                            }
                            catch (Exception) { }
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uncown ID");
                    return true;
                case "filter":
                    if(ar.Length > 1)
                        _start = _tryDate(ar[1], _start);
                    if(ar.Length > 2)
                        _end = _tryDate(ar[2], _end);
                    return true;
                case "main":
                    return false;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uncown command");
                    return true;
            };
        }

        private DateTime _floorDay(DateTime date, TimeSpan span)
        {
            double sec = date.Ticks / 10000000;
            sec += span.TotalSeconds;
            var day = Math.Floor(sec / (60 * 60 * 24));
            sec = day * (60 * 60 * 24);
            return new DateTime((long)sec * 10000000);
        }

        private int _strToId(string str)
        {
            try
            {
                return int.Parse(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
