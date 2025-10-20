using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Services;
using Grocery.App.Views;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Grocery.App.ViewModels
{
    public partial class ChangeProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;

        [ObservableProperty]
        Product selectedProduct;

        [ObservableProperty]
        private string? newName;

        [ObservableProperty]
        private int newStock;

        private DateOnly NewShelfLife { get; set; }

        [ObservableProperty]
        private int newShelfLifeYear;

        [ObservableProperty]
        private int newShelfLifeMonth;

        [ObservableProperty]
        private int newShelfLifeDay;

        [ObservableProperty]
        private string? newPrice;

        [ObservableProperty]
        private string? message;

        //public ObservableCollection<BoughtProducts> BoughtProductsList { get; set; } = [];

        public ObservableCollection<Product> Products { get; set; }

        public ChangeProductViewModel(IProductService productService) 
        {
            _productService = productService;
            Products = new(productService.GetAll());
        }

        partial void OnSelectedProductChanged(Product? oldValue, Product newValue)
        {
            //BoughtProductsList.Clear();
            //List<BoughtProducts> list = _boughtProductsService.Get(newValue.Id);
            //foreach (var item in list)
            //{
            //    BoughtProductsList.Add(item);
            //}
        }

        [RelayCommand]
        private void CreateProduct()
        {
            try
            {
                NewShelfLife = new DateOnly(NewShelfLifeYear, NewShelfLifeMonth, NewShelfLifeDay);
                decimal price = decimal.Parse(NewPrice, CultureInfo.InvariantCulture);
                price = price / 100;

                if (_productService.CheckProductInfo(NewName, NewStock, NewShelfLife, price))
                {
                    Product newProduct = new Product(0, NewName, NewStock, NewShelfLife, price);
                    _productService.Add(newProduct);
                    GoToProducts();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Message = "De ingevulde gegevens zijn niet geldig, probeer het aub opnieuw.";
            }
        }

        private async Task GoToProducts()
        {
            await Shell.Current.GoToAsync(nameof(ProductView));
        }

        [RelayCommand]
        public void NewSelectedProduct(Product product)
        {
            SelectedProduct = product;
        }
    }
}
