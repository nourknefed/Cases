using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using NOUR.models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using NOUR.Data;
using static NOUR.models.Case;
using Microsoft.Extensions.Configuration;

namespace NOUR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CasesController : ControllerBase
    {
        private readonly SqlDbContext _context;




        //private readonly UserManager<User> _userManager;
        public IConfiguration Configuration { get; }

        public CasesController(SqlDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;

        }


        //public CasesController(SqlDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        //{
        //    _context = context;
        //    _httpContextAccessor = httpContextAccessor;

        //    _userManager = userManager;

        //}

        // GET: api/Cases
        [HttpGet("GetCases")]

        public async Task<ActionResult<IEnumerable<Case>>> GetCase()
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user2 = await _context.Users.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(userId));

            return await _context.Case.Where(w => w.UserId.ToString() == user2.Id.ToString()).ToListAsync();
        }

        // GET: api/Cases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Case>> GetCase(int id)
        {
            var @case = await _context.Case.FindAsync(id);

            if (@case == null)
            {
                return NotFound();
            }

            return @case;
        }

        //SEARCH BY NAME

        [HttpGet("FilterCases")]
        public async Task<ActionResult<Case>> FilterCases([FromQuery] String name, [FromQuery] int? status, [FromQuery] DateTime? creatdate)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user2 = await _context.Users.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(userId));

            if (!String.IsNullOrEmpty(name))
            {
                var namefilter = await _context.Case.Where(x => x.Customer.StartsWith(name)).ToListAsync();

                namefilter = namefilter.Where(x => x.UserId== user2.Id).ToList();

                return Ok(namefilter);

            }

            else if (status != null)
            {
                var statusfilter = await _context.Case.Where(x => x.status == (status == 0 ? Status.Closed : status == 1 ? Status.Inprogress : Status.Notstarted)).ToListAsync();
                statusfilter = statusfilter.Where(x => x.UserId == user2.Id).ToList();

                return Ok(statusfilter);
            }


            else if (creatdate != null)
            {
                var Datefilter = await _context.Case.Where(x => x.CreationDate.Year == creatdate.Value.Year && x.CreationDate.Month == creatdate.Value.Month && x.CreationDate.Day == creatdate.Value.Day).ToListAsync();

                Datefilter = Datefilter.Where(x => x.UserId == user2.Id).ToList();
                return Ok(Datefilter);

            }




            var @case = await _context.Case.ToListAsync();

            if (@case == null)
            {
                return NotFound();
            }

            return Ok(@case);
        }
















        // PUT: api/Cases/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCase(int id, Case @case)
        {
            if (id != @case.Id)
            {
                return BadRequest();
            }
            @case.LastModified = DateTime.Now;
            var Case = _context.Case.FirstOrDefault(x => x.Id == @case.Id);
            //_context.Entry(@case).State = EntityState.Modified;

            Case.Customer = @case.Customer;
            Case.status = @case.status;
            Case.Info = @case.Info;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Cases
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Case>> PostCase(Case @case)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user2 = await _context.Users.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(userId));
            @case.UserId = user2.Id;
            @case.CreationDate = DateTime.Now;

            _context.Case.Add(@case);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCase", new { id = @case.Id }, @case);
        }

        // DELETE: api/Cases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Case>> DeleteCase(int id)
        {
            var @case = await _context.Case.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }

            _context.Case.Remove(@case);
            await _context.SaveChangesAsync();

            return @case;
        }

        private bool CaseExists(int id)
        {
            return _context.Case.Any(e => e.Id == id);
        }
    }
}
