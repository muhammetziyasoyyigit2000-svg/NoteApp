using MediatR;
using NoteApp.Application.Common.Interfaces;

namespace NoteApp.Application.Features.Notes.Commands.DeleteNote;

public record DeleteNoteCommand(Guid Id) : IRequest<bool>;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteNoteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _context.Notes.FindAsync(new object[] { request.Id }, cancellationToken);

        if (note == null)
        {
            return false;
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}