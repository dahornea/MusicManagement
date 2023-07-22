using Model;

namespace Controllers
{
    public class Validations
    {
        public bool ValidateArtist(Artist artist){
            if (artist.Name == null || artist.Name.Length < 2 || artist.Name.Length > 50)
                return false;
            if (artist.Country == null || artist.Country.Length < 2 || artist.Country.Length > 50)
                return false;
            if (artist.DateOfBirth == null || artist.DateOfBirth.Length < 2 || artist.DateOfBirth.Length > 50)
                return false;
            if (artist.MainGenre == null || artist.MainGenre.Length < 2 || artist.MainGenre.Length > 50)
                return false;
            return true;
        }

        public bool ValidateAlbum(Album album){
            if (album.Title == null || album.Title.Length < 2 || album.Title.Length > 50)
                return false;
            if (album.YearOFRelease == null || album.YearOFRelease.Length < 2 || album.YearOFRelease.Length > 50)
                return false;
            if (album.Genre == null || album.Genre.Length < 2 || album.Genre.Length > 50)
                return false;
            if (album.Price < 0 || album.Price > 1000)
                return false;
            if (album.NumberOfTracks < 0 || album.NumberOfTracks > 100)
                return false;
            return true;
        }

        public bool ValidateRecordLabel(RecordLabel recordLabel){
            if (recordLabel.Name == null || recordLabel.Name.Length < 2 || recordLabel.Name.Length > 50)
                return false;
            if (recordLabel.Country == null || recordLabel.Country.Length < 2 || recordLabel.Country.Length > 50)
                return false;
            if (recordLabel.DateOfEstablishment == null || recordLabel.DateOfEstablishment.Length < 2 || recordLabel.DateOfEstablishment.Length > 50)
                return false;
            if (recordLabel.CEO == null || recordLabel.CEO.Length < 2 || recordLabel.CEO.Length > 50)
                return false;
            if (recordLabel.NumberOfArtists < 0 || recordLabel.NumberOfArtists > 1000)
                return false;
            return true;
        }
    }
}