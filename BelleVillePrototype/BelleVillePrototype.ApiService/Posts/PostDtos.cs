using System.ComponentModel.DataAnnotations;


namespace BelleVillePrototype.ApiService.Posts
{
    public class PostCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Author { get; set; }
    }

    public class PostGetDto(Guid id, string title, string? author)
    {
        public Guid Id { get; set; } = id;
        public string Title { get; set; } = title;
        public string? Author { get; set; } = author;
    }

    public class PostQueryDto
    {
        public Guid? Id { get; set; } 
        
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
        public string? Title { get; set; } 
        
        [MinLength(3, ErrorMessage = "Author must be at least 3 characters long.")]
        public string? Author { get; set; }
    }
}
