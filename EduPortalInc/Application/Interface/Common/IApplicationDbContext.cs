using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Common
{
    public interface IApplicationDbContext
    {
        DbSet<UserProfile> UserProfiles { get; set; }
        DbSet<Courses> Courses { get; set; }
        DbSet<Booking> Bookings { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}