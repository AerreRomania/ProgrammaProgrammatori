using PP.WPF.ViewModels;
using System;

namespace PP.WPF.State.Navigator
{
    public enum ViewType
    {
        Login,
        Home,
        Tracking,
        ArticleReport,
        OperatorReport,
        GeneralReport
    }
   
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }

        event Action StateChanged;
    }
}