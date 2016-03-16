using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Interfaces
{
    public interface IViewModelList<T> : IViewModel
    {
        T SelectedItem { get; set; }

        ObservableCollection<T> ItemsSource { get; set; }

        DelegateCommand AddItemCommand { get; }

    }
}
