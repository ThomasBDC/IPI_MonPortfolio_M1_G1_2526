using MonPortfolio_M1_2526.Data;
using System.ComponentModel.DataAnnotations;

namespace MonPortfolio_M1_2526.Entities
{
    public class CommentEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;

        public ApplicationUser? Author { get; set; }

        [Required]
        public ArticleEntity Article { get; set; } = default!;
    }
}
