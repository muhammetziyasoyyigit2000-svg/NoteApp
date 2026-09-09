using MediatR;
using NoteApp.Application.Common.Interfaces;

namespace NoteApp.Application.Features.Notes.Commands.UpdateNote;

public record UpdateNoteCommand(Guid Id, string Title, string Content) : IRequest<bool>;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateNoteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _context.Notes.FindAsync(new object[] { request.Id }, cancellationToken);

        if (note == null)
        {
            return false;
        }

        // Domain metodunu çağırıyoruz
        note.Update(request.Title, request.Content);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}