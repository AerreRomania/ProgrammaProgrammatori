using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date; 
            EndDate = DateTime.Now;
        }
        private void GetClients()
        {
           
            Task.Run(async () => 
            {
                ClientsList = await _reportsService.GetClientisAsync();
                
            });
        }
        private void GetArticles()
        {
           
            Task.Run(async () =>
            {
               
                ArticleList = await _articleService.GetAll();
               
                ArticleList = new ObservableCollection<Articole>(_articleList.OrderBy(a => a.Articol));
            });
        }
        private void SelectCurrentYear()
        {
            if (IsChecked)
            {
                StartDate = new DateTime(DateTime.Now.Year, 1, 1).Date;
                EndDate = DateTime.Now;
               
            }
            else
            {
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date; ;
                EndDate = DateTime.Now;
            }
        }
        private async void GetReport()
        {
            AnalysisList = new ObservableCollection<AnalysisArticle>();
            AnalysisArticle total = new AnalysisArticle();
            if (SelectedIndex == -1) return;
            if (SelectedClient != null)
            {
                var analiza = await _reportsService.GetAnalysisArticleAsync(SelectedArticle.Id, StartDate, EndDate, SelectedClient.Id);

                total.JobTypeName = "Total";
                foreach (var a in analiza)
                {
                    var curr = a;
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
            else
            {
                MessageBox.Show("Please select a client.");
               
            }
            
        }
        private bool _ischecked;
        public bool IsChecked
        {
            get => _ischecked;
            set
            {
                _ischecked = value;
                OnPropertyChanged(nameof(IsChecked));
                SelectCurrentYear();
            }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
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
        private IEnumerable<Clienti> _clientsList;
        public IEnumerable<Clienti> ClientsList
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
                GetReport();
            }
        }
        private IEnumerable<Articole> _articleList;
        public IEnumerable<Articole> ArticleList
        {
            get => _articleList;
            set
            {
                _articleList = value;
                OnPropertyChanged(nameof(ArticleList));
            }
        }

        private DateTime _startdate ;
        public DateTime StartDate
        {
            get => _startdate;
            set
            {
                _startdate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

            private DateTime _enddate;
        public DateTime EndDate
        {
            get => _enddate;
            set
            {
                _enddate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        
    }
}
