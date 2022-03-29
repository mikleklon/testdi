using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TestDi3.Models;

namespace TestDi3.Service
{
    public class NotesService : INotesService
    {
        private Dictionary<int, Note> _notes = new();
        public Note[] List(DateTime? start, DateTime? end){
            List<Note> list = new();
            var zero = TimeSpan.FromSeconds(0);
            _notes.ToList().ForEach(keyval => {
                var note = keyval.Value;
                
                if (
                    (start == null && end == null)
                    ||
                    (
                        (start == null || _floorMin(note.start) >= _floorMin((DateTime)start))
                        &&
                        (end == null || _floorMin(note.start) <= _floorMin((DateTime)end))
                    )
                )
                    list.Add(note);
            });
            return list.ToArray();
        }

        public Note? Get(int id)
        {
            return _notes.GetValueOrDefault(id);
        }

        public int Set(Note note)
        {
            Check(note);
                
            if(note.id != null)
            {
                var n = Get((int)note.id);
                if (n == null)
                    throw new Exception(EError.NoElement.ToString());
                n.edit(note);
            }
            else
            {
                int id = 1;
                _notes.ToList().ForEach(n => {
                    if ((int)n.Value.id >= id)
                        id = (int)n.Value.id + 1;
                });
                note.id = id;
                _notes.Add(id, note);
            }
            return (int)note.id;
        }
        public void Check(Note note)
        {
            if (note.start > note.end)
                throw new Exception(EError.NoCheckDate.ToString());
            _notes.ToList().ForEach(n =>
            {
                var value = n.Value;
                if (
                    note.start > value.start && note.start < value.end
                    ||
                    note.end > value.start && note.end < value.end
                )
                    throw new Exception(EError.NotesOverlapByDate.ToString());
            });
        }

        public void Remove(int id)
        {
            if (Get(id) == null)
                throw new Exception(EError.NoElement.ToString());
            _notes.Remove(id);
        }

        public void toFile(string name, DateTime? start, DateTime? end)
        {
            var notes = List(start, end);
            using (StreamWriter outputFile = new StreamWriter(name))
            {
                outputFile.WriteLine("List notes for {0} - {1}:", _dateToString(start), _dateToString(end));
                outputFile.WriteLine("ID\tName\tStart\tEnd\tNotification (min)");
                _notes.ToList().ForEach(keyvalue => {
                    var note = keyvalue.Value;
                    outputFile.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", note.id, note.name, _dateToString(note.start), _dateToString(note.end), note.notification.TotalMinutes);
                });

            }
        }
        private string _dateToString(DateTime? date)
        {
            if (date == null)
                return "";
            var culture = CultureInfo.GetCultureInfo("ru-Ru");
            return ((DateTime)date).ToString("g", culture);
        }
        private DateTime _floorMin(DateTime date)
        {
            double sec = date.Ticks / 10000000;
            var day = Math.Floor(sec / (60));
            sec = day * (60);
            return new DateTime((long)sec * 10000000);
        }
    }
}
