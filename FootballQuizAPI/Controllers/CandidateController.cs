using System;
using Microsoft.AspNetCore.Mvc;
using FootballQuizAPI.Models;
using FootballQuizAPI.Error;

namespace NaovisQuizAPI.Controllers
{
    [Route("api/candidates")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly FootballQuizContext _context;
        public CandidateController(FootballQuizContext context)
        {
            _context = context;
        }

        [HttpGet("{id}", Name = "GetCandidate")]
        public ActionResult<Candidate> GetById(int id)
        {
            var candidate = _context.candidate.Find(id);
            if (candidate == null)
            {
                return NotFound();
            }
            return candidate;
        }

        [HttpPost]
        public IActionResult Create(Candidate candidate)
        {
            _context.candidate.Add(candidate);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new Error() { ErrorMessage = ex.Message, InnerException = ex.InnerException.Message });

            }
            return CreatedAtRoute("GetCandidate", new { id = candidate.id }, candidate);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Candidate candidate)
        {
            var c = _context.candidate.Find(id);
            if (c == null)
            {
                return NotFound();
            }

            c.first_name = candidate.first_name;
            c.last_name = candidate.last_name;
            c.email = candidate.email;

            _context.candidate.Update(c);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var c = _context.candidate.Find(id);
            if (c == null)
            {
                return NotFound();
            }

            _context.candidate.Remove(c);
            _context.SaveChanges();
            return NoContent();
        }
    }
}