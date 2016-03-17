﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM.Data;
using Newtonsoft.Json;

namespace My_QCM.Models
{
    public class Mcq : ObservableObject
    {
        #region Fields

        private string _Name;
        private bool _IsActif;
        private int _Countdown;
        private DateTime _DiffDeb;
        private DateTime _DiffEnd;
        private DateTime _CreatedAt;
        private DateTime _UpdatedAt;

        //private List<Question> questions;

        #endregion

        #region Properties

        [JsonProperty("name")]
        public string Name
        {
            get { return _Name; }
            set { SetProperty(nameof(Name), ref _Name, value); }
        }

        [JsonProperty("isActif")]
        public bool IsActif
        {
            get { return _IsActif; }
            set { SetProperty(nameof(IsActif),ref _IsActif, value); }
        }

        [JsonProperty("countdown")]
        public int Countdown
        {
            get { return _Countdown; }
            set { SetProperty(nameof(Countdown), ref _Countdown, value); }
        }

        [JsonProperty("diff_deb")]
        public DateTime DiffDeb
        {
            get { return _DiffDeb; }
            set { SetProperty(nameof(DiffDeb), ref _DiffDeb, value); }
        }

        [JsonProperty("diff_end")]
        public DateTime DiffEnd
        {
            get { return _DiffEnd; }
            set { SetProperty(nameof(DiffEnd), ref _DiffEnd, value); }
        }

        [JsonProperty("created_at")]
        public DateTime CreatedAt
        {
            get { return _CreatedAt; }
            set { SetProperty(nameof(CreatedAt), ref _CreatedAt, value); }
        }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt
        {
            get { return _UpdatedAt; }
            set { SetProperty(nameof(UpdatedAt), ref _UpdatedAt, value); }
        }

        #endregion

        #region Constructor
        public Mcq(string name, bool isActif)
        {
            Name = name;
        }
        #endregion
    }
}
