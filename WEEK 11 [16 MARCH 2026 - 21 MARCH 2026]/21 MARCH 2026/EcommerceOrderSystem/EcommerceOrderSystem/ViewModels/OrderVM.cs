using EcommerceOrderSystem.Models;

public class OrderVM
{
    public Order Order { get; set; }
    public List<OrderItem> Items { get; set; }
    public ShippingDetail Shipping { get; set; }
}