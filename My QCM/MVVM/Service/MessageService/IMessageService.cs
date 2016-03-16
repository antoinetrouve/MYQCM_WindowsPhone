using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Service
{
    public interface IMessageService : IService
    {
        void Show(string message);
    }
}
