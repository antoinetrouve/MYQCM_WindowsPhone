using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM
{
    public class DelegateCommand : ICommand
    {
        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Fields

        private Action<object> _Execute;
        private Func<object, bool> _CanExecute;

        #endregion

        #region Constructors

        public DelegateCommand(Action<object> execute, Func<object,bool> canExecute = null )
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            _Execute = execute;
            _CanExecute = canExecute;
        }

        #endregion


        public bool CanExecute(object parameter)
        {
            return _CanExecute != null ? _CanExecute(parameter) : true;
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
