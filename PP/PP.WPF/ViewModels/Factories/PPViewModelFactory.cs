using PP.WPF.State.Navigator;
using System;

namespace PP.WPF.ViewModels.Factories
{
    public class PPViewModelFactory : IPPViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<TrackingViewModel> _createTrackingViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<AnalysisArticleViewModel> _createArticleReportViewModel;
        private readonly CreateViewModel<AnalisiOperatoreViewModel> _createAnaliziOperatoreViewModel;

        public PPViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel,
            CreateViewModel<LoginViewModel> createLoginViewModel, CreateViewModel<TrackingViewModel> createTrackingViewModel, CreateViewModel<AnalysisArticleViewModel> createArticleReportViewModel, CreateViewModel<AnalisiOperatoreViewModel> createOperatorReportViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createTrackingViewModel = createTrackingViewModel;
            _createArticleReportViewModel = createArticleReportViewModel;
            _createAnaliziOperatoreViewModel = createOperatorReportViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _createLoginViewModel();

                case ViewType.Home:
                    return _createHomeViewModel();

                case ViewType.Tracking:
                    return _createTrackingViewModel();
                case ViewType.ArticleReport:
                    return _createArticleReportViewModel();
                case ViewType.OperatorReport:
                    return _createAnaliziOperatoreViewModel();
                default:
                    throw new ArgumentException(@"The ViewType does not have ViewModel", "viewType");
            }
        }
       
    }
}