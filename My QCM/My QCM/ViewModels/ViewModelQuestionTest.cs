using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM.Interfaces;
using My_QCM.Models;
using MVVM.ViewModels;
using MVVM.Service;

namespace My_QCM.ViewModels
{
    class ViewModelQuestionTest : ViewModelList<Answer>, IViewModelAnswers
    {
        public override void LoadData()
        {
           
            this.ItemsSource.Clear();

            foreach(Question question in DataStore.Instance.McqSelected.Questions )
            {   
                foreach(Answer answer in question.Answers)
                {
                    this.ItemsSource.Add(answer);
                }
            }
        }


        #region Method
        protected override void InitializePropertyTrackers()
        {
            base.InitializePropertyTrackers();

            this.AddPropertyTrackerAction(nameof(this.SelectedItem), (sender, args) =>
            {
                if (SelectedItem != null)
                {
                    ServiceResolver.GetService<INavigationService>().Navigate(new Uri("/Views/QuestionPage.xaml", UriKind.Relative));
                }
            });
        }
        #endregion
        #region Navigation

        /// <summary>
        ///   Call When the page Become the First
        /// </summary>
        /// <param name="viewModel">View model Page.</param>
        public override void OnNavigatedTo(IViewModel viewModel)
        {
            base.OnNavigatedTo(viewModel);

            LoadData();
        }

        /// <summary>
        ///     Call when the page in not used
        /// </summary>
        /// <param name="viewModel">page viewModel</param>
        public override void OnNavigatedFrom(IViewModel viewModel)
        {
            base.OnNavigatedFrom(viewModel);
            //if view model of the following page is category 
            if (viewModel is IViewModelAnswers)
            {
                //give slected item to the view model
                ((IViewModelAnswers)viewModel).SelectedItem = this.SelectedItem;
                //Load data
                ((IViewModelAnswers)viewModel).LoadData();
                
                SelectedItem = null;
            }
        }

        #endregion
    }
}
