using MediatR;
using Microsoft.EntityFrameworkCore;
using NoteApp.Application.Common.Interfaces;
using NoteApp.Domain.Entities;

namespace NoteApp.Application.Features.GetNotes.Queries;

public record GetNotesQuery : IRequest<List<Note>>;

public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, List<Note>>
{
    private readonly IApplicationDbContext _context;

    public GetNotesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Note>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Notes
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
