using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.Models;

namespace TestDi3.Service
{
    public class Stack<T> : IStack<T> where T: IStackType<T>
    {
        private System.Collections.Generic.Stack<T> _stack = new();

        protected INotesService _notesService;

        public Stack(INotesService notesService)
        {
            _notesService = notesService;
        }
        public T Peek()
        {
            return _stack.Peek();
        }

        public T Pop()
        {
            return _stack.Pop();
        }

        public void Push(T step)
        {
            step.SetService(_notesService, this);
            _stack.Push(step);
        }

        public int Count()
        {
            return _stack.Count();
        }
    }
}
