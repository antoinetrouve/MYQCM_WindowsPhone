using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM;
using MVVM.Interfaces;
using My_QCM.Models;
using My_QCM.ViewModels.Interfaces;
using MVVM.ViewModels;

namespace My_QCM.ViewModels
{
    public class ViewModelMcqListTest : ViewModelList<Mcq>, IViewModelMcqs
    {
        #region Contructors
        public ViewModelMcqListTest()
        {
            LoadData();
        }
        #endregion
        #region Methods
        public override void LoadData()
        {
            //this.ItemsSource.Add(new Mcq("25523-63"));
        }
        #endregion


    }
}
