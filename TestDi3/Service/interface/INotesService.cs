using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.Models;

namespace TestDi3.Service
{
    public interface INotesService
    {
        Note[] List(DateTime? start, DateTime? end);
        Note? Get(int id);
        int Set(Note note);

        void Remove(int id);
        void Check(Note note);

        void toFile(string name, DateTime? start, DateTime? end);
    }
}
