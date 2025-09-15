using System.Windows.Input;
using Microsoft.Maui.Controls;
using Login_Registration.Services;
using System.Threading.Tasks;

namespace Login_Registration.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        private string _username;
        private string _newPassword;
        private string _confirmPassword;
        private string _message;
        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }
        public string NewPassword { get => _newPassword; set { _newPassword = value; OnPropertyChanged(); } }
        public string ConfirmPassword { get => _confirmPassword; set { _confirmPassword = value; OnPropertyChanged(); } }
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }
        public ICommand SaveCommand { get; }
        public ICommand BackCommand { get; }
        public ForgotPasswordViewModel()
        {
            SaveCommand = new Command(async () => await OnSave());
            BackCommand = new Command(async () => await OnBack());
        }
        public void ClearMessages()
        {
            Message = string.Empty;
        }
        private async Task OnSave()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                Message = "All fields are required.";
                return;
            }
            if (NewPassword != ConfirmPassword)
            {
                Message = "Passwords do not match.";
                return;
            }
            if (NewPassword.Length < 6 || NewPassword.Length > 8)
            {
                Message = "Password must be 6-8 characters.";
                return;
            }
            var dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "users.db3");
            var service = new UserDataService(dbPath);
            var user = await service.GetUserByUsernameAsync(Username);
            if (user == null)
            {
                Message = "User not found.";
                return;
            }
            if (user.Password == NewPassword)
            {
                Message = "New password cannot be the same as the old password.";
                return;
            }
            user.Password = NewPassword;
            await service.UpdateUserAsync(user);
            Message = string.Empty;
            await Application.Current.MainPage.DisplayAlert("Success", "Password updated successfully.", "OK");
            await Shell.Current.GoToAsync("//LoginPage");
        }
        private async Task OnBack()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
