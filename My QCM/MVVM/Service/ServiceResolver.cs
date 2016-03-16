using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Service
{
    public class ServiceResolver
    {
        #region Fields

        private static Dictionary<Type, IService> _Services;

        #endregion

        #region Constructors

        static ServiceResolver()
        {
            _Services = new Dictionary<Type, IService>();
        }

        #endregion

        #region Methods

        public static void RegisterService<T>(T service, bool overrideIfExist = true)
            where T : IService
        {
            Type serviceType = typeof(T);
            bool serviceAllreadyRegistered = _Services.ContainsKey(serviceType);

            if (!serviceAllreadyRegistered)
            {
                _Services.Add(serviceType, service);
            }
            if (serviceAllreadyRegistered && overrideIfExist)
            {

            }
        }

        public static T GetService<T>()
            where T : IService
        {
            Type serviceType = typeof(T);
            T service = default(T);

            if (_Services.ContainsKey(serviceType))
            {
                service = (T)_Services[serviceType];
            }

            return service;
        }

        #endregion
    }
}
