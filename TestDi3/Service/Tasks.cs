using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.Models;

namespace TestDi3.Service
{
    public class Tasks: ITasks
    {
        private INotesService _notesService;
        private List<ITask> _tasks = new();
        public Tasks(INotesService notesService)
        {
            _notesService = notesService;
        }
        public ITasks Add(ITask task)
        {
            _tasks.Add(task);
            return this;
        }
        public async Task Run()
        {
            List<Task> list = new();
            _tasks.ForEach(t => list.Add(t.Run()));
            await Task.WhenAny(list.ToArray());
        }
    }
}
