using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model

{
    public class Album
    {
        public long AlbumId { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? YearOFRelease { get; set; }
        public int Price { get; set; }
        public int NumberOfTracks { get; set; }
        
        //Hidden from the user
        public virtual Artist Artist { get; set; } = null!; //one artist can have many albums
        public long? ArtistId { get; set; }
        public virtual RecordLabel RecordLabel { get; set; } = null!; //one record label can have many albums
        public long? RecordLabelId { get; set; }
    }   
}