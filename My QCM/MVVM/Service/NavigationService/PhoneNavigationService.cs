using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Phone.Controls;

namespace MVVM.Service
{
    public class PhoneNavigationService : INavigationService
    {
        #region Fields

        private PhoneApplicationFrame _PhoneApplicationFrame;

        #endregion

        #region Properties

        public PhoneApplicationFrame PhoneApplicationFrame
        {
            get { return _PhoneApplicationFrame; }
        }

        #endregion

        #region Constructors

        public PhoneNavigationService(PhoneApplicationFrame phoneApplicationFrame)
        {
            if (phoneApplicationFrame == null)
            {
                throw new ArgumentNullException(nameof(phoneApplicationFrame));
            }
            _PhoneApplicationFrame = phoneApplicationFrame;
        }

        #endregion

        #region Methods

        public bool Navigate(Uri uri)
        {
            return PhoneApplicationFrame.Navigate(uri) == true;
        }

        public void GoBack()
        {
            PhoneApplicationFrame.GoBack();
        }

        #endregion
    }
}
