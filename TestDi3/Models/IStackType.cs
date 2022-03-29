using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.Service;

namespace TestDi3.Models
{
    public interface IStackType<T> where T : IStackType<T>
    {
        IStackType<T> SetService(INotesService notesService, IStack<T> stackService);
    }
}
