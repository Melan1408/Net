using ContactsBook.UI.ViewModels;

namespace ContactsBook.UI.Views;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _viewModel;
    public MainPage()
	{
        var viewModel = App.Services.GetService<MainPageViewModel>();
        BindingContext = viewModel;
        _viewModel = viewModel;
        InitializeComponent();
	}
}