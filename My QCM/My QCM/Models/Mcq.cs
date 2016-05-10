using System;
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
        // timer to answer questions
        private int _Countdown;
        // publication start and end time
        private DateTime _DiffDeb;
        private DateTime _DiffEnd;
        private DateTime _CreatedAt;
        private DateTime _UpdatedAt;
        private Category _Category;
        private int _IdServer;
        #endregion

        #region Properties

        [JsonProperty("id")]
        public int IdServer
        {
            get { return _IdServer; }
            set { SetProperty(nameof(IdServer), ref _IdServer, value); }
        }
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

        
        public Category Category
        {
            get { return _Category; }
            set { SetProperty(nameof(Category),ref _Category , value); }
        }

        [JsonProperty("questions")]
        public List<Question> Questions { get; set; }
        #endregion

        #region Constructor
        public Mcq()
        {
           
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
