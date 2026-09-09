using MediatR;
using Microsoft.EntityFrameworkCore;
using NoteApp.Application.Common.Interfaces;

namespace NoteApp.Application.Features.Notes.Queries.GetNotes;

// Dışarıya döneceğimiz veri modeli (DTO)
public record NoteDto(Guid Id, string Title, string Content, DateTime CreatedAt, bool IsArchived);

// MediatR Query isteği
public record GetNotesQuery : IRequest<List<NoteDto>>;

// Sorguyu işleyen Handler
public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, List<NoteDto>>
{
    private readonly IApplicationDbContext _context;

    public GetNotesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<NoteDto>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Notes
            .AsNoTracking()
            .Select(n => new NoteDto(n.Id, n.Title, n.Content, n.CreatedAt, n.IsArchived))
            .ToListAsync(cancellationToken);
    }
}