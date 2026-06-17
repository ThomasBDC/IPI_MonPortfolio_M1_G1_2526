using MonPortfolio_M1_2526.Data;
using System.ComponentModel.DataAnnotations;

namespace MonPortfolio_M1_2526.Entities
{
    public class ArticleEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [MaxLength(400)]
        public string? Description { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public ApplicationUser? Author { get; set; }
    }
}
