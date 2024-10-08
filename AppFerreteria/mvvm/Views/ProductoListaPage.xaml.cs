using AppFerreteria.mvvm.ViewModels;

namespace AppFerreteria.mvvm.Views;

public partial class ProductoListaPage : ContentPage
{
	public ProductoListaPage(ProductoListaViewModel _viewModel)
	{
		BindingContext = _viewModel;
		InitializeComponent();
	}
}