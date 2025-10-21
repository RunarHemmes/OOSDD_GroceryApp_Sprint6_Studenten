using System.Security.Cryptography;
using System.Text;

namespace Grocery.Core.Helpers
{
    public static class ProductHelper
    {
        public static bool CheckProductInfo(string name, int stock, DateOnly shelfLife, decimal price)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            if (0 < name.Length && name.Length < 80 && stock >= 0 && shelfLife > today && price >= 0)
            {
                return true;
            }
            return false;
        }
    }
}
