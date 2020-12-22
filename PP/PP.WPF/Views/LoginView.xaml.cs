using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PP.WPF.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public static readonly DependencyProperty LoginCommandProperty =
            DependencyProperty.Register("LoginCommand", typeof(ICommand), typeof(LoginView), new PropertyMetadata(null));

        public ICommand LoginCommand
        {
            get { return (ICommand)GetValue(LoginCommandProperty); }
            set { SetValue(LoginCommandProperty, value); }
        }

        public LoginView()
        {
            InitializeComponent();
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            if (LoginCommand != null)
            {
                string password = pbPassword.Password;
                LoginCommand.Execute(password);
            }
        }
    }
}
