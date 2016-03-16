using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MVVM.Data;

namespace My_QCM.Models
{
    public class Category : ObservableObject
    {

        #region Fields
        private string _Name;
        #endregion

        #region Properties
        public string Name
        {
            get { return _Name; }
            set { SetProperty(nameof(Name), ref _Name, value); }
        }
        #endregion

        #region Constructor
        public Category( string name = null)
        {
            Name = name;
        }
        #endregion

        #region methods
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
