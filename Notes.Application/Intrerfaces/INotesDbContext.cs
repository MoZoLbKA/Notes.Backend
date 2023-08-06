using Microsoft.EntityFrameworkCore;
using Notes.Domain;


namespace Notes.Application.Intrerfaces
{
    public interface INotesDbContext
    {
        DbSet<Note> Notes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
