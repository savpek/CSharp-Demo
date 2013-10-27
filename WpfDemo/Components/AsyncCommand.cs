using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace WpfDemo.Utilities
{
    public class AsyncCommand : ICommand
    {
        private readonly Action _command;
        private readonly Func<bool> _canExecute;
        private readonly Dispatcher _dispatcher;
        private bool _isExecuting;

        public AsyncCommand(Action command, 
            Func<bool> canExecute)
        {
            _command = command;
            _canExecute = canExecute;
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        public bool CanExecute(object parameter)
        {
            return 
                (_canExecute == null || _canExecute()) 
                && !_isExecuting;
        }

        public void Execute(object parameter)
        {
            if (AsynchronousIsDisabledContext.IsAsyncCommandsEnabled)
            {
                Action a = () =>
                               {
                                   try
                                   {
                                       _isExecuting = true;
                                       _command();
                                   }
                                   catch (Exception ex)
                                   {
                                       // Throw uncatched exceptions to UI thread, this probably will crash application which is good thing.
                                       _dispatcher.BeginInvoke((Action) delegate { throw ex; });
                                   }
                                   finally
                                   {
                                       _isExecuting = false;
                                   }
                               };
                a.BeginInvoke(null, null);
            }
            else
                _command();
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public void ThrowAsynExceptionsToUiThreadWrapper(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                var throwAction =
                new Action(delegate { throw ex; });
                _dispatcher.BeginInvoke(throwAction);
            }
        }
    }
}
