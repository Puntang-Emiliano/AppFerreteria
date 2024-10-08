
using AppFerreteria;
using AppFerreteria.mvvm.Views;
using AppFerreteria.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace AppFerreteria.mvvm.ViewModels;

public partial class ProductoListaViewModel : BaseViewModel
{

    [ObservableProperty] private ObservableCollection<Producto> _productos;
    [ObservableProperty] private Producto _productoSeleccionado;
    [ObservableProperty] private bool isRefreshing;

    public ProductoListaViewModel()
    {
        Title = "Lista de Productos";

        Task.Run(async () => { await ObtenerTodos(); }).Wait();
    }

    [RelayCommand]
    private async Task ObtenerTodos()
    {
        IsBusy = IsRefreshing = true;

        var productos = await ApiService.ObtenerTodos();

        if (productos != null)
        {
            Productos = new ObservableCollection<Producto>(productos);
        }

        IsBusy = IsRefreshing = false;
    }

    [RelayCommand]
    private async Task GoToDetalle()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new ProductoDetallePage());
    }

}