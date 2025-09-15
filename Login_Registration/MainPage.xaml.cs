using Login_Registration.ViewModels;
using Microsoft.Maui.Controls;

namespace Login_Registration
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.DashboardViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is DashboardViewModel vm)
                vm.Refresh();
        }
    }
}
