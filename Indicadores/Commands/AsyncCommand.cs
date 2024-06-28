using System.Windows.Input;

namespace Indicadores.Commands
{
    public class AsyncCommand: ICommand
    {
        private bool _isExecuting;
        public event EventHandler CanExecuteChanged;
        private readonly Func<object?, Task> ExecuteAsync;

        public AsyncCommand(Func<object?, Task> func)
        {
            ExecuteAsync = func;
        }

        public bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                CanExecuteChanged.Invoke(this, new EventArgs());
            }
        }

        public bool CanExecute(object? parameter)
        {
            return !IsExecuting;
        }

        public async void Execute(object? parameter)
        {
            IsExecuting = true;
            await ExecuteAsync(parameter);
            IsExecuting = false;
        }
    }
}
