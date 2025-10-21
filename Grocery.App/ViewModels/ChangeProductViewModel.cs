using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Services;
using Grocery.App.Views;
using Grocery.Core.Helpers;
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

        partial void OnSelectedProductChanged(Product? oldValue, Product product)
        {
            SelectedProduct = product;
            NewName = product.Name;
            NewStock = product.Stock;
            NewShelfLife = product.ShelfLife;
            NewShelfLifeYear = product.ShelfLife.Year;
            NewShelfLifeMonth = product.ShelfLife.Month;
            NewShelfLifeDay = product.ShelfLife.Day;
            NewPrice = product.Price.ToString();
            Message = "";
        }

        [RelayCommand]
        private void CreateProduct()
        {
            try
            {
                NewShelfLife = new DateOnly(NewShelfLifeYear, NewShelfLifeMonth, NewShelfLifeDay);
                decimal price = decimal.Parse(NewPrice, CultureInfo.InvariantCulture);
                price = price / 100;
                int id = SelectedProduct.Id;

                if (ProductHelper.CheckProductInfo(NewName, NewStock, NewShelfLife, price))
                {
                    Product newProduct = new Product(id, NewName, NewStock, NewShelfLife, price);
                    _productService.Update(newProduct);
                    GoToProducts();
                    Message = "";
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
