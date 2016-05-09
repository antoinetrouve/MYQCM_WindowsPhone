using MVVM;
using MVVM.Interfaces;
using MVVM.ViewModels;
using MVVM.Service;
using My_QCM.Models;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Networking.Connectivity;


namespace My_QCM.ViewModels
{
    public class ViewModelCategoryListTest : ViewModelList<Category>, IViewModelCategories
    {
        #region Fields
        private DelegateCommand _CategorySelectCommand;
        #endregion

        #region Properties
        public DelegateCommand CategorySelectCommand => _CategorySelectCommand;
        public Category category { get; set; }
        #endregion
        
        #region Methods

        public override void LoadData()
        {
            IsBusy = true;
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri("http://192.168.1.14/qcm/web/app_dev.php/api/users/antoine"));
        }

        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
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

            User deserializedUser = JsonConvert.DeserializeObject<User>(jsonsstream);

            // liste des id server sans doublon
            List<int> categoriesIdServers = new List<int>();
            // liste des catégory a utilisé
            List<Category> categories = new List<Category>();
            //Gestion Doublon de QCM user pour le nom des catégoeries
            foreach (Mcq mcq in deserializedUser.Mcqs)
            {
                // Renvoi un bool false si idServeur n'existe pas dans la liste
                bool isInside = categoriesIdServers.Contains(mcq.Category.IdServer);
                if (isInside == false)
                {
                    categoriesIdServers.Add(mcq.Category.IdServer);
                    categories.Add(mcq.Category); 
                }
            }

            foreach (Category cat in categories)
            {
                this.ItemsSource.Add(cat);
            }
            IsBusy = false;
        }

        protected override void InitializePropertyTrackers()
        {
            base.InitializePropertyTrackers();

            this.AddPropertyTrackerAction(nameof(SelectedItem), (sender, args) =>
            {
                if (SelectedItem != null)
                {
                    ServiceResolver.GetService<INavigationService>().Navigate(new Uri("/Views/McqListPage.xaml", UriKind.Relative));
                }
            });
        }
        #endregion
        #region Navigation

        /// <summary>
        ///     Appelé lorsqu'une page devient la page active dans une frame.
        /// </summary>
        /// <param name="viewModel">Vue-modèle de la page.</param>
        public override void OnNavigatedTo(IViewModel viewModel)
        {
            base.OnNavigatedTo(viewModel);

            //Chargement des données lorsque l'on arrive sur la page.
            LoadData();
        }

        /// <summary>
        ///     Appelé lorsqu'une page n'est plus la page active dans une frame.
        /// </summary>
        /// <param name="viewModel">Vue-modèle de la page.</param>
        public override void OnNavigatedFrom(IViewModel viewModel)
        {
            base.OnNavigatedFrom(viewModel);

            //Si le vue-modèle de la page suivante est celui de la fiche d'une catégorie.
            if (viewModel is IViewModelCategory)
            {
                //On donné l'élément sélectionné au vue-modèle.
                ((IViewModelCategory)viewModel).Item = this.SelectedItem;
                //On charge les données
                ((IViewModelCategory)viewModel).LoadData();
                //On remet la sélection à null.
                SelectedItem = null;
            }
        }

        #endregion

        #region CategoryAddCommand

        private bool CanExecuteCategoryAddCommand(object parameter)
        {
            return !string.IsNullOrWhiteSpace(category.ToString());
        }

        private void ExecuteCategoryAddCommand(object parameter)
        {
            Debug.WriteLine(this.SelectedItem.Name);
        }
        #endregion

    }
}
