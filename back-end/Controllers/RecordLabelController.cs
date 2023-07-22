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
    public class RecordLabelController : ControllerBase
    {
        private readonly ProjectContext _context;
        private readonly Validations _validations = new Validations();

        public RecordLabelController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/RecordLabel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordLabelDTO>>> GetRecordLabel()
        {
          if (_context.RecordLabel == null)
          {
              return NotFound();
          }
            return await _context.RecordLabel
                .Select(x => RecordLabelToDTO(x))
                .ToListAsync();
        }

        // GET: api/RecordLabel/5
        // fetch record label with given id and array of full albums and artists
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordLabel>> GetRecordLabel(long id)
        {
            if (_context.RecordLabel == null)
            {
                return NotFound();
            }
                var recordLabel = await _context.RecordLabel
                    .Include(x => x.Albums)
                    .Include(x => x.Artists)
                    .FirstOrDefaultAsync(x => x.RecordLabelId == id);
    
                if (recordLabel == null)
                {
                    return NotFound();
                }
    
                return recordLabel;
        }

        // PUT: api/RecordLabel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecordLabel(long id, RecordLabelDTO recordLabelDTO)
        {
            if (id != recordLabelDTO.RecordLabelId)
            {
                return BadRequest();
            }

            var recordLabel = await _context.RecordLabel.FindAsync(id);
            if (recordLabel == null)
            {
                return NotFound();
            }

            recordLabel.Name = recordLabelDTO.Name;
            recordLabel.Country = recordLabelDTO.Country;
            recordLabel.DateOfEstablishment = recordLabelDTO.DateOfEstablishment;
            recordLabel.CEO = recordLabelDTO.CEO;
            recordLabel.Description = recordLabelDTO.Description;
            recordLabel.NumberOfArtists = recordLabelDTO.NumberOfArtists;

            if(!_validations.ValidateRecordLabel(recordLabel))
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!RecordLabelExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/RecordLabel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecordLabelDTO>> PostRecordLabel(RecordLabelDTO recordLabelDTO)
        {
            var recordLabel = new RecordLabel
            {
                Name = recordLabelDTO.Name,
                Country = recordLabelDTO.Country,
                DateOfEstablishment = recordLabelDTO.DateOfEstablishment,
                CEO = recordLabelDTO.CEO,
                Description = recordLabelDTO.Description,
                NumberOfArtists = recordLabelDTO.NumberOfArtists
            };

            if(!_validations.ValidateRecordLabel(recordLabel))
            {
                return BadRequest();
            }
            
            _context.RecordLabel.Add(recordLabel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetRecordLabel),
                new { id = recordLabel.RecordLabelId },
                RecordLabelToDTO(recordLabel));
        }

        // DELETE: api/RecordLabel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecordLabel(long id)
        {
            if (_context.RecordLabel == null)
            {
                return NotFound();
            }
            var recordLabel = await _context.RecordLabel.FindAsync(id);
            if (recordLabel == null)
            {
                return NotFound();
            }

            _context.RecordLabel.Remove(recordLabel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecordLabelExists(long id)
        {
            return (_context.RecordLabel?.Any(e => e.RecordLabelId == id)).GetValueOrDefault();
        }

        
        [HttpGet("filter/{NumberOfArtists}")]
        public async Task<ActionResult<IEnumerable<RecordLabel>>> GetRecordLabelByNumberOfArtists(int NumberOfArtists)
        {
          if (_context.RecordLabel == null)
          {
              return NotFound();
          }
            return await _context.RecordLabel.Where(x => x.NumberOfArtists > NumberOfArtists).ToListAsync();
        }

        private static RecordLabelDTO RecordLabelToDTO(RecordLabel recordLabel) =>
            new RecordLabelDTO
            {
                RecordLabelId = recordLabel.RecordLabelId,
                Name = recordLabel.Name,
                Country = recordLabel.Country,
                DateOfEstablishment = recordLabel.DateOfEstablishment,
                CEO = recordLabel.CEO,
                Description = recordLabel.Description,
                NumberOfArtists = recordLabel.NumberOfArtists
            };
    }
}
