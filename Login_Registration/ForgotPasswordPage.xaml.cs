using Microsoft.Maui.Controls;
namespace Login_Registration
{
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.ForgotPasswordViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is ViewModels.ForgotPasswordViewModel vm)
                vm.ClearMessages();
        }
    }
}
