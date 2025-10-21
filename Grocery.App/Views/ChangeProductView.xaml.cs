using Grocery.App.ViewModels;
using Grocery.Core.Models;

namespace Grocery.App.Views;

public partial class ChangeProductView : ContentPage
{
    private readonly ChangeProductViewModel _viewModel;

    public ChangeProductView(ChangeProductViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _viewModel = viewModel;
	}

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            Product product = picker.SelectedItem as Product;
            _viewModel.NewSelectedProduct(product);
        }
    }
}