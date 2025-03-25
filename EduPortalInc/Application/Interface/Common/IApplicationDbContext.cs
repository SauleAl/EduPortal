using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Common
{
    public interface IApplicationDbContext
    {
        DbSet<UserProfile> UserProfiles { get; set; }
        DbSet<Courses> Courses { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
