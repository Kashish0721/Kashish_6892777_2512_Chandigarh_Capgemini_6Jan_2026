using System.ComponentModel.DataAnnotations;
namespace EcommerceOrderSystem.Models;

public class Customer
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public List<Order>? Orders { get; set; }
}