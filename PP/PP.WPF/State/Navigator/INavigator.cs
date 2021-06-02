using PP.WPF.ViewModels;
using System;

namespace PP.WPF.State.Navigator
{
    public enum ViewType
    {
        Login,
        Home,
        Tracking,
        AnalysisArticle
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }

        event Action StateChanged;
    }
}