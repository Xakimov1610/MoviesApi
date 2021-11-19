using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Image
    {
        public Guid Id { get; set; }

        [Required]
        public Byte[] Data { get; set; }
        public Guid MovieId { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }

        public Image(Guid MoviId);

        public Image(Guid id, DateTimeOffset releaseDate)
        {
            this.Id = id;
            this.ReleaseDate = releaseDate;

        }

    }
}