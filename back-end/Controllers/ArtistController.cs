using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using DTOs;
using Controllers;

namespace Albums.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly Validations _validations = new Validations();

        public ArtistController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Artist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtist()
        {
          if (_context.Artist == null)
          {
              return NotFound();
          }
            return await _context.Artist
                .Select(x => ArtistToDTO(x))
                .ToListAsync();
        }

        // GET: api/Artist/5
        //fetch artist with given id the collection of albums inside the artist object
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(long id)
        {
            if (_context.Artist == null)
            {
                return NotFound();
            }
                var artist = await _context.Artist
                    .Include(x => x.Albums)
                    .Include(x => x.RecordLabel)
                    .Include(x => x.Certifications)
                    .FirstOrDefaultAsync(x => x.ArtistId == id);
    
                if (artist == null)
                {
                    return NotFound();
                }
    
                return artist;
        }

        // PUT: api/Artist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(long id, ArtistDTO artistDTO)
        {
            if(id != artistDTO.ArtistId)
            {
                return BadRequest();
            }

            var artistToUpdate = await _context.Artist.FindAsync(id);
            var recordLabel = await _context.RecordLabel.FindAsync(artistDTO.RecordLabelId);
            if (artistToUpdate == null)
            {
                return NotFound();
            }
            if(recordLabel == null)
            {
                return NotFound();
            }
            artistToUpdate.Name = artistDTO.Name;
            artistToUpdate.Country = artistDTO.Country;
            artistToUpdate.DateOfBirth = artistDTO.DateOfBirth;
            artistToUpdate.MainGenre = artistDTO.MainGenre;
            artistToUpdate.RecordLabelId = artistDTO.RecordLabelId;
            artistToUpdate.RecordLabel = recordLabel;

            if(_validations.ValidateArtist(artistToUpdate) == false)
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ArtistExists(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Artist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(ArtistDTO artistDTO)
        {
            var recordLabel = await _context.RecordLabel.FindAsync(artistDTO.RecordLabelId);
            if(recordLabel == null)
            {
                return NotFound();
            }
            var artist = new Artist
            {
                Name = artistDTO.Name,
                Country = artistDTO.Country,
                DateOfBirth = artistDTO.DateOfBirth,
                MainGenre = artistDTO.MainGenre,
                RecordLabelId = artistDTO.RecordLabelId,
                RecordLabel = recordLabel
            };

            if(_validations.ValidateArtist(artist) == false)
            {
                return BadRequest();
            }

            _context.Artist.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArtist), new { id = artist.ArtistId }, ArtistToDTO(artist));
        }

        // DELETE: api/Artist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(long id)
        {
            if (_context.Artist == null)
            {
                return NotFound();
            }
            var artist = await _context.Artist.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artist.Remove(artist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistExists(long id)
        {
            return (_context.Artist?.Any(e => e.ArtistId == id)).GetValueOrDefault();
        }

        //create a new certification
        [HttpPost("{RecordLabelId}/certification/{ArtistId}")]
        public async Task<ActionResult<CertificationsDTO>> PostCertification(long RecordLabelId, long ArtistId, CertificationsDTO certificationDTO)
        {
            if (_context.Artist == null)
            {
                return Problem("Entity set 'ProjectContext.Artist'  is null.");
            }
            if (_context.RecordLabel == null)
            {
                return Problem("Entity set 'ProjectContext.RecordLabel'  is null.");
            }
            var artist = await _context.Artist.FindAsync(ArtistId);
            var recordLabel = await _context.RecordLabel.FindAsync(RecordLabelId);
            var certification = new Certifications
            {
                Artist = artist,
                RecordLabel = recordLabel,
                Award = certificationDTO.Award,
                UnitsSold = certificationDTO.UnitsSold
            };
            _context.Certifications.Add(certification);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCertification), certificationDTO);
        }

        private static ArtistDTO ArtistToDTO(Artist artist) =>
            new ArtistDTO
            {
                ArtistId = artist.ArtistId,
                Name = artist.Name,
                Country = artist.Country,
                DateOfBirth = artist.DateOfBirth,
                MainGenre = artist.MainGenre,
                RecordLabelId = artist.RecordLabelId
            };

        //show all artists ordered by the total number of songs they have
        [HttpGet("Total")]
        public async Task<ActionResult<IEnumerable<ArtistTotalDTO>>> GetArtistTotal()
        {
            if (_context.Artist == null)
            {
                return NotFound();
            }

            //get all albums
            var albums = await _context.Album
                .Include(x => x.Artist)
                .ToListAsync();
            if (albums == null)
            {
                return NotFound();
            }

            var artists = new List<ArtistTotalDTO>();

            if(artists == null)
            {
                return NotFound();
            }
            
            //loop through all albums and calculate the total number of songs for each artist
            foreach (var album in albums)
            {
                var artist = artists.FirstOrDefault(x => x.id == album.Artist.ArtistId);
                if (artist == null)
                {
                    artist = new ArtistTotalDTO
                    {
                        id = album.Artist.ArtistId,
                        Name = album.Artist.Name,
                        TotalSongs = album.NumberOfTracks
                    };
                    artists.Add(artist);
                }
                else
                {
                    artist.TotalSongs += album.NumberOfTracks;
                }
                artists = artists.OrderByDescending(x => x.TotalSongs).ToList();

            }
            return artists;
        }
        //Show how much money each artist has earned from their albums based on Certifications and the price of the album
        [HttpGet("Money")]
        public async Task<ActionResult<IEnumerable<ArtistMoneyDTO>>> GetArtistMoney()
        {
            if (_context.Artist == null)
            {
                return NotFound();
            }

            //get all albums
            var albums = await _context.Album
                .Include(x => x.Artist)
                .ToListAsync();

            //get all certifications
            var certifications = await _context.Certifications
                .Include(x => x.Artist)
                .ToListAsync();

            var artists = new List<ArtistMoneyDTO>();

            //loop through all albums and calculate the total money earned for each artist
            foreach (var album in albums)
            {
                var artist = artists.FirstOrDefault(x => x.id == album.Artist.ArtistId);
                if (artist == null)
                {
                    artist = new ArtistMoneyDTO
                    {
                        id = album.Artist.ArtistId,
                        Name = album.Artist.Name,
                        MoneyEarned = album.Price
                    };
                    artists.Add(artist);
                }
                else
                {
                    artist.MoneyEarned += album.Price;
                }
            }

            //loop through all certifications and calculate the total money earned for each artist
            foreach (var certification in certifications)
            {

                if(certifications == null)
                {
                    return NotFound();
                }
                if(certification.Artist == null)
                {
                    return NotFound();
                }
                if(certification.UnitsSold == null)
                {
                    return NotFound();
                }
                var artist = artists.FirstOrDefault(x => x.id == certification.Artist.ArtistId);
                if (artist == null)
                {
                    artist = new ArtistMoneyDTO
                    {
                        id = certification.Artist.ArtistId,
                        Name = certification.Artist.Name,
                        MoneyEarned = certification.UnitsSold * 0.1
                    };
                    artists.Add(artist);
                }
                else
                {
                    artist.MoneyEarned += (int)certification.UnitsSold * 0.1;
                }
            }
            return artists.OrderByDescending(x => x.MoneyEarned).ToList();
    }
    [HttpPut("{id}/Albums")]
    public async Task<IActionResult> PutAlbumsToArtist(long id, [FromBody]List<long> albumIds){
        if(_context.Artist == null){
            return NotFound();
        }
        var artist = await _context.Artist
            .Include(x => x.Albums)
            .FirstOrDefaultAsync(x => x.ArtistId == id);
        if(artist == null){
            return NotFound();
        }
        var albums = await _context.Album
            .Where(x => albumIds.Contains(x.AlbumId))
            .ToListAsync();
        if(albums.Count != albumIds.Count){
            return BadRequest();
        }
        artist.Albums.Clear();
        artist.Albums = albums;

        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPost("{id}/Albums")]
    public async Task<IActionResult> PostAlbumsToArtist(long id, List<AlbumDTO> albumsDTO){
        if(_context.Artist == null){
            return NotFound();
        }
        var artist = await _context.Artist.FindAsync(id);
        if(artist == null){
            return NotFound();
        }
        foreach (var albumDTO in albumsDTO){
            var album = new Album{
                Title = albumDTO.Title,
                Genre = albumDTO.Genre,
                YearOFRelease = albumDTO.YearOFRelease,
                Price = albumDTO.Price,
                NumberOfTracks = albumDTO.NumberOfTracks,
                ArtistId = id,
            };
            _context.Album.Add(album);
        }
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
}

