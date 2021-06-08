using PP.Domain.Models;
using PP.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PP.WPF.ViewModels
{
    public class AnalisiOperatoreViewModel : ViewModelBase
    {
        private readonly IReportsService _reportsService;
        public AnalisiOperatoreViewModel(IReportsService reportservice)
        {
            _reportsService = reportservice;

            AnnoList = new List<int>() { 2021, 2020 };
            GetAngajati();
            GetClients();
            GetStagiuni();
        }
        private void GetStagiuni()
        {
            StagioneList = new ObservableCollection<Stagiuni>();
            Task.Run(async () =>
            {
                var stg = await _reportsService.GetStagiuniAsync();
                foreach (var s in stg)
                    StagioneList.Add(s);
            });
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
        private void GetAngajati()
        {
            AngajatiList = new ObservableCollection<Angajati>();
            Task.Run(async () =>
            {
                var clients = await _reportsService.GetAngajatiAsync();
                foreach (var c in clients)
                    AngajatiList.Add(c);
            });
        }

        public void GetReport()
        {
            TemporaryList = new ObservableCollection<AnalisiOperatore>();
            AnalisiList = new ObservableCollection<AnalisiOperatoriColumns>();
            Task.Run(async () =>
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var analisi = await _reportsService.GetAnalisiOperatore(SelectedAngajat.Id, SelectedAnno, i,SelectedClient.Id);
                        foreach (var a in analisi)
                        {
                            a.Luna = i;
                            TemporaryList.Add(a);
                        }
                       
                    }
                    for (int i = 0; i <= JobNames.Count; i++)
                    {
                        AnalisiOperatoriColumns item = new AnalisiOperatoriColumns();
                        item.JobTypeName = JobNames[i];
                        AnalisiList.Add(item);
                        var curr = JobNames[i];
                        foreach (var filter in TemporaryList.Where(a => a.JobTypeName == curr))
                        {
                           for(int x=0;x<AnalisiList.Count;x++)
                            {

                                if (AnalisiList[x].JobTypeName == filter.JobTypeName)
                                {
                                    if (filter.Luna == 1) AnalisiList[x].Gennaio += 1;
                                    else if (filter.Luna == 2) AnalisiList[x].Febbraio += 1;
                                    else if (filter.Luna == 3) AnalisiList[x].Marzo += 1;
                                    else if (filter.Luna == 4) AnalisiList[x].Aprile += 1;
                                    else if (filter.Luna == 5) AnalisiList[x].Maggio += 1;
                                    else if (filter.Luna == 6) AnalisiList[x].Giugno += 1; //= filter.Count;
                                    else if (filter.Luna == 7) AnalisiList[x].Luglio += 1;//= filter.Count;
                                    else if (filter.Luna == 8) AnalisiList[x].Agosto += 1;//= filter.Count;
                                    else if (filter.Luna == 9) AnalisiList[x].Settembre += 1;//= filter.Count;
                                    else if (filter.Luna == 10) AnalisiList[x].Ottombre += 1;// = filter.Count;
                                    else if (filter.Luna == 11) AnalisiList[x].Novembre += 1;//= filter.Count;
                                    else if (filter.Luna == 12) AnalisiList[x].Dicembre += 1;// = filter.Count;
                                    AnalisiList[x].Total = AnalisiList[x].Gennaio+ AnalisiList[x].Febbraio+ AnalisiList[x].Marzo+ AnalisiList[x].Maggio+ AnalisiList[x].Aprile+ AnalisiList[x].Giugno+ AnalisiList[x].Luglio+ AnalisiList[x].Agosto+ AnalisiList[x].Settembre+ AnalisiList[x].Ottombre+ AnalisiList[x].Novembre+ AnalisiList[x].Dicembre;
                                }
                                else continue;
                            }
                        }
                    }
                    

                });

        }
        public List<string> JobNames = new List<string>()
        {
            "Prototipo",
            "Campionario",
            "Preproduzione",
            "Svillupo Taglie",
            "Schede Tehnice",
            "Riparazione/Ass.Rep.", 
            "Prove tecniche",
            "Contracampione",
            "Vacanza",
        };
        private ObservableCollection<AnalisiOperatore> _temoporarylist;
        public ObservableCollection<AnalisiOperatore> TemporaryList
        {
            get => _temoporarylist;
            set
            {
                _temoporarylist = value;
                OnPropertyChanged(nameof(TemporaryList));
            }
        }
        private ObservableCollection<AnalisiOperatoriColumns> _analisiList;
        public ObservableCollection<AnalisiOperatoriColumns> AnalisiList
        {
            get => _analisiList;
            set
            {
                _analisiList = value;
                OnPropertyChanged(nameof(AnalisiList));
            }
        }
        private Stagiuni _selectedStg;
        private Stagiuni SelectedStagiune
        {
            get => _selectedStg;
            set
            {
                _selectedStg = value;
                OnPropertyChanged(nameof(SelectedStagiune));
            }
        }
        private ObservableCollection<Stagiuni> _stagiuni;
        public ObservableCollection<Stagiuni> StagioneList
        {
            get => _stagiuni;
            set
            {
                _stagiuni = value;
                OnPropertyChanged(nameof(StagioneList));
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

        private int _selectedAnno;
        public int SelectedAnno
        {
            get => _selectedAnno;
            set
            {
                _selectedAnno = value;
                OnPropertyChanged(nameof(SelectedAnno));
            }
        }
        private List<int> _annoList;
        public List<int> AnnoList
        {
            get => _annoList;
            set
            {
                _annoList = value;
                OnPropertyChanged(nameof(AnnoList));
            }
        }

        private ObservableCollection<Angajati> _analysisList;

        public ObservableCollection<Angajati> AngajatiList
        {
            get => _analysisList;
            set
            {
                _analysisList = value;
                OnPropertyChanged(nameof(AngajatiList));
            }

        }
        private Angajati _selectedArticle;
        public Angajati SelectedAngajat
        {
            get => _selectedArticle;
            set
            {
                _selectedArticle = value;
                OnPropertyChanged(nameof(SelectedAngajat));
                Task.Delay(5000);
                GetReport();
            }
        }
       

        
    }
}
