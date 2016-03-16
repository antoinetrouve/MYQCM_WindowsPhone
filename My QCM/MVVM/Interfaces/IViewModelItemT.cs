using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Interfaces
{
    public interface IViewModelItem<T> : IViewModel
    {
        T Item { get; set; }
    }
}
