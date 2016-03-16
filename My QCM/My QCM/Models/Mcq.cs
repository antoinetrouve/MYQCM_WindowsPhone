using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM.Data;

namespace My_QCM.Models
{
    public class Mcq : ObservableObject
    {
        #region Fields

        private string _Name;
        private bool _isActif;
        private int _countdown;
        private DateTime _diffDeb;
        private DateTime _diffEnd;
        private DateTime _createdAt;
        private DateTime _updatedAt;

        private List<Category> categories;
        private List<Question> questions;

        #endregion

        #region Properties

        public string Name
        {
            get { return _Name; }
            set { SetProperty(nameof(Name), ref _Name, value); }
        }

        public bool isActif
        {
            get { return _isActif; }
            set { SetProperty(nameof(isActif),ref _isActif, value); }
        }

        public int countdown
        {
            get { return _countdown; }
            set { SetProperty(nameof(countdown), ref _countdown, value); }
        }
        
        public DateTime diffDeb
        {
            get { return _diffDeb; }
            set { SetProperty(nameof(diffDeb), ref _diffDeb, value); }
        }
        
        public DateTime diffEnd
        {
            get { return _diffEnd; }
            set { SetProperty(nameof(diffEnd), ref _diffEnd, value); }
        }
        
        public DateTime createdAt
        {
            get { return _createdAt; }
            set { SetProperty(nameof(createdAt), ref _createdAt, value); }
        }
        
        public DateTime updatedAt
        {
            get { return _updatedAt; }
            set { SetProperty(nameof(updatedAt), ref _updatedAt, value); }
        }

        #endregion

        #region Constructor
        public Mcq(string name)
        {
            Name = name;
        }
        #endregion
    }
}
