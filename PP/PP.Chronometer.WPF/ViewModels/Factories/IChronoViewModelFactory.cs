
using PP.Chronometer.WPF.State.Navigator;

namespace PP.Chronometer.WPF.ViewModels.Factories
{
    public interface IChronoViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
