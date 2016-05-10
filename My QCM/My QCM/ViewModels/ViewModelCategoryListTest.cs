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
            foreach (Category cat in DataStore.Instance.Categories)
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

    }
}
