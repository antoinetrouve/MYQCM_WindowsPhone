using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MVVM.Data
{
    public class ObservableObject : INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        #region Fields

        /// <summary>
        ///     Liste des traqueurs de propriétés
        /// </summary>
        private List<PropertyTrackerDependencies> _PropertyTrackerDependencies;

        /// <summary>
        ///     Liste des traqueurs de propriétés avec action.
        /// </summary>
        private List<PropertyTrackerAction> _PropertyTrackerActions;

        #endregion

        #region Constructors

        public ObservableObject()
        {
            InitializePropertyTrackers();
        }

        #endregion

        #region Methods

        #region PropertyTracker

        protected virtual void InitializePropertyTrackers()
        {
            _PropertyTrackerActions = new List<PropertyTrackerAction>();
            _PropertyTrackerDependencies = new List<PropertyTrackerDependencies>();
        }

        /// <summary>
        ///     Ajoute un traqueur de propriété.
        /// </summary>
        /// <param name="trackedProperty">Propriété à suivre.</param>
        /// <param name="dependentProperties">Propriétés dépendances.</param>
        public void AddPropertyTrackerDependencies(string trackedProperty, params string[] dependentProperties)
        {
            _PropertyTrackerDependencies.Add(new PropertyTrackerDependencies(trackedProperty, dependentProperties));
        }

        /// <summary>
        ///     Ajoute un traqueur de propriété avec action.
        /// </summary>
        /// <param name="trackedProperty">Propriété à suivre.</param>
        /// <param name="action">Action à déclencher.</param>
        public void AddPropertyTrackerAction(string trackedProperty, Action<object, string> action)
        {
            _PropertyTrackerActions.Add(new PropertyTrackerAction(trackedProperty, action));
        }

        #endregion

        protected void SetProperty<T>(string propertyName, ref T field, T newValue)
        {
            if (field == null && newValue != null ||
                field != null && !field.Equals(newValue))
            {
                OnPropertyChanging(propertyName);
                field = newValue;
                OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        ///     Déclenche l'évènement <see cref="ObservableObject.PropertyChanging"/>.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui s'apprète à changer.</param>
        protected virtual void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        ///     Déclenche l'évènement <see cref="ObservableObject.PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété qui a changé.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            //C#5
            //PropertyChangedEventHandler handler = PropertyChanged;

            //if (handler != null)
            //{
            //    handler(this, new PropertyChangedEventArgs(propertyName));
            //}

            //C#6
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (_PropertyTrackerDependencies != null)
            {
                foreach (string dependentProperty in _PropertyTrackerDependencies.Where(propertyTracker => propertyTracker.TrackedProperty == propertyName).SelectMany(propertyTracker => propertyTracker.DependentProperties))
                {
                    OnPropertyChanged(dependentProperty);
                }
            }
            if (_PropertyTrackerActions != null)
            {
                foreach (PropertyTrackerAction propertyTrackerAction in _PropertyTrackerActions.Where(propertyTracker => propertyTracker.TrackedProperty == propertyName))
                {
                    propertyTrackerAction.Action(this, propertyName);
                }
            }
        }


        #endregion
    }
}
