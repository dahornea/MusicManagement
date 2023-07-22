using System.Text.Json.Serialization;
namespace Model
{
    public class Artist
    {
        public long ArtistId { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? DateOfBirth { get; set; }
        public string? MainGenre { get; set; }
        public long? RecordLabelId { get; set; }

        //Hidden from the user
        public virtual ICollection<Album> Albums { get; set; } = null!; //one artist can have many albums

        public virtual RecordLabel RecordLabel { get; set; } = null!; //one artist can have multiple
        public virtual ICollection<Certifications> Certifications { get; set; } = null!; //many to many between RecordLabel and Artist

    }
}