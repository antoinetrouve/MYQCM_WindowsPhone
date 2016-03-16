using MVVM.Data;
using MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.ViewModels
{
    public abstract class ViewModelList<T> : ViewModel, IViewModelList<T>
    {
        #region Fields

        private ObservableCollection<T> _ItemsSource;
        private T _SelectedItem;
        private DelegateCommand _AddItemCommand;

        #endregion

        #region Properties

        public ObservableCollection<T> ItemsSource
        {
            get { return _ItemsSource; }
            set { SetProperty(nameof(ItemsSource), ref _ItemsSource, value); }
        }

        public T SelectedItem
        {
            get { return _SelectedItem; }
            set { SetProperty(nameof(SelectedItem), ref _SelectedItem, value); }
        }

        public DelegateCommand AddItemCommand => _AddItemCommand;

        #endregion

        #region Constructors

        public ViewModelList()
        {
            ItemsSource = new ObservableCollection<T>();

            _AddItemCommand = new DelegateCommand(ExecuteAddItem, CanExecuteAddItem);
        }

        #endregion

        #region Methods

        #region AddItemCommand

        protected virtual bool CanExecuteAddItem(object parameter)
        {
            return false;
        }

        protected virtual void ExecuteAddItem(object parameter)
        {

        }

        #endregion

        #endregion
    }
}
