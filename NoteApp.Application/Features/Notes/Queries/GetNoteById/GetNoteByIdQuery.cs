using MediatR;
using Microsoft.EntityFrameworkCore;
using NoteApp.Application.Common.Interfaces;
using NoteApp.Domain.Entities;

namespace NoteApp.Application.Features.Notes.Queries.GetNoteById;

// Tek bir Guid Id alıp geriye Note entity'si (bulunamazsa null) döner
public record GetNoteByIdQuery(Guid Id) : IRequest<Note?>;

public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, Note?>
{
    private readonly IApplicationDbContext _context;

    public GetNoteByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Note?> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await _context.Notes
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);

        return note;
    }
}