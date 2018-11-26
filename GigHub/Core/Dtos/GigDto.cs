using System;

namespace GigHub.Core.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }
        public bool IsCanceled { get; set; }

        public UserDto Artist { get; set; }

        //[Required]
       // public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        //[Required]
        //[StringLength(255)]
        public string Venue { get; set; }


        public GenreDto Genre { get; set; }

        //[Required]
        //public byte GenreId { get; set; }

        //public ICollection<Attendance> Attendances { get; private set; }
    }
}