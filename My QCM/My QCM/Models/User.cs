using MVVM.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_QCM.Models
{
    public class User : ObservableObject
    {
        #region Fields
        private DateTime _Created_at;
        private DateTime _Updated_at;
        private string _Username;
        private string _Email;
        private int _IdServer;
        private DateTime _LastLogin;
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

        [JsonProperty("username")]
        public string Username
        {
            get { return _Username; }
            set { SetProperty(nameof(Username), ref _Username, value); }
        }

        [JsonProperty("email")]
        public string Email
        {
            get { return _Email; }
            set { SetProperty(nameof(Email), ref _Email, value); }
        }

        [JsonProperty("last_login")]
        public DateTime LastLogin
        {
            get { return _LastLogin; }
            set { SetProperty(nameof(LastLogin), ref(_LastLogin),value); }
        }

        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("mcqs")]
        public List<Mcq> Mcqs { get; set; }

        #endregion

        #region Constructor
        public User(int idServer,string username,string email, Team team, DateTime createdAt, DateTime updatedAt, DateTime lastlogin, List<Mcq> mcqs)
        {
            IdServer = idServer;
            Team = team;
            Email = email;
            Username = username;
            Created_at = createdAt;
            Updated_at = updatedAt;
            lastlogin = LastLogin = lastlogin;
            Mcqs = mcqs;
        }
        #endregion
    }
}
