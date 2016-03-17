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
            webClient.DownloadStringAsync(new Uri("http://192.168.100.22/qcm/web/app_dev.php/api/categories"));
        }

        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string jsonsstream = e.Result;
            System.Diagnostics.Debug.WriteLine(jsonsstream);
            //Category deserializedProduct = JsonConvert.DeserializeObject<Category>(jsonsstream);
            List<Category> deserializedProduct = JsonConvert.DeserializeObject<List<Category>>(jsonsstream);
            
            //Category[] deserializedProduct = JsonConvert.DeserializeObject<Category[]>(jsonsstream);
           
            foreach (Category category in deserializedProduct)
            {
                this.ItemsSource.Add(category);
                foreach (Mcq mcq in category.Mcqs)
                {
                 System.Diagnostics.Debug.WriteLine(mcq.Name);
                }
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
