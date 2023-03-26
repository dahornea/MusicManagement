namespace DTOs
{
    public class ArtistTotalDTO
    {
        //show all artists ordered by the total number of songs they have

        public long? id { get; set; }
        public string? Name { get; set; }
        public int TotalSongs { get; set; }

    }
}