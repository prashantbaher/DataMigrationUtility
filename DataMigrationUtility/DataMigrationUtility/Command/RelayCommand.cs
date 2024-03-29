﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataMigrationUtility.Command
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new NullReferenceException("Execute");
            }

            _execute = execute;
            _canExecute = canExecute ?? (x => true);
        }
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        { }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter) => _canExecute(parameter);
        //{
        //    return _canExecute == null ? true : _canExecute(parameter);
        //}

        public void Execute(object parameter) => _execute(parameter);
        //{
        //    _execute.Invoke(parameter);
        //}
        public void Refresh() => CommandManager.InvalidateRequerySuggested();
    }
}
