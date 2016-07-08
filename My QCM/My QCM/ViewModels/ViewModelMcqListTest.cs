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
using MVVM.ViewModels;
using MVVM.Service;

namespace My_QCM.ViewModels
{
    public class ViewModelMcqListTest : ViewModelList<Mcq>, IViewModelMcqs
    {
        #region Fields
        /// <summary>
        ///     View model to show question associated to the Mcq.
        /// </summary>
        private ViewModelQuestionTest _ViewModelQuestionTest;
        #endregion

        #region Properties
        /// <summary>
        ///     Get View Model Question, to show Question with Answer list
        /// </summary>
        public IViewModelAnswers ViewModelQuestions => _ViewModelQuestionTest;
        #endregion
        #region Contructors
        public ViewModelMcqListTest()
        {
            _ViewModelQuestionTest = new ViewModelQuestionTest();
        }
        #endregion

        #region Methods
        public override void LoadData()
        {
            // Foreach Mcq in Category
            this.ItemsSource.Clear();
            foreach (Mcq mcq in DataStore.Instance.Mcqs_Category)
            {
                this.ItemsSource.Add(mcq);
            }
            System.Diagnostics.Debug.WriteLine(" Il y a "+this.ItemsSource.Count);
        }

        protected override void InitializePropertyTrackers()
        {
            base.InitializePropertyTrackers();

            this.AddPropertyTrackerAction(nameof(SelectedItem), (sender, args) =>
            {
                if (SelectedItem != null)
                {
                    int id = this.SelectedItem.IdServer;
                    foreach (Mcq mcq in DataStore.Instance.Mcqs)
                    {
                        // Set the Mcq value
                        if (mcq.IdServer == id)
                        {
                            DataStore.Instance.McqSelected.IdServer = mcq.IdServer;
                            DataStore.Instance.McqSelected.Category = mcq.Category;
                            DataStore.Instance.McqSelected.Questions = mcq.Questions;
                            DataStore.Instance.McqSelected.Countdown = mcq.Countdown;
                            DataStore.Instance.McqSelected.CreatedAt = mcq.CreatedAt;
                            DataStore.Instance.McqSelected.DiffDeb = mcq.DiffDeb;
                            DataStore.Instance.McqSelected.DiffEnd = mcq.DiffEnd;
                            DataStore.Instance.McqSelected.IsActif = mcq.IsActif;
                            DataStore.Instance.McqSelected.Name = mcq.Name;
                            DataStore.Instance.McqSelected.UpdatedAt = mcq.UpdatedAt;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Absente");
                        }
                    }
                    //Run the Next Page
                    ServiceResolver.GetService<INavigationService>().Navigate(new Uri("/Views/QuestionPage.xaml", UriKind.Relative));
                }
            });
        }
        #endregion
        #region Navigation

        /// <summary>
        ///     Call when the page become the Active Page
        /// </summary>
        /// <param name="viewModel">ViewModel of the Page</param>
        public override void OnNavigatedTo(IViewModel viewModel)
        {
            base.OnNavigatedTo(viewModel);

            //Upload data on the page
            LoadData();
        }

        /// <summary>
        ///     Call when the page is not active on the Model
        /// </summary>
        /// <param name="viewModel">Page ViewModel</param>
        public override void OnNavigatedFrom(IViewModel viewModel)
        {
            base.OnNavigatedFrom(viewModel);

            //If ViewModel is IViewModelMcq
            if (viewModel is IViewModelMcq)
            {
                //Set the Event to the View_Model
                ((IViewModelMcq)viewModel).Item = this.SelectedItem;
                //Upload Data
                ((IViewModelMcq)viewModel).LoadData();
                //Return to null
                SelectedItem = null;
            }
        }

        #endregion

    }
}
