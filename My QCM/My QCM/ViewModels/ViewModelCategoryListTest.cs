using BookShop.ViewModels;
using MVVM.Interfaces;
using MVVM.ViewModels;
using My_QCM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_QCM.ViewModels
{
   public class ViewModelCategoryListTest : ViewModelList<Category>,IViewModelCategories  
    {
        #region Constructors

        public ViewModelCategoryListTest()
        {
            LoadData();
        }
        #endregion
        #region Methods

        public override  void LoadData()
        {
            this.ItemsSource.Add(new Category("Roman"));
            this.ItemsSource.Add(new Category("SFI"));
            this.ItemsSource.Add(new Category("Romance"));
            this.ItemsSource.Add(new Category("Fantastique"));
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
