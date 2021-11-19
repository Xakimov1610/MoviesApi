using System;
using System.ComponentModel.DataAnnotations;

namespace movie.Models
{
    public class NewActor
    {
        [Required]
        [MaxLength(255)]
        public string Fullname { get; set; }

        [Required]
        public DateTimeOffset Birthdate { get; set; }
    }
}