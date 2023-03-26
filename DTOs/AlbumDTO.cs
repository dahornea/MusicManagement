namespace DTOs
{
    public class AlbumDTO
    {
        public long AlbumId { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? YearOFRelease { get; set; }
        public int Price { get; set; }
        public int NumberOfTracks { get; set; }

        public long? ArtistId { get; set; }

        public long? RecordLabelId { get; set; }
    }
}