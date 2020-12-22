using System;
using PP.WPF.State.Navigator;

namespace PP.WPF.ViewModels.Factories
{
    public class PPViewModelFactory : IPPViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<TrackingViewModel> _createTrackingViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;


        public PPViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel,
            CreateViewModel<LoginViewModel> createLoginViewModel, CreateViewModel<TrackingViewModel> createTrackingViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createTrackingViewModel = createTrackingViewModel;
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

                default:
                    throw new ArgumentException(@"The ViewType does not have ViewModel", "viewType");
            }
        }
    }
}
