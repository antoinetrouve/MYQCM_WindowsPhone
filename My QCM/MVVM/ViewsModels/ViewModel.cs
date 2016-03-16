using MVVM.Data;
using MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MVVM.ViewModels
{
    public abstract class ViewModel : ObservableObject, IViewModel
    {
        #region Fields

        private bool _IsBusy;

        #endregion

        #region Properties

        public bool IsBusy
        {
            get { return _IsBusy; }
            set { SetProperty(nameof(IsBusy), ref _IsBusy, value); }
        }

        #endregion

        #region Methods

        public abstract void LoadData();

        public virtual void OnNavigatedFrom(IViewModel viewModel)
        {
        }

        public virtual void OnNavigatedTo(IViewModel viewModel)
        {
        }

        public virtual void OnNavigatingFrom(Uri uri, ref bool cancel, bool isCancelable)
        {
        }

        #endregion

    }
}
