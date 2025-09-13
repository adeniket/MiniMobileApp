using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Login_Registration.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private string _welcomeMessage;
        private int _loginCount;
        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set { _welcomeMessage = value; OnPropertyChanged(); }
        }
        public int LoginCount
        {
            get => _loginCount;
            set { _loginCount = value; OnPropertyChanged(); }
        }
        public ICommand LogoutCommand { get; }
        public DashboardViewModel()
        {
            Refresh();
            LogoutCommand = new Command(OnLogout);
        }
        public void Refresh()
        {
            var name = LoginViewModel.CurrentUserFullName;
            WelcomeMessage = string.IsNullOrEmpty(name) ? "Welcome to the dashboard!" : $"Welcome, {name}!";
            LoginCount = LoginViewModel.CurrentUserLoginCount;
        }
        private async void OnLogout()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}