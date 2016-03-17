using My_QCM.ViewModels;
using MVVM.Interfaces;
using MVVM.ViewModels;
using My_QCM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Windows.Networking.Connectivity;

namespace My_QCM.ViewModels
{
    public class ViewModelCategoryListTest : ViewModelList<Category>, IViewModelCategories
    {
        #region Constructors

        public ViewModelCategoryListTest()
        {
            LoadData();
        }
        #endregion
        #region Methods

        public override void LoadData()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri("http://IPSERVERWEB/qcm/web/app_dev.php/api/users/antoine"));
        }

        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string jsonsstream = e.Result;
            System.Diagnostics.Debug.WriteLine(jsonsstream);
            User deserializedUser = JsonConvert.DeserializeObject<User>(jsonsstream);
            //List<Category> deserializedProduct = JsonConvert.DeserializeObject<List<Category>>(jsonsstream);
            //deserializedUser.Mcqs.Concat(deserializedUser.Team.Mcqs).Distinct();

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

            
        }

        #endregion

        #region Methods

        protected override void InitializePropertyTrackers()
        {
            base.InitializePropertyTrackers();

            this.AddPropertyTrackerAction(nameof(SelectedItem), (sender, args) =>
            {
                if (SelectedItem != null)
                {

                }
            });
        }

        public override void OnNavigatedFrom(IViewModel viewModel)
        {
            if (viewModel is IViewModelCategory)
            {
                ((IViewModelCategory)viewModel).Item = this.SelectedItem;
                ((IViewModelCategory)viewModel).LoadData();
                SelectedItem = null;
            }
        }
        #endregion
    }
}
