using DevExpress.Mvvm;
using PP.Domain.Models;
using PP.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PP.WPF.ViewModels
{
    public class AnalisiOperatoreViewModel : ViewModelBase
    {
        private readonly IReportsService _reportsService;
        public DelegateCommand<object> RefreshCommand { get; set; }
        public AnalisiOperatoreViewModel(IReportsService reportservice)
        {
            _reportsService = reportservice;
            RefreshCommand = new DelegateCommand<object>(OnRefreshCommand);
            AnnoList = new List<int>() { 2020, 2021 };
           
            GetAngajati();
            GetClients();
            GetStagiuni();
            GetJobs();
        }
        private void GetJobs()
        {
            AnalisiList = new ObservableCollection<AnalisiOperatoriColumns>();
            for (int i = 0; i < JobNames.Count; i++)
            {
                AnalisiOperatoriColumns item = new AnalisiOperatoriColumns();
                item.JobTypeName = JobNames[i];
                AnalisiList.Add(item);
            }
            AnalisiPercent = new ObservableCollection<AnalisiOperatoriColumns>();
            for (int i = 0; i < JobNames.Count; i++)
            {
                AnalisiOperatoriColumns item = new AnalisiOperatoriColumns();
                item.JobTypeName = JobNames[i];
                AnalisiPercent.Add(item);
            }
        }
        private void OnRefreshCommand(object obj)
        {
            GetAngajati();
            GetClients();
            GetStagiuni();
            
        }
        private void GetPercent()
        {
            try {
                //AnalisiPercent = new ObservableCollection<AnalisiOperatoriColumns>();

                foreach (AnalisiOperatoriColumns perc in AnalisiList)
                {
                    AnalisiOperatoriColumns item = new AnalisiOperatoriColumns();
                    item.JobTypeName = perc.JobTypeName;

                    if (item.JobTypeName == "Total")
                    {
                        if (Totals.Gennaio != 0 && Totals.Total != 0) item.Gennaio = Math.Round((Totals.Gennaio / Totals.Total) * 100, 1);
                        if (Totals.Febbraio != 0 && Totals.Total != 0) item.Febbraio = Math.Round((Totals.Febbraio / Totals.Total) * 100, 1);
                        if (Totals.Marzo != 0 && Totals.Total != 0) item.Marzo = Math.Round((Totals.Marzo / Totals.Total) * 100, 1);
                        if (Totals.Aprile != 0 && Totals.Total != 0) item.Aprile = Math.Round((Totals.Aprile / Totals.Total) * 100, 1);
                        if (Totals.Maggio != 0 && Totals.Total != 0) item.Maggio = Math.Round((Totals.Maggio / Totals.Total) * 100, 1);
                        if (Totals.Giugno != 0 && Totals.Total != 0) item.Giugno = Math.Round((Totals.Giugno / Totals.Total) * 100, 1);
                        if (Totals.Luglio != 0 && Totals.Total != 0) item.Luglio = Math.Round((Totals.Luglio / Totals.Total) * 100, 1);
                        if (Totals.Agosto != 0 && Totals.Total != 0) item.Agosto = Math.Round((Totals.Agosto / Totals.Total) * 100, 1);
                        if (Totals.Settembre != 0 && Totals.Total != 0) item.Settembre = Math.Round((Totals.Settembre / Totals.Total) * 100, 1);
                        if (Totals.Ottombre != 0 && Totals.Total != 0) item.Ottombre = Math.Round((Totals.Ottombre / Totals.Total) * 100, 1);
                        if (Totals.Novembre != 0 && Totals.Total != 0) item.Novembre = Math.Round((Totals.Novembre / Totals.Total) * 100, 1);
                        if (Totals.Dicembre != 0 && Totals.Total != 0) item.Dicembre = Math.Round((Totals.Dicembre / Totals.Total) * 100, 1);
                        item.Total = 100;
                        AnalisiPercent.Add(item);
                    }
                    else
                    {
                        if (Totals.Gennaio != 0 && perc.Gennaio != 0) item.Gennaio = Math.Round((perc.Gennaio / Totals.Gennaio) * 100, 1);
                        if (Totals.Febbraio != 0 && perc.Febbraio != 0) item.Febbraio = Math.Round((perc.Febbraio / Totals.Febbraio) * 100, 1);
                        if (Totals.Marzo != 0 && perc.Marzo != 0) item.Marzo = Math.Round((perc.Marzo / Totals.Marzo) * 100, 1);
                        if (Totals.Aprile != 0 && perc.Aprile != 0) item.Aprile = Math.Round((perc.Aprile / Totals.Aprile) * 100, 1);
                        if (Totals.Maggio != 0 && perc.Maggio != 0) item.Maggio = Math.Round((perc.Maggio / Totals.Maggio) * 100, 1);
                        if (Totals.Giugno != 0 && perc.Giugno != 0) item.Giugno = Math.Round((perc.Giugno / Totals.Giugno) * 100, 1);
                        if (Totals.Luglio != 0 && perc.Luglio != 0) item.Luglio = Math.Round((perc.Luglio / Totals.Luglio) * 100, 1);
                        if (Totals.Agosto != 0 && perc.Agosto != 0) item.Agosto = Math.Round((perc.Agosto / Totals.Agosto) * 100, 1);
                        if (Totals.Settembre != 0 && perc.Settembre != 0) item.Settembre = Math.Round((perc.Settembre / Totals.Settembre) * 100, 1);
                        if (Totals.Ottombre != 0 && perc.Ottombre != 0) item.Ottombre = Math.Round((perc.Ottombre / Totals.Ottombre) * 100, 1);
                        if (Totals.Novembre != 0 && perc.Novembre != 0) item.Novembre = Math.Round((perc.Novembre / Totals.Novembre) * 100, 1);
                        if (Totals.Dicembre != 0 && perc.Dicembre != 0) item.Dicembre = Math.Round((perc.Dicembre / Totals.Dicembre) * 100, 1);
                        if (Totals.Total != 0 && perc.Total != 0) item.Total = Math.Round((perc.Total / Totals.Total) * 100, 1);
                        AnalisiPercent.Add(item);
                    }
                }
                OnPropertyChanged(nameof(AnalisiPercent)); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void GetStagiuni()
        {
           
            Task.Run(async () =>
            {
                StagioneList = await _reportsService.GetStagiuniAsync();
               
                
            });
        }
        private void GetClients()
        {
           
            Task.Run(async () =>
            {
                ClientsList = await _reportsService.GetClientisAsync();
                
            });
        }
        private void GetAngajati()
        {
            
            Task.Run(async () =>
            {
                AngajatiList = await _reportsService.GetAngajatiAsync();
                
            });
        }

        private AnalisiOperatoriColumns _totals;
        public AnalisiOperatoriColumns Totals
        {
            get => _totals;
            set
            {
                _totals = value;
                OnPropertyChanged(nameof(Totals));
            }
        }
        public void GetReport()
        {
            TemporaryList = new ObservableCollection<AnalisiOperatore>();
            AnalisiList = new ObservableCollection<AnalisiOperatoriColumns>();
            AnalisiPercent = new ObservableCollection<AnalisiOperatoriColumns>();
            if (SelectedIndex == -1) return;
            if (SelectedAnno == null || SelectedClient == null || SelectedStagiune == null)
            {
                MessageBox.Show("Please select all the fields!", "Error !");
            }
            else
            {
                Task.Factory.StartNew(async () =>
                    {
                        for (int i = 1; i <= 12; i++)
                        {

                            var analisi = await _reportsService.GetAnalisiOperatore(SelectedAngajat.Id, SelectedAnno, i, SelectedClient.Id);
                            foreach (var a in analisi)
                            {
                                a.Luna = i;
                                TemporaryList.Add(a);
                            }

                        }
                        for (int i = 0; i < JobNames.Count; i++)
                        {
                            AnalisiOperatoriColumns item = new AnalisiOperatoriColumns();
                            item.JobTypeName = JobNames[i];
                            AnalisiList.Add(item);
                            var curr = JobNames[i];
                            foreach (var filter in TemporaryList.Where(a => a.JobTypeName == curr))
                            {
                                for (int x = 0; x < AnalisiList.Count; x++)
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
                                    AnalisiList[x].Total = AnalisiList[x].Gennaio + AnalisiList[x].Febbraio + AnalisiList[x].Marzo + AnalisiList[x].Maggio + AnalisiList[x].Aprile + AnalisiList[x].Giugno + AnalisiList[x].Luglio + AnalisiList[x].Agosto + AnalisiList[x].Settembre + AnalisiList[x].Ottombre + AnalisiList[x].Novembre + AnalisiList[x].Dicembre;
                                    }
                                    else continue;
                                }
                            }
                        }
                        AnalisiOperatoriColumns total = new AnalisiOperatoriColumns();
                        total.JobTypeName = "Total";
                        foreach (var a in AnalisiList)
                        {
                            total.Gennaio += a.Gennaio;
                            total.Febbraio += a.Febbraio;
                            total.Marzo += a.Marzo;
                            total.Aprile += a.Aprile;
                            total.Maggio += a.Maggio;
                            total.Giugno += a.Giugno;
                            total.Luglio += a.Luglio;
                            total.Agosto += a.Agosto;
                            total.Settembre += a.Settembre;
                            total.Ottombre += a.Ottombre;
                            total.Novembre += a.Novembre;
                            total.Dicembre += a.Dicembre;
                            total.Total += a.Total;
                        }

                        AnalisiList.Add(total);
                        Totals = total;

                        OnPropertyChanged(nameof(Totals));
                        OnPropertyChanged(nameof(AnalisiList));
                    }).ContinueWith(Task =>
                    {
                        GetPercent();
                    }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

               
            }
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

        private ObservableCollection<AnalisiOperatoriColumns> _analisisPercent;
        public ObservableCollection<AnalisiOperatoriColumns> AnalisiPercent
        {
            get => _analisisPercent;
            set
            {
                _analisisPercent = value;
                OnPropertyChanged(nameof(AnalisiPercent));
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
        public Stagiuni SelectedStagiune
        {
            get => _selectedStg;
            set
            {
                _selectedStg = value;
                OnPropertyChanged(nameof(SelectedStagiune));
            }
        }
        private IEnumerable<Stagiuni> _stagiuni;
        public IEnumerable<Stagiuni> StagioneList
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

        private IEnumerable<Angajati> _analysisList;

        public IEnumerable<Angajati> AngajatiList
        {
            get => _analysisList;
            set
            {
                _analysisList = value;
                OnPropertyChanged(nameof(AngajatiList));
            }

        }
        private Angajati _selectedAngajat;
        public Angajati SelectedAngajat
        {
            get => _selectedAngajat;
            set
            {
                _selectedAngajat = value;
                OnPropertyChanged(nameof(SelectedAngajat));
                GetReport();

            }
        }
       

        
    }
}
