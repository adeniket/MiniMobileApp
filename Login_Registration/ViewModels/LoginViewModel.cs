using Login_Registration.Models;
using Login_Registration.Services;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace Login_Registration.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private int _failedAttempts;
        private bool _isLocked;
        private string _lockoutMessage;
        private string _countdown;
        private string _loginErrorMessage;
        private readonly UserDataService _service;
        private bool _timerActive;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string LockoutMessage
        {
            get => _lockoutMessage;
            set { _lockoutMessage = value; OnPropertyChanged(); }
        }

        public bool IsLocked
        {
            get => _isLocked;
            set { _isLocked = value; OnPropertyChanged(); }
        }

        public string Countdown
        {
            get => _countdown;
            set { _countdown = value; OnPropertyChanged(); }
        }

        public string LoginErrorMessage
        {
            get => _loginErrorMessage;
            set { _loginErrorMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ForgotPasswordCommand { get; }
        public static string CurrentUserFullName { get; private set; }
        public static int CurrentUserLoginCount { get; private set; }

        public LoginViewModel()
        {
            var dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "users.db3");
            _service = new UserDataService(dbPath);
            LoginCommand = new Command(async () => await OnLogin());
            RegisterCommand = new Command(async () => await OnRegister());
            ForgotPasswordCommand = new Command(async () => await OnForgotPassword());
        }

        private async Task OnLogin()
        {
            LoginErrorMessage = string.Empty;
            var user = await _service.GetUserByUsernameAsync(Username);
            if (user == null)
            {
                LoginErrorMessage = "User does not exist.";
                return;
            }
            if (user.IsLocked && user.LockoutEnd > DateTime.Now)
            {
                IsLocked = true;
                LockoutMessage = "Account locked.";
                StartCountdown(user.LockoutEnd.Value);
                return;
            }
            if (user.Password == Password)
            {
                _failedAttempts = 0;
                user.IsLocked = false;
                user.LockoutEnd = null;
                user.LoginCount++;
                await _service.UpdateUserAsync(user);
                CurrentUserFullName = user.FullName;
                CurrentUserLoginCount = user.LoginCount;
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                _failedAttempts++;
                int remaining = 3 - _failedAttempts;
                if (_failedAttempts < 3)
                {
                    LoginErrorMessage = $"Incorrect password. You have {remaining} trial(s) left.";
                }
                else
                {
                    LoginErrorMessage = "Incorrect password.";
                }
                if (_failedAttempts >= 3)
                {
                    user.IsLocked = true;
                    user.LockoutEnd = DateTime.Now.AddSeconds(30);
                    await _service.UpdateUserAsync(user);
                    IsLocked = true;
                    LockoutMessage = "Account locked for 30 seconds.";
                    StartCountdown(user.LockoutEnd.Value);
                }
            }
        }

        private void StartCountdown(DateTime lockoutEnd)
        {
            _timerActive = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                if (!_timerActive) return false;
                var secondsLeft = (int)(lockoutEnd - DateTime.Now).TotalSeconds;
                Countdown = $"Try again in {secondsLeft} seconds.";
                if (secondsLeft <= 0)
                {
                    IsLocked = false;
                    LockoutMessage = string.Empty;
                    Countdown = string.Empty;
                    _timerActive = false;
                    return false;
                }
                return true;
            });
        }

        private async Task OnRegister()
        {
            await Shell.Current.GoToAsync("//RegistrationPage");
        }

        private async Task OnForgotPassword()
        {
            await Shell.Current.GoToAsync("//ForgotPasswordPage");
        }

        public void ClearMessages()
        {
            LoginErrorMessage = string.Empty;
            LockoutMessage = string.Empty;
            Countdown = string.Empty;
            _timerActive = false;
            IsLocked = false;
        }
    }
}