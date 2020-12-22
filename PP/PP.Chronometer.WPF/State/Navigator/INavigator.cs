using System;
using PP.Chronometer.WPF.ViewModels;

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
