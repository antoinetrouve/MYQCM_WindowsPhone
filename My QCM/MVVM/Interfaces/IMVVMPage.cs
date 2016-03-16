using MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Interfaces
{
    public interface IMVVMPage
    {
        IViewModel ViewModel { get; set; }
    }
}
