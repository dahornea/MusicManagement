namespace Model
{
    public class RecordLabel
    {
        public long RecordLabelId { get; set; }

        public string? Name { get; set; }

        public string? Country { get; set; }

        public string? DateOfEstablishment { get; set; }

        public string? CEO { get; set; }

        public int NumberOfArtists { get; set; }

        //Hidden from the user

        public virtual ICollection<Artist> Artists { get; set; } = null!; //one record label can have many artists
        public virtual ICollection<Album> Albums { get; set; } = null!; //one record label can have many albums
        public virtual ICollection<Certifications> Certifications { get; set; } = null!; //many to many between RecordLabel and Artist
    }
}