using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Application.Features.Notes.Commands.CreateNote;
using NoteApp.Application.Features.Notes.Commands.DeleteNote;
using NoteApp.Application.Features.Notes.Commands.UpdateNote;
using NoteApp.Application.Features.GetNotes.Queries;
using NoteApp.Application.Features.Notes.Queries.GetNoteById;

namespace NoteApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly ISender _sender;

    public NotesController(ISender sender)
    {
        _sender = sender;
    }

    // 1. Yeni Not Oluşturma (Create)
    [HttpPost]
    public async Task<IActionResult> Create(CreateNoteCommand command)
    {
        var noteId = await _sender.Send(command);
        return Ok(new { Id = noteId, Message = "Not başarıyla oluşturuldu." });
    }

    // 2. Tüm Notları Listeleme (GetAll)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var notes = await _sender.Send(new GetNotesQuery());
        return Ok(notes);
    }

    // 3. ID'ye Göre Tek Not Getirme (GetById)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var note = await _sender.Send(new GetNoteByIdQuery(id));

        if (note == null)
        {
            return NotFound(new { Message = "Not bulunamadı." });
        }

        return Ok(note);
    }

    // 4. Not Güncelleme (Update)
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateNoteCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new { Message = "URL'deki Id ile gövdedeki Id uyuşmuyor." });
        }

        var success = await _sender.Send(command);

        if (!success)
        {
            return NotFound(new { Message = "Güncellenecek not bulunamadı." });
        }

        return Ok(new { Message = "Not başarıyla güncellendi." });
    }

    // 5. Not Silme (Delete)
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _sender.Send(new DeleteNoteCommand(id));

        if (!success)
        {
            return NotFound(new { Message = "Silinecek not bulunamadı." });
        }

        return Ok(new { Message = "SSNot başarıyla silindi." });
    }
}


