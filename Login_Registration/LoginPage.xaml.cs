using Microsoft.Maui.Controls;
using Login_Registration.ViewModels;

namespace Login_Registration
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is LoginViewModel vm)
                vm.ClearMessages();
        }
    }
}