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
            List<string> ipAddresses = new List<string>();

            var hostnames = NetworkInformation.GetHostNames();
            foreach (var hn in hostnames)
            {
                if (hn.IPInformation != null)
                {
                    string ipAddress = hn.DisplayName;
                    ipAddresses.Add(ipAddress);
                }
            }

            //IPAddress address = IPAddress.Parse(ipAddresses[0]);
            //System.Diagnostics.Debug.WriteLine(address);

            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri("http://192.168.100.22/qcm/web/app_dev.php/api/users"));
            this.ItemsSource.Add(new Category("Roman"));
            this.ItemsSource.Add(new Category("SFI"));
            this.ItemsSource.Add(new Category("Romance"));
            this.ItemsSource.Add(new Category("Fantastique"));
        }

        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            string jsonsstream = e.Result;
            System.Diagnostics.Debug.WriteLine(jsonsstream);
            Category deserializedProduct = JsonConvert.DeserializeObject<Category>(jsonsstream);
            System.Diagnostics.Debug.WriteLine( deserializedProduct.Name);

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
