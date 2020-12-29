using PP.Chronometer.WPF.State.Navigator;
using System;

namespace PP.Chronometer.WPF.ViewModels.Factories
{
    public class ChronoViewModelFactory : IChronoViewModelFactory
    {
        private readonly CreateViewModel<ChronometerViewModel> _createChronometerViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<AssistanceTasksViewModel> _createAssistanceTasksViewModel;
        private readonly CreateViewModel<AssignedTasksViewModel> _createAssignedTasksViewModel;

        public ChronoViewModelFactory(
            CreateViewModel<ChronometerViewModel> createChronometerViewModel,
            CreateViewModel<LoginViewModel> createLoginViewModel,
            CreateViewModel<AssistanceTasksViewModel> createAssistanceTasksViewModel,
            CreateViewModel<AssignedTasksViewModel> createAssignedTasksViewModel)
        {
            _createChronometerViewModel = createChronometerViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createAssistanceTasksViewModel = createAssistanceTasksViewModel;
            _createAssignedTasksViewModel = createAssignedTasksViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _createLoginViewModel();

                case ViewType.Chronometer:
                    return _createChronometerViewModel();

                case ViewType.Assigned:
                    return _createAssignedTasksViewModel();

                case ViewType.Assistance:
                    return _createAssistanceTasksViewModel();

                default:
                    throw new ArgumentException(@"The ViewType does not have ViewModel", "viewType");
            }
        }
    }
}