using Microsoft.EntityFrameworkCore;
using NoteApp.Domain.Entities;
using System.Collections.Generic;

namespace NoteApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Note> Notes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}