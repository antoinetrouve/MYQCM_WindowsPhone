using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVM.Data
{
    public class PropertyTrackerDependencies : PropertyTracker
    {
        #region Fields

        private string[] _DependentProperties;

        #endregion

        #region Properties

        public string[] DependentProperties
        {
            get { return _DependentProperties; }
        }

        #endregion

        #region Constructors
        public PropertyTrackerDependencies(string trackedProperty, params string[] dependentProperties)
            : base(trackedProperty)
        {
            _DependentProperties = dependentProperties;
        }

        #endregion
    }
}
