using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Image
    {
        public Guid Id { get; set; }

        [Required]
        public byte[] Data { get; set; }
        public Guid MovieId { get; set; }

        public Image(byte[] data, Guid movieId)
        {
            Id = Guid.NewGuid();
            Data = data;
            MovieId = movieId;

        }

        [Obsolete("Used only for Entities binding.", true)]
        public Image() {}
    }
}