namespace ShoppingCartDiscount
{
    public class DiscountService
    {
        public decimal ApplyDiscount(decimal total)
        {
            if (total >= 100) return total * 0.90m;
            if (total >= 50) return total * 0.95m;
            return total;
        }
    }
}
