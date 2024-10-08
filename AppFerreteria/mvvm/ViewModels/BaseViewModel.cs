using CommunityToolkit.Mvvm.ComponentModel;

namespace AppFerreteria.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty] private bool isBusy;  

    [ObservableProperty]    private string title;
}
