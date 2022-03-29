using Microsoft.Extensions.DependencyInjection;
using TestDi3.Models;
using TestDi3.Service;

namespace TestDi3
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<INotesService, NotesService>()
                .AddSingleton<ITasks, Tasks>()
                .AddSingleton<IStack<IStep>, Stack<IStep>>()
                .AddSingleton<IConsole, Console>()
                .AddSingleton<ITimer, Timer>()
                .BuildServiceProvider();
            var console = serviceProvider.GetService<IConsole>();
            var timer = serviceProvider.GetService<ITimer>();
            serviceProvider.GetService<ITasks>()
                .Add(console)
                .Add(timer)
                .Run()
                .Wait();
        }
    }
}
