using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVM.Data
{
    public class PropertyTrackerAction : PropertyTracker
    {
        #region Fields

        private Action<object, string> _Action;

        #endregion

        #region Properties

        public Action<object, string> Action
        {
            get { return _Action; }
        }

        #endregion

        #region Constructors

        public PropertyTrackerAction(string trackedProperty, Action<object, string> action)
            : base(trackedProperty)
        {
            _Action = action;
        }

        #endregion
    }
}
