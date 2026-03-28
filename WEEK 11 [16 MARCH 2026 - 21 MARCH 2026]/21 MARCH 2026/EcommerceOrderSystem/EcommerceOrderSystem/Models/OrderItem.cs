using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EcommerceOrderSystem.Models;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; }
}