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
        /// <summary>
        ///     View model to show mcq List associated to the Category.
        /// </summary>
        private ViewModelMcqListTest _ViewModelMcqListTest;
        private DelegateCommand _CategorySelectCommand;
        #endregion

        #region Properties
        /// <summary>
        ///     Get View Model Mcqs,to show Mcq list
        /// </summary>
        public IViewModelMcqs ViewModelMcqs => _ViewModelMcqListTest;
        public DelegateCommand CategorySelectCommand => _CategorySelectCommand;
        public Category category { get; set; }
        #endregion

        #region Constructor
        public ViewModelCategoryListTest()
        {
            _ViewModelMcqListTest = new ViewModelMcqListTest();
        }
        #endregion

        #region Methods

        public override void LoadData()
        {
            IsBusy = true;
            //clear itemSource list of Category
            this.ItemsSource.Clear();
            //Foreach the Category and Add to the List
            foreach (Category cat in DataStore.Instance.Categories)
            {
                this.ItemsSource.Add(cat);
            }

            IsBusy = false;
        }

        //When a Item selected inside the list
        protected override void InitializePropertyTrackers()
        {
            base.InitializePropertyTrackers();

            this.AddPropertyTrackerAction(nameof(SelectedItem), (sender, args) =>
            {
                if (SelectedItem != null)
                {
                    int id = this.SelectedItem.IdServer;
                    DataStore.Instance.Mcqs_Category.Clear();
                    foreach(Mcq mcq in DataStore.Instance.Mcqs)
                    {
                        if(mcq.Category.IdServer == id)
                        {
                            DataStore.Instance.Mcqs_Category.Add(mcq);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Absente");
                        }
                    }
                    ServiceResolver.GetService<INavigationService>().Navigate(new Uri("/Views/McqListPage.xaml", UriKind.Relative));
                }
            });
        }
        #endregion
        #region Navigation

        /// <summary>
        ///     Call when a page become the page inside a Frame
        /// </summary>
        /// <param name="viewModel">Vue-modèle de la page.</param>
        public override void OnNavigatedTo(IViewModel viewModel)
        {
            base.OnNavigatedTo(viewModel);

            //Download Data inside
            LoadData();
        }

        /// <summary>
        ///   Call when tis Page is not longer active
        /// </summary>
        /// <param name="viewModel">Page View Model.</param>
        public override void OnNavigatedFrom(IViewModel viewModel)
        {
            base.OnNavigatedFrom(viewModel);

            //If the next Model View is IViewModelCategory
            if (viewModel is IViewModelCategory)
            {
                //Give the Element.
                ((IViewModelCategory)viewModel).Item = this.SelectedItem;
                //Upload Data
                ((IViewModelCategory)viewModel).LoadData();
                //Make selector to null
                SelectedItem = null;
            }
        }

        #endregion

    }
}
