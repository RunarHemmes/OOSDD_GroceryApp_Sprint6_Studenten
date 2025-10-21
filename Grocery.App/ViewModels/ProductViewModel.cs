using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class ProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        public ObservableCollection<Product> Products { get; set; }

        Client Client;

        public ProductViewModel(IProductService productService, GlobalViewModel globalViewModel)
        {
            _productService = productService;
            Products = [];
            foreach (Product p in _productService.GetAll()) Products.Add(p);
            Client = globalViewModel.Client;
        }

        [RelayCommand]
        private async Task GoToNewProduct()
        {
            if (Client.Role == Role.Admin) await Shell.Current.GoToAsync(nameof(NewProductView));
        }

        [RelayCommand]
        private async Task GoToChangeProduct()
        {
            if (Client.Role == Role.Admin) await Shell.Current.GoToAsync(nameof(ChangeProductView));
        }
    }
}
