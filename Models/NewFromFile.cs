using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MoviesApi.Models
{
    public class NewFromFile
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}