using System.Collections.Generic;

namespace CheckoutSystem
{
    public class Checkout : ICheckout
    {
        private readonly Dictionary<string, int> _prices;
        private readonly Dictionary<string, (int Quantity, int SpecialPrice)> _specialPrices;
        private readonly Dictionary<string, int> _items = new Dictionary<string, int>();

        public Checkout(Dictionary<string, int> prices, Dictionary<string, (int Quantity, int SpecialPrice)> specialPrices)
        {
            _prices = prices;
            _specialPrices = specialPrices;
        }

        public void Scan(string item)
        {
            if (_items.ContainsKey(item))
                _items[item]++;
            else
                _items[item] = 1;
        }

        public int GetTotalPrice()
        {
            int total = 0;

            foreach (var item in _items)
            {
                if (_specialPrices.ContainsKey(item.Key) && item.Value >= _specialPrices[item.Key].Quantity)
                {
                    int specialCount = item.Value / _specialPrices[item.Key].Quantity;
                    int remainder = item.Value % _specialPrices[item.Key].Quantity;
                    total += specialCount * _specialPrices[item.Key].SpecialPrice + remainder * _prices[item.Key];
                }
                else
                {
                    total += item.Value * _prices[item.Key];
                }
            }

            return total;
        }

    }
}
