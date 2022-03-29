using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.Models;

namespace TestDi3.BL.StepConsole
{
    public class Export : AStep
    {
        private DateTime? _start;
        private DateTime? _end;

        public Export(DateTime? start, DateTime? end)
        {
            _start = start;
            _end = end;

        }
        public override void Menu()
        {
            Console.WriteLine("File name?");
        }

        public override bool Process(string str)
        {
            if (str != "")
                _notesService.toFile(str, _start, _end);
            return false;
        }


    }
}
