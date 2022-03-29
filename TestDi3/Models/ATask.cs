using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.Service;

namespace TestDi3.Models
{
    public abstract class ATask: ITask
    {
        protected NotesService _notesService;
        public ITask SetService(NotesService notesService)
        {
            _notesService = notesService;
            return this;
        }
        public Task Run()
        {
            return Task.Run(() => _run());
        }
        public abstract void _run();
    }
}
