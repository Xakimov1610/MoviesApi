using System.ComponentModel.DataAnnotations;

namespace movie.Models
{
    public class NewGenre
    {
    [Required]
    [MaxLength(20)]
    public string Name { get; set; }
    }
}