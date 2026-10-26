
using System.ComponentModel.DataAnnotations;

namespace MediCart.DTOs;

public class MedicineForm
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [Required]
    [StringLength(80)]
    public string Manufacturer { get; set; } = "";

    [Range(typeof(decimal), "0.01", "100000")]
    public decimal Price { get; set; }

    [Range(0, 100000)]
    public int StockQuantity { get; set; }

    [DataType(DataType.Date)]
    public DateTime ExpiryDate { get; set; }
        = DateTime.Today.AddYears(1);
}
