namespace Model
{
    public class Certifications
    {
        //many to many between RecordLabel and Artist

        public string? Award { get; set; }

        public int? UnitsSold { get; set; }

        public long? RecordLabelId { get; set; }
        public long? ArtistId { get; set; }

        //Hidden from the user
        public virtual RecordLabel? RecordLabel { get; set; } = null!;
        public virtual Artist? Artist { get; set; } = null!;
    }
}