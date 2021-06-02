using PP.WPF.State.Navigator;
using System;

namespace PP.WPF.ViewModels.Factories
{
    public class PPViewModelFactory : IPPViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<TrackingViewModel> _createTrackingViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<AnalysisArticleViewModel> _createAnalysisArticleViewModel;

        public PPViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel,
            CreateViewModel<LoginViewModel> createLoginViewModel, CreateViewModel<TrackingViewModel> createTrackingViewModel, CreateViewModel<AnalysisArticleViewModel> createAnalysisViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createTrackingViewModel = createTrackingViewModel;
            _createAnalysisArticleViewModel = createAnalysisViewModel;
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
                case ViewType.AnalysisArticle:
                    return _createAnalysisArticleViewModel();
                default:
                    throw new ArgumentException(@"The ViewType does not have ViewModel", "viewType");
            }
        }
    }
}