using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.Models;
using TestDi3.Service;

namespace TestDi3.BL.StepConsole
{
    public abstract class AStep: IStep
    {
        protected INotesService _notesService;
        protected IStack<IStep> _stackService;
        public IStackType<IStep> SetService(INotesService notesService, IStack<IStep> stackService)
        {
            _notesService = notesService;
            _stackService = stackService;
            return this;
        }

        public abstract void Menu();

        public abstract bool Process(string str);

        protected string _dateToString(DateTime date)
        {
            var culture = CultureInfo.GetCultureInfo("ru-Ru");
            return date.ToString("g", culture);
        }

        protected DateTime _tryDate(string str, DateTime def)
        {
            try
            {
                return str != "" ? Convert.ToDateTime(str) : def;
            }
            catch (Exception)
            {
                return def;
            }
        }
        protected TimeSpan _trySpan(string str, TimeSpan def)
        {
            try
            {
                return TimeSpan.FromMinutes(int.Parse(str));
            }
            catch (Exception)
            {
                return def; ;
            }
        }

    }
}
