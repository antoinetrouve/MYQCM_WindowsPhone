using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using MVVM.Views;
using Microsoft.Phone.Shell;
using My_QCM.ViewModels;

namespace My_QCM.Views
{
    public partial class CategoryListPage : MVVMPhonePage
    {
        #region Constructor
        public CategoryListPage()
        {
            this.ViewModel = new ViewModelCategoryListTest();
            InitializeComponent();
        }
        #endregion
    }
}