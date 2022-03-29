using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.BL.StepConsole;
using TestDi3.Models;
using TestDi3.Service;

namespace TestDi3.Service
{
    public class Console: ATask, IConsole
    {
        private IStack<IStep> _stekStep;
        public Console(IStack<IStep> stekStep)
        {
            _stekStep = stekStep;
        }
        public override void _run()
        {

            _stekStep.Push(new Main());
            while (_stekStep.Count() > 0)
            {
                System.Console.ForegroundColor = ConsoleColor.White;
                _stekStep.Peek().Menu();
                System.Console.Write(">");
                string str = System.Console.ReadLine();
                if (!_stekStep.Peek().Process(str))
                    _stekStep.Pop();

                System.Console.WriteLine("\n\n");
            }

        }
    }

}
