using MVVM.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_QCM.Models
{
    public class Answer : ObservableObject
    {
        #region Fields
        private string _Value;
        private bool _IsValid;
        private DateTime _Created_at;
        private DateTime _Updated_at;
        private int _IdServer;
        #endregion

        #region Properties
        [JsonProperty("id")]
        public int IdServer
        {
            get { return _IdServer; }
            set { SetProperty(nameof(IdServer), ref _IdServer, value); }
        }

        [JsonProperty("created_at")]
        public DateTime Created_at
        {
            get { return _Created_at; }
            set { SetProperty(nameof(Created_at), ref _Created_at, value); }
        }

        [JsonProperty("updated_at")]
        public DateTime Updated_at
        {
            get { return _Updated_at; }
            set { SetProperty(nameof(Updated_at), ref _Updated_at, value); }
        }

        [JsonProperty("Value")]
        public string Value
        {
            get { return _Value; }
            set { SetProperty(nameof(Value), ref _Value, value); }
        }

        [JsonProperty("isValid")]
        public bool IsValid
        {
            get { return _IsValid; }
            set { SetProperty(nameof(IsValid), ref _IsValid, value); }
        }

        #endregion


       
        #region Constructor
        public Answer(int idServer, string value, DateTime createdAt, DateTime updatedAt, bool isValid)
        {
            IdServer = idServer;
            Value = value;
            Created_at = createdAt;
            Updated_at = updatedAt;
            IsValid = isValid;
        }
        #endregion

        #region methods
        public override string ToString()
        {
            return Value;
        }

        #endregion
    }
}
