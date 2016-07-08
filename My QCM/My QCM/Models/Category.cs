using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MVVM.Data;
using Newtonsoft.Json;

namespace My_QCM.Models
{
    public class Category : ObservableObject
    {

        #region Fields
        private string _Name;
        private DateTime _Created_at;
        private DateTime _Updated_at;
        private int _IdServer;
        #endregion

        /// <summary>
        /// Json property to define element recup when the json flux was Deserialize 
        /// </summary>

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

        [JsonProperty("name")]
        public string Name
        {
            get { return _Name; }
            set { SetProperty(nameof(Name), ref _Name, value); }
        }

       

        #region Constructor
        public Category( int idServer, string name, DateTime createdAt,DateTime updatedAt)
        {
            IdServer = idServer;
            Name = name;
            Created_at = createdAt;
            Updated_at = updatedAt;
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
