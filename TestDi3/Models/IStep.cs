﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDi3.Service;

namespace TestDi3.Models
{
    public interface IStep : IStackType<IStep>
    {
        void Menu();
        bool Process(string str);
    }
}
