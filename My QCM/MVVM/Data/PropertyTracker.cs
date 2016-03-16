using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVM.Data
{
    public abstract class PropertyTracker
    {
        #region Fields

        private string _TrackedProperty;

        #endregion
        
        #region Properties

        public string TrackedProperty
        {
            get { return _TrackedProperty; }
        }

        #endregion

        #region Constructors

        public PropertyTracker(string trackedProperty)
        {
            _TrackedProperty = trackedProperty;
        }

        #endregion
    }
}
