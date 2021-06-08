using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using PP.Domain.Models;
using PP.Domain.Services;
using PP.EntityFramework.Services;

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
            GetClients();
            
        }
        private void GetClients()
        {
            ClientsList = new ObservableCollection<Clienti>();
            Task.Run(async () => 
            {
                var clients = await _reportsService.GetClientisAsync();
                foreach (var c in clients)
                    ClientsList.Add(c);
            });
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
        private async void GetReport() 
        {
            AnalysisList = new ObservableCollection<AnalysisArticle>();
            AnalysisArticle total = new AnalysisArticle();
            var analiza = await _reportsService.GetAnalysisArticleAsync(SelectedArticle.Id, StartDate, EndDate, SelectedClient.Id);
            
            foreach(var a in analiza)
            {
                var curr = a;
                total.JobTypeName = "Total:";
                total.ComputerHours += curr.ComputerHours;
                total.ComputerMachineHours += curr.ComputerMachineHours;
                total.MachineHours += curr.MachineHours;
                total.Total += curr.Total;
                
               
            }
            AnalysisList.Add(total);
            foreach (var a in analiza)
            { AnalysisList.Add(a); }
           
           // AnalysisList = tmp;
            OnPropertyChanged(nameof(AnalysisList));
        }

       

        private Clienti _selectedClient;
        public Clienti SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }
        private ObservableCollection<Clienti> _clientsList;
        public ObservableCollection<Clienti> ClientsList
        {
            get => _clientsList;
            set
            {
                _clientsList = value;
                OnPropertyChanged(nameof(ClientsList));
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
                Task.Delay(5000);
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
