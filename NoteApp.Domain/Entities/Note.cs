using NoteApp.Domain.Common;

namespace NoteApp.Domain.Entities;

public class Note : BaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public bool IsArchived { get; private set; } = false;

    // EF Core için boş kurucu metot
    private Note() { }

    public Note(string title, string content)
    {
        Title = title;
        Content = content;
    }

    // Güncelleme metodu
    public void Update(string title, string content)
    {
        Title = title;
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }
}