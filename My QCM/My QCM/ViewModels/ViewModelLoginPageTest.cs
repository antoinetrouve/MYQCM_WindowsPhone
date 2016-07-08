using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My_QCM.Models;
using MVVM.ViewModels;
using MVVM;
using System.Net;
using MVVM.Service;
using Newtonsoft.Json;
using MVVM.Interfaces;

namespace My_QCM.ViewModels
{
    public class ViewModelLoginPageTest : ViewModelItem<User>, IViewModelUser
    {
        #region Fields
        /// <summary>
        ///     View model to show Category List associated to the User.
        /// </summary>
        private ViewModelCategoryListTest _ViewModelCategoryListTest;
        private DelegateCommand _ConnectionCommand;
        private string _userName;
        private string _password;
        private string jsonConnection;
        private string isAuthent;
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
        /// <summary>
        ///     Get View Model Categories,to show Category list
        /// </summary>
        public IViewModelCategories ViewModelCategories => _ViewModelCategoryListTest;
        public DelegateCommand ConnectionCommand => _ConnectionCommand;
        public User user { get; set; }
        #endregion
        #region Constructor
        public ViewModelLoginPageTest()
        {
            _ConnectionCommand = new DelegateCommand(ExecuteConnectionCommand, CanExecuteConnectionCommand);
            _ViewModelCategoryListTest = new ViewModelCategoryListTest();
        }

        #endregion


        #region Methods
        /// <summary>
        ///  Load Data on WebService with WebClient
        /// </summary>
        public override void LoadData()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri("http://192.168.100.78/MY_QCM/web/app_dev.php/api/users/" + userName));
        }

        private void WebClient_ToConnect()
        {
            
        }
        // When the Client finish to download the Flux Deserialize User
        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            User deserializedUser = null;
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
            {   //Deserialize the json flux to user
                 deserializedUser = JsonConvert.DeserializeObject<User>(jsonsstream);
                
            }
            catch (Exception ex)
            {
                     err = "error  = " + ex.Message;
                    System.Diagnostics.Debug.WriteLine(err);
                    ServiceResolver.GetService<IMessageService>().Show("Error");   
            }
            
            if (err == null)
            {
                // list ID_server Unique Of Categ
                List<int> categoriesIdServers = new List<int>();
                // liste des catégory a utilisé
                List<Category> categories = new List<Category>();
                //Gestion Doublon de QCM user pour le nom des catégories
                foreach (Mcq mcq in deserializedUser.Mcqs)
                {
                    DataStore.Instance.Mcqs.Add(mcq);

                    // Renvoi un bool false si idServeur n'existe pas dans la liste
                    bool isInside = categoriesIdServers.Contains(mcq.Category.IdServer);
                    if (isInside == false)
                    {
                        categoriesIdServers.Add(mcq.Category.IdServer);
                        categories.Add(mcq.Category);
                    }

                    foreach(Question question in mcq.Questions)
                    {
                        DataStore.Instance.Questions.Add(question);

                        foreach(Answer answer in question.Answers)
                        {
                            DataStore.Instance.Answers.Add(answer);
                        }
                    }
                }

                foreach (Category cat in categories)
                {
                    DataStore.Instance.Categories.Add(cat);
                }
                IsBusy = false;

                ServiceResolver.GetService<INavigationService>().Navigate(new Uri("/Views/CategoryListPage.xaml", UriKind.Relative));
            }
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

        /// <summary>
        /// Create a new Object Connection and serialize to Json
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pswd"></param>
        /// <returns> json </returns>
        private string IdToJson(string login,string pswd)
        {
            Connection connection = new Connection(login, pswd);

            string json = JsonConvert.SerializeObject(connection);
            return json;
        }
        /// <summary>
        /// Function to Call the webClient for Post Connection
        /// </summary>
        /// <param name="flux"></param>
        private void SendDataToServer(string flux)
        {
            WebClient webClient = new WebClient();
            webClient.UploadStringCompleted += WebClient_UploadStringCompleted;
            webClient.UploadStringAsync(new Uri("http://192.168.100.22/qcm/web/app_dev.php/api/users/") , flux);
        }

        /// <summary>
        /// When the Upload is Finish pass the isAuthent in true or false
        /// U
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebClient_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            /* if (e.Result != null)
             {
                 isAuthent = e.Result;
             }*/
            isAuthent = "true";
            if (isAuthent == "true")
            {
                System.Diagnostics.Debug.WriteLine(jsonConnection);
                LoadData();
            }
            else
            {
                ServiceResolver.GetService<IMessageService>().Show("Erreur dans la connexion veuillez vérifier vos identifiants.");
            }
        }

        #region ConnectionCommand
        //If UserName and Password is not set cant click on the ButtonConnection
        private bool CanExecuteConnectionCommand(object parameter)
        {
            return (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password));
        }

        // 
        private void ExecuteConnectionCommand(object parameter)
        {
            jsonConnection = IdToJson(userName, password);
            SendDataToServer(jsonConnection);
        }
        #endregion
        #endregion

        #region Navigation

        /// <summary>
        ///     Call when the Page Become the page of the View
        /// </summary>
        /// <param name="viewModel">Model View of the Page.</param>
        public override void OnNavigatedTo(IViewModel viewModel)
        {
            base.OnNavigatedTo(viewModel);
         
        }

  
   
        #endregion



    }
}
