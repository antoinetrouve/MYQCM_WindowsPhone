using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My_QCM.Models;
using My_QCM.ViewModels.Interfaces;
using MVVM.ViewModels;
using MVVM;
using System.Net;
using MVVM.Service;
using Newtonsoft.Json;

namespace My_QCM.ViewModels
{
    public class ViewModelLoginPageTest :  ViewModel,IViewModelUser
    {
        #region Fields
        private DelegateCommand _ConnectionCommand;
        private string _userName;
        private string _password;
        private string err = null;
        #endregion

        public string password
        {
            get { return _password; }
            set { SetProperty(nameof(password), ref _password, value); }
        }

        public string userName
        {
            get { return _userName; }
            set { SetProperty(nameof(userName), ref _userName, value); }
        }


        #region Properties
        public DelegateCommand ConnectionCommand => _ConnectionCommand;
        public User user { get; set; }
        #endregion
        #region Constructor
        public ViewModelLoginPageTest()
        {
            _ConnectionCommand = new DelegateCommand(ExecuteConnectionCommand, CanExecuteConnectionCommand);
        }
        
        #endregion

        #region Methods
        public override void LoadData()
        {
            System.Diagnostics.Debug.WriteLine("Bonjour");
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri("http://192.168.1.14/qcm/web/app_dev.php/api/users/" + userName));
        }
        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            err = null;
            string jsonsstream = null;
            try
            {
                jsonsstream = e.Result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    string err = ex.InnerException.Message;
                }
            }

            try
            {
                User deserializedUser = JsonConvert.DeserializeObject<User>(jsonsstream);
            }
            catch (Exception ex)
            {
                     err = "error  = " + ex.Message;
                    System.Diagnostics.Debug.WriteLine(err);
                    ServiceResolver.GetService<IMessageService>().Show("Error");
                
            }
            
            if (err == null)
            {
                System.Diagnostics.Debug.WriteLine(err + " supoose to be null");
                ServiceResolver.GetService<INavigationService>().Navigate(new Uri("/Views/CategoryListPage.xaml", UriKind.Relative));
            }
            //// liste des id server sans doublon
            //List<int> categoriesIdServers = new List<int>();
            //// liste des catégory a utilisé
            //List<Category> categories = new List<Category>();
            ////Gestion Doublon de QCM user pour le nom des catégoeries
            //foreach (Mcq mcq in deserializedUser.Mcqs)
            //{
            //    // Renvoi un bool false si idServeur n'existe pas dans la liste
            //    bool isInside = categoriesIdServers.Contains(mcq.Category.IdServer);
            //    if (isInside == false)
            //    {
            //        categoriesIdServers.Add(mcq.Category.IdServer);
            //        categories.Add(mcq.Category);
            //    }
            //}

            //foreach (Category cat in categories)
            //{
            //    this.ItemsSource.Add(cat);
            //}
            //IsBusy = false;
        }

        protected override void InitializePropertyTrackers()
        {
            base.InitializePropertyTrackers();

            this.AddPropertyTrackerAction(nameof(userName), (sender, args) =>
            {
                _ConnectionCommand.OnCanExecuteChanged();
            });
            this.AddPropertyTrackerAction(nameof(password), (sender, args) =>
            {
                _ConnectionCommand.OnCanExecuteChanged();
            });

        }

        #region ConnectionCommand

        private bool CanExecuteConnectionCommand(object parameter)
        {
            System.Diagnostics.Debug.WriteLine(userName);
            System.Diagnostics.Debug.WriteLine(password);
            return (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password));
        }

        private void ExecuteConnectionCommand(object parameter)
        {
            LoadData();
          
        }
        #endregion
        #endregion


    }
}
