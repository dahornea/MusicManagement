using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Albums.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordLabelController : ControllerBase
    {
        private readonly ProjectContext _context;

        public RecordLabelController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/RecordLabel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordLabel>>> GetRecordLabel()
        {
          if (_context.RecordLabel == null)
          {
              return NotFound();
          }
            return await _context.RecordLabel.ToListAsync();
        }

        // GET: api/RecordLabel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordLabel>> GetRecordLabel(long id)
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

            return recordLabel;
        }

        // PUT: api/RecordLabel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecordLabel(long id, RecordLabel recordLabel)
        {
            if (id != recordLabel.RecordLabelId)
            {
                return BadRequest();
            }

            _context.Entry(recordLabel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordLabelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RecordLabel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecordLabel>> PostRecordLabel(RecordLabel recordLabel)
        {
          if (_context.RecordLabel == null)
          {
              return Problem("Entity set 'ProjectContext.RecordLabel'  is null.");
          }
            _context.RecordLabel.Add(recordLabel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecordLabel", new { id = recordLabel.RecordLabelId }, recordLabel);
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
    }
}
