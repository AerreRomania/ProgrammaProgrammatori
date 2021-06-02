using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using PP.Domain.Models;
using PP.Domain.Services;

namespace PP.WPF.ViewModels
{
    public class AnalysisArticleViewModel : ViewModelBase
    {
        private readonly IReportsService _reportsService;
        private readonly IArticleService _articleService;
        public AnalysisArticleViewModel(IReportsService reportservice, IArticleService articleService)
        {
            _reportsService = reportservice;
            _articleService = articleService;
           

            GetArticles();
            
        }
        private void GetArticles()
        {
            ArticleList = new ObservableCollection<Articole>();
            Task.Run(async () =>
            {
                var clients = await _articleService.GetAll();
                foreach (var c in clients)
                    ArticleList.Add(c);
            });
        }
        private void GetReport() 
        {
            AnalysisList = new ObservableCollection<AnalysisArticle>();
            Task.Run(async () =>
            {
                var analiza = await _reportsService.GetAnalysisArticleAsync(SelectedArticle.Id, StartDate, EndDate, 6, "21A");
                foreach (var a in analiza)
                    AnalysisList.Add(a);
            });
            OnPropertyChanged(nameof(AnalysisList));
        }

        private string _filtername;
        public string FilterName
        {
            get => _filtername;
            set
            {
                _filtername = value;
                OnPropertyChanged(nameof(FilterName));
            }
        }

        private ObservableCollection<AnalysisArticle> _analysisList;

        public ObservableCollection<AnalysisArticle> AnalysisList
        {
            get => _analysisList;
            set
            {
                _analysisList = value;
                OnPropertyChanged(nameof(AnalysisList));
            }

        }
        private Articole _selectedArticle;
        public Articole SelectedArticle
        {
            get => _selectedArticle;
            set
            {
                _selectedArticle = value;
                OnPropertyChanged(nameof(SelectedArticle));
                GetReport();
            }
        }
        private ObservableCollection<Articole> _articleList;
        public ObservableCollection<Articole> ArticleList
        {
            get => _articleList;
            set
            {
                _articleList = value;
                OnPropertyChanged(nameof(ArticleList));
            }
        }

        private DateTime _startdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month,1);
        public DateTime StartDate
        {
            get => _startdate;
            set
            {
                _startdate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

            private DateTime _enddate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public DateTime EndDate
        {
            get => _enddate;
            set
            {
                _startdate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        
    }
}
