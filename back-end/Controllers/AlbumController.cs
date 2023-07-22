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
    public class AlbumController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly Validations _validations = new Validations();

        public AlbumController(ProjectContext context)
        {
            _context = context;
        }


        // GET: api/Album
        //fetch all albums + their artist id and record label id
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAlbum()
        {
            if (_context.Album == null)
            {
                return NotFound();
            }
                return await _context.Album
                    .Select(x => AlbumToDTO(x))
                    .ToListAsync();
        }

        // GET: api/Album/5
        //return the album with the given id and the complete artist and record label objects
        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetAlbum(long id)
        {
            if (_context.Album == null)
            {
                return NotFound();
            }
            
                var album = await _context.Album
                    .Include(x => x.Artist)
                    .Include(x => x.RecordLabel)
                    .FirstOrDefaultAsync(x => x.AlbumId == id);
    
                if (album == null)
                {
                    return NotFound();
                }
    
                return album;

        }

        // PUT: api/Album/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(long id, AlbumDTO albumDTO)
        {
            if (id != albumDTO.AlbumId)
            {
                return BadRequest();
            }
            var album = await _context.Album.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            //search for the artist id and return BadRequest if it is invalid
            var artist = await _context.Artist.FindAsync(albumDTO.ArtistId);
            var recordlabel = await _context.RecordLabel.FindAsync(albumDTO.RecordLabelId);
            if (artist == null)
                return BadRequest();
            if (recordlabel == null)
                return BadRequest();
            
            album.Title =  albumDTO.Title;
            album.Genre = albumDTO.Genre;
            album.YearOFRelease = albumDTO.YearOFRelease;
            album.Price = albumDTO.Price;
            album.NumberOfTracks = albumDTO.NumberOfTracks;
            album.ArtistId = albumDTO.ArtistId;
            album.RecordLabelId = albumDTO.RecordLabelId;
            album.Artist = artist;
            album.RecordLabel = recordlabel;

            if(_validations.ValidateAlbum(album) == false)
            {
                return BadRequest();
            }
            
            try{
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AlbumExists(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Album
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //Add the corresponding artist to the Artist field of the album. The artist should be found by the artistId field of the album.
        [HttpPost]
        public async Task<ActionResult<AlbumDTO>> PostAlbum(AlbumDTO albumDTO)
        {
          if (_context.Album == null)
          {
              return Problem("Entity set 'ProjectContext.Album'  is null.");
          }
            
            // search for the artist id and return BadRequest if it is invalid
            var artist = await _context.Artist.FindAsync(albumDTO.ArtistId);
            var recordlabel = await _context.RecordLabel.FindAsync(albumDTO.RecordLabelId);
            if (artist == null)
                return BadRequest();
            if (recordlabel == null)
                return BadRequest();

            var newAlbum = new Album
            {
                Title = albumDTO.Title,
                Genre = albumDTO.Genre,
                YearOFRelease = albumDTO.YearOFRelease,
                Price = albumDTO.Price,
                NumberOfTracks = albumDTO.NumberOfTracks,
                ArtistId = albumDTO.ArtistId,
                Artist = artist,
                RecordLabelId = albumDTO.RecordLabelId,
                RecordLabel = recordlabel
            };
            if(_validations.ValidateAlbum(newAlbum) == false)
            {
                return BadRequest();
            }
            _context.Album.Add(newAlbum);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAlbum),
                new { id = newAlbum.AlbumId },
                AlbumToDTO(newAlbum));
        }

        // DELETE: api/Album/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(long id)
        {
            if (_context.Album == null)
            {
                return NotFound();
            }
            var album = await _context.Album.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            _context.Album.Remove(album);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlbumExists(long id)
        {
            return (_context.Album?.Any(e => e.AlbumId == id)).GetValueOrDefault();
        }

        //Implement filtering on a numeric field for at least one entity. The filtering should return all entities with the numeric field higher than a given value.
        [HttpGet("filter/{Price}")]
        public async Task<IActionResult> FilterAlbumByPrice(int price){
            if(_context.Album == null){
                return NotFound();
            }
            var album = await _context.Album.Where(a => a.Price > price).ToListAsync();
            if(album == null){
                return NotFound();
            }
            //return list of albums with price higher than given value
            return Ok(album);
            

        }

        private static AlbumDTO AlbumToDTO(Album album){
            return new AlbumDTO{
                AlbumId = album.AlbumId,
                Title = album.Title,
                Genre = album.Genre,
                YearOFRelease = album.YearOFRelease,
                Price = album.Price,
                NumberOfTracks = album.NumberOfTracks,
                ArtistId = album.ArtistId,
                RecordLabelId = album.RecordLabelId
            };
        }
    }
}
