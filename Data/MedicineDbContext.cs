
using MediCart.Models;
using Microsoft.EntityFrameworkCore;

namespace MediCart.Data;

public class MedicineDbContext : DbContext
{
    public MedicineDbContext(
        DbContextOptions<MedicineDbContext> options)
        : base(options)
    {
    }

    public DbSet<Medicine> Medicines
        => Set<Medicine>();
}
