using Microsoft.Phone.Controls;
using MVVM.Interfaces;
using MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MVVM.Views
{
    public class MVVMPhonePage : PhoneApplicationPage, IMVVMPage
    {
        #region Properties

        public IViewModel ViewModel
        {
            get { return this.DataContext as IViewModel; }
            set { this.DataContext = value; }
        }

        #endregion

        #region Methods

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            bool cancel = e.Cancel;
            this.ViewModel.OnNavigatingFrom(e.Uri, ref cancel, e.IsCancelable);
            e.Cancel = cancel;
            base.OnNavigatingFrom(e);

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.Content is IMVVMPage)
            {
                this.ViewModel.OnNavigatedFrom(((IMVVMPage)e.Content).ViewModel);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Content is IMVVMPage)
            {
                this.ViewModel.OnNavigatedTo(((IMVVMPage)e.Content).ViewModel);
            }
        }

        #endregion
    }
}
