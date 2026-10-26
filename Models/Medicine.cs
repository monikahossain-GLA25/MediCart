
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MediCart.Models;

public class Medicine
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";

    [Required]
    [MaxLength(80)]
    public string Manufacturer { get; set; } = "";

    [Precision(18, 2)]
    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public DateTime ExpiryDate { get; set; }
}
