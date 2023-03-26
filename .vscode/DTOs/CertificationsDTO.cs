using Microsoft.Build.Framework;
namespace DTOs
{
    public class CertificationsDTO
    {
        public string? Award { get; set; }

        public int? UnitsSold { get; set; }

        public long? RecordLabelId { get; set; }
        public long? ArtistId { get; set; }
    }
}