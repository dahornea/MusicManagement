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
        public string? RecordLabel { get; set; }
        public int Price { get; set; }
        public int NumberOfTracks { get; set; }
        public virtual Artist Artist { get; set; } = null!;
        public long? ArtistId { get; set; }
    }   
}