using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Service
{
    public interface INavigationService : IService
    {
        bool Navigate(Uri uri);

        void GoBack();
    }
}
