namespace TestWebAPI.Controllers
{
    using System;

    public class NoteItem
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Body { get; set; }
        
        public DateTimeOffset CreatedAt { get; set; }
        
        public DateTimeOffset UpdatedAt { get; set; }

        public override string ToString()
        {
            return $"{Id}:{Title}";
        }
    }
}