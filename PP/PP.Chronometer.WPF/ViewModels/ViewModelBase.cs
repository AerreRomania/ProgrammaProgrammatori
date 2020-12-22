using System.ComponentModel;

namespace PP.Chronometer.WPF.ViewModels
{
    public delegate TViewModel CreateViewModel<out TViewModel>() where TViewModel : ViewModelBase;

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
