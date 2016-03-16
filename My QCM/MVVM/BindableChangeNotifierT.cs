using MVVM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM
{
    public sealed class BindableChangeNotifier<T> : ObservableObject
    {
        #region Fields

        private T _Instance;

        #endregion

        #region Properties

        public T Instance => _Instance;

        #endregion

        #region Constructors

        public BindableChangeNotifier(T instance)
        {
            _Instance = instance;
        }

        #endregion

        #region Methods

        public void OnPropertyChanged()
        {
            OnPropertyChanged("");
        }

        #endregion
    }
}
