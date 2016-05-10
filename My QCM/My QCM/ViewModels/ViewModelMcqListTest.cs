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
                    ServiceResolver.GetService<INavigationService>().Navigate(new Uri("/Views/QuestionPage.xaml", UriKind.Relative));
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
            if (viewModel is IViewModelMcq)
            {
                //On donné l'élément sélectionné au vue-modèle.
                ((IViewModelMcq)viewModel).Item = this.SelectedItem;
                //On charge les données
                ((IViewModelMcq)viewModel).LoadData();
                //On remet la sélection à null.
                SelectedItem = null;
            }
        }

        #endregion

    }
}
