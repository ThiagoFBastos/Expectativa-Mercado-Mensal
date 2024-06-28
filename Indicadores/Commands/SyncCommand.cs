using System.Windows;
using System.Windows.Input;
using Indicadores.ViewModels;
using Indicadores.Utils;

namespace Indicadores.Commands
{
    public class SyncCommand: ICommand
    {
        private readonly Action<object?> _action;

        public event EventHandler CanExecuteChanged;
        public SyncCommand(Action<object?> action)
        {
            _action = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }
        public void Execute(object? parameter)
        {
            _action(parameter);
        }
    }
}
