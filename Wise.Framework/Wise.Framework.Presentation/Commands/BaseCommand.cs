﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wise.Framework.Presentation.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public abstract bool CanExecute(object parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public abstract void Execute(object parameter);
    }
}
