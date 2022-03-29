using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.Models;

namespace TestDi3.BL.StepConsole
{
    public class Main : AStep
    {

        public override void Menu()
        {
            Console.Clear();
            Console.WriteLine("Main menu:");
            Console.WriteLine("view - View notes");
            Console.WriteLine("create - Create note");
            Console.WriteLine("export - Export all notes to text file");
            Console.WriteLine("exit - to exit program");
        }

        public override bool Process(string str)
        {
            switch (str)
            {
                case "view":
                    _stackService.Push(new List());
                    return true;
                case "create":
                    _stackService.Push(new Edit(new Note()));
                    return true;
                case "export":
                    _stackService.Push(new Export(null, null));
                    return true;
                case "exit":
                    return false;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uncown command");
                    return true;
            };
        }
    }
}
