using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_QCM.Models
{
   public class DataStore
    {
        #region Fields

        /// <summary>
        ///     Collection of answers.
        /// </summary>
        private ObservableCollection<Answer> _Answers;

        /// <summary>
        ///     Collection of mcqs.
        /// </summary>
        private ObservableCollection<Mcq> _Mcqs;

        /// <summary>
        ///     Collection of questions.
        /// </summary>
        private ObservableCollection<Question> _Questions;

        /// <summary>
        ///     Collection of categories.
        /// </summary>
        private ObservableCollection<Category> _Categories;

        /// <summary>
        ///     Collection of Mcqs by Selected Category.
        /// </summary>
        private ObservableCollection<Mcq> _McqsCategory;

        /// <summary>
        ///    Unique instance of the class <see cref="DataStore"/>.
        /// </summary>
        private static DataStore _Instance;

        /// <summary>
        ///    Mcq selected on list 
        /// </summary>
        private Mcq _mcqSelected;

        /// <summary>
        ///    Int position Mcq
        /// </summary>
        private int _postionMcq;

        /// <summary>
        ///    total number of question
        /// </summary>
        private int _countQuestionMcq;

        #endregion

        #region Properties

        /// <summary>
        ///     Get collection of Answer.
        /// </summary>
        public ObservableCollection<Answer> Answers => _Answers;

        /// <summary>
        ///     Get collection of Mcqs.
        /// </summary>
        public ObservableCollection<Mcq> Mcqs => _Mcqs;

        /// <summary>
        ///     Get collection of Questions.
        /// </summary>
        public ObservableCollection<Question> Questions => _Questions;

        /// <summary>
        ///     Get collection of Category
        /// </summary>
        public ObservableCollection<Category> Categories => _Categories;

        /// <summary>
        ///     Get collection of Qcm in SelectedCateg
        /// </summary>
        public ObservableCollection<Mcq> Mcqs_Category => _McqsCategory;

        /// <summary>
        ///     Get instance of DataStore
        /// </summary>
        public static DataStore Instance => _Instance;
        /// <summary>
        ///  Get Mcq selected 
        /// </summary>
        public Mcq McqSelected => _mcqSelected;

        /// <summary>
        /// Get position in the Qcm
        /// </summary>
        public int PositionSelected => _postionMcq;

        /// <summary>
        /// Get the total number of Question in Mcq 
        /// </summary>
        public int CountQuestionMcq => _countQuestionMcq;
        #endregion

        #region Constructors

        /// <summary>
        ///     Static constructor <see cref="DataStore"/>.
        /// </summary>
        static DataStore()
        {
            _Instance = new DataStore();
        }

        /// <summary>
        ///     Initialize new Instance of <see cref="DataStore"/>.
        /// </summary>
        private DataStore()
        {
            _Answers = new ObservableCollection<Answer>();
            _Mcqs = new ObservableCollection<Mcq>();
            _Questions = new ObservableCollection<Question>();
            _Categories = new ObservableCollection<Category>();
            _McqsCategory = new ObservableCollection<Mcq>();
            _mcqSelected = new Mcq();
            _postionMcq = 0;
            _countQuestionMcq = 0;
        }

        #endregion

        #region Methods


        #endregion
    }
}
