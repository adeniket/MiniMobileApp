using Microsoft.Maui.Controls;
using Login_Registration.ViewModels;

namespace Login_Registration
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = new RegistrationViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is RegistrationViewModel vm)
                vm.ClearMessages();
        }
    }
}