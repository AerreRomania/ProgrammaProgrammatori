using System;
using PP.WPF.ViewModels;

namespace PP.WPF.State.Navigator
{
    public enum ViewType
    {
        Login,
        Home,
        Tracking
   
    }
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
