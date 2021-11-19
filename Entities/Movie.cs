using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie.Entities
{
    public class Movie
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        [Range(0,10)]
        public double Rating { get; set; }

        [Required]
        public DateTimeOffset ReleaseDate { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Actor> Actors { get; set; }
          
        
    }
}