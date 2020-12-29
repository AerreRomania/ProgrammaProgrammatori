using PP.WPF.State.Navigator;

namespace PP.WPF.ViewModels.Factories
{
    public interface IPPViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}