using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace Grocery.App.ViewModels
{
    public partial class NewProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        
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

        public NewProductViewModel(IProductService productService)
        {
            _productService = productService;
        }

        [RelayCommand]
        private void CreateProduct()
        {
            try
            {
                NewShelfLife = new DateOnly(newShelfLifeYear, newShelfLifeMonth, newShelfLifeDay);
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


    }
}
