using MediatR;
using NoteApp.Application.Common.Interfaces;
using NoteApp.Domain.Entities;

namespace NoteApp.Application.Features.Notes.Commands.CreateNote;

// Dışarıdan gelecek veri modeli
public record CreateNoteCommand(string Title, string Content) : IRequest<Guid>;

// İsteği işleyecek mantık (Handler)
public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateNoteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note(request.Title, request.Content);

        _context.Notes.Add(note);
        await _context.SaveChangesAsync(cancellationToken);

        return note.Id;
    }
}