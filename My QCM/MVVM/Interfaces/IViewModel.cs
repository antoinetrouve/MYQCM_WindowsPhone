using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MVVM.Interfaces
{
    public interface IViewModel : INotifyPropertyChanged, INotifyPropertyChanging
    {
        bool IsBusy { get; set; }
        void LoadData();
        void OnNavigatingFrom(Uri uri, ref bool cancel, bool isCancelable);
        void OnNavigatedFrom(IViewModel viewModel);
        void OnNavigatedTo(IViewModel viewModel);
    }
}
