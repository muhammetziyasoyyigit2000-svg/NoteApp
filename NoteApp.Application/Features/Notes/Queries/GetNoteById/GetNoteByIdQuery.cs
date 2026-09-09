using MediatR;
using Microsoft.EntityFrameworkCore;
using NoteApp.Application.Common.Interfaces;
using NoteApp.Application.Features.Notes.Queries.GetNotes;

namespace NoteApp.Application.Features.Notes.Queries.GetNoteById;

// Tek bir Guid Id alıp geriye NoteDto (veya bulunamazsa null) dönecek Query
public record GetNoteByIdQuery(Guid Id) : IRequest<NoteDto?>;

public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteDto?>
{
    private readonly IApplicationDbContext _context;

    public GetNoteByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<NoteDto?> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await _context.Notes
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);

        if (note == null)
        {
            return null;
        }

        return new NoteDto(note.Id, note.Title, note.Content, note.CreatedAt, note.IsArchived);
    }
}