using PP.Chronometer.WPF.ViewModels;
using System;

namespace PP.Chronometer.WPF.State.Navigator
{
    public enum ViewType
    {
        Login,
        Chronometer,
        Assistance,
        Assigned
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }

        event Action StateChanged;
    }
}