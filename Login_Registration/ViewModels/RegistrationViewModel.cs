using Login_Registration.Models;
using Login_Registration.Services;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace Login_Registration.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string _fullName;
        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _validationMessage;
        private readonly UserDataService _service;
        public string FullName { get => _fullName; set { _fullName = value; OnPropertyChanged(); } }
        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string ConfirmPassword { get => _confirmPassword; set { _confirmPassword = value; OnPropertyChanged(); } }
        public string ValidationMessage { get => _validationMessage; set { _validationMessage = value; OnPropertyChanged(); } }
        public ICommand SubmitCommand { get; }
        public ICommand BackCommand { get; }

        public RegistrationViewModel()
        {
            var dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "users.db3");
            _service = new UserDataService(dbPath);
            SubmitCommand = new Command(async () => await OnSubmit());
            BackCommand = new Command(async () => await OnBack());
        }

        private async Task OnSubmit()
        {
            if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ValidationMessage = "All fields are required.";
                return;
            }
            if (Password.Length < 6 || Password.Length > 8)
            {
                ValidationMessage = "Password must be 6-8 characters.";
                return;
            }
            if (Password != ConfirmPassword)
            {
                ValidationMessage = "Passwords do not match.";
                return;
            }
            var existing = await _service.GetUserByUsernameAsync(Username);
            if (existing != null)
            {
                ValidationMessage = "User already exists.";
                return;
            }
            var user = new Models.UserModel
            {
                FullName = FullName,
                Username = Username,
                Password = Password,
                IsLocked = false
            };
            await _service.AddUserAsync(user);
            ValidationMessage = "Registration successful.";
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async Task OnBack()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        public void ClearMessages()
        {
            ValidationMessage = string.Empty;
        }
    }
}