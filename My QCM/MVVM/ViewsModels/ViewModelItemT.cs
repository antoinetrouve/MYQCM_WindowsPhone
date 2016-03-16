using MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.ViewModels
{
    public abstract class ViewModelItem<T> : ViewModel, IViewModelItem<T>
    {
        #region Fields

        private T _Item;

        #endregion

        #region Properties

        public T Item
        {
            get { return _Item; }
            set { SetProperty(nameof(Item), ref _Item, value); }
        }

        #endregion
    }
}
