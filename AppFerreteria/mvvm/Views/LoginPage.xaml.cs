using AppFerreteria.mvvm.ViewModels;

namespace AppFerreteria.mvvm.Views;

public partial class LoginPage : ContentPage
{
	private LoginViewModel viewModel;
	public LoginPage()
	{
		BindingContext= viewModel = new LoginViewModel();
		InitializeComponent();
	}
}