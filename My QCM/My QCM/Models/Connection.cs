using MVVM.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_QCM.Models
{
    class Connection : ObservableObject
    {

        #region Fields
        private string _UserName;
        private string _Pswd;
        #endregion

        [JsonProperty("userName")]
        public string UserName
        {
            get { return _UserName; }
            set { SetProperty(nameof(UserName), ref _UserName, value); }
        }

        [JsonProperty("pswd")]
        public string Pswd
        {
            get { return _Pswd; }
            set { SetProperty(nameof(_Pswd), ref _Pswd, value); }
        }

        #region Constructor
        public Connection(string userName, string pswd)
        {
            UserName = userName;
            Pswd = pswd;
        }
        #endregion

        #region methods
        public override string ToString()
        {
            return UserName;
        }
        #endregion

    }
}
