using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nothing_Fancy.Models;
using Nothing_Fancy.Models.DataContext;

namespace Nothing_Fancy.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationDbContext _context;

        public ReservationController(ReservationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Detailed(string sortOrder, string searchString)
        {
            var reservations = from r in _context.Reservation
                               select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                reservations = reservations.Where(s => (s.reserverName.Contains(searchString) || s.nameOfRoom.Contains(searchString) ||
                                              s.Id.Equals(searchString)));
            }

            switch (sortOrder)
            {
                case "Date_A":
                    reservations = _context.Reservation.OrderBy(r => r.reserveDateBegin);
                    break;
                case "Date_D":
                    reservations = _context.Reservation.OrderByDescending(r => r.reserveDateBegin);
                    break;
                case "Name_A":
                    reservations = _context.Reservation.OrderBy(r => r.reserverName);
                    break;
                case "Name_D":
                    reservations = _context.Reservation.OrderByDescending(r => r.reserverName);
                    break;

            }
            return View(await reservations.ToListAsync());
        }


        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            var dbList = from r in _context.Reservation
                         select r;
            ViewBag.List = dbList.ToList();
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,reserverName,nameOfRoom,reserveDateBegin,reserveDateEnd,cost")] Reservation reservation)
        {
            DateTime start = reservation.reserveDateBegin;
            DateTime end = reservation.reserveDateEnd;
            string room = reservation.nameOfRoom;


            var dbList = from r in _context.Reservation
                         where r.nameOfRoom == room
                         where r.reserveDateBegin <= end
                         where r.reserveDateEnd >= start
                         select r;

            List<int> reserveIds = dbList.Select(c => c.Id).Distinct().ToList();

            if (dbList.Any())
            {
                ModelState.AddModelError("reserveDateBegin", "There is a date conflict in our database with reservation ID's " + string.Join(", ", reserveIds) + ", check our calendar for availability!");
                return View(reservation);
            }


            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Calendar));
            }
            return View(reservation);
        }


        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Calendar));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Calendar()
        {
            var reservations = from r in _context.Reservation
                               select r;
            return View(await reservations.ToListAsync());
        }

        // API GET
        [HttpGet]
        [Route("api/v1/reservations")]
        public IActionResult GetReservations([FromQuery] string ID, [FromQuery] string Name, [FromQuery] string RoomNumber,
                                             [FromQuery] string DateBegin, [FromQuery] string DateEnd, [FromQuery] string Cost)
        {
            var reservations = from r in _context.Reservation
                               select r;

            if (!String.IsNullOrEmpty(ID))
            {
                reservations = reservations.Where(r => (r.Id == Int16.Parse(ID)));
            }

            if (!String.IsNullOrEmpty(Name))
            {
                reservations = reservations.Where(r => (r.reserverName == Name));
            }

            if (!String.IsNullOrEmpty(RoomNumber))
            {
                reservations = reservations.Where(r => (r.nameOfRoom.Contains(RoomNumber)));
            }

            if (!String.IsNullOrEmpty(DateBegin))
            {
                reservations = reservations.Where(r => (r.reserveDateBegin == DateTime.Parse(DateBegin)));
            }

            if (!String.IsNullOrEmpty(DateEnd))
            {
                reservations = reservations.Where(r => (r.reserveDateEnd == DateTime.Parse(DateEnd)));
            }

            if (!String.IsNullOrEmpty(Cost))
            {
                reservations = reservations.Where(r => (r.cost == double.Parse(Cost)));
            }

            return Ok(reservations);
        }

        // API POST
        [HttpPost]
        [Route("api/v1/reservations")]
        public async Task<IActionResult> PostReview([FromBody] Dictionary<string, string> jsonInfo)
        {
            if (!jsonInfo.ContainsKey("Name"))
            {
                return BadRequest("Did not have Name key.");
            }

            else if (!jsonInfo.ContainsKey("RoomNumber"))
            {
                return BadRequest("Did not have RoomNumber key.");
            }

            else if (!jsonInfo.ContainsKey("DateBegin"))
            {
                return BadRequest("Did not have DateBegin key.");
            }

            else if (!jsonInfo.ContainsKey("DateEnd"))
            {
                return BadRequest("Did not have DateEnd key.");
            }

            var reservation = new Reservation();
            reservation.reserverName = jsonInfo["Name"];
            reservation.nameOfRoom = jsonInfo["RoomNumber"];
            reservation.reserveDateBegin = DateTime.Parse(jsonInfo["DateBegin"]);
            reservation.reserveDateEnd = DateTime.Parse(jsonInfo["DateEnd"]);

            switch (jsonInfo["RoomNumber"])
            {
                case "Room 101":
                    reservation.cost = (reservation.reserveDateEnd - reservation.reserveDateBegin).TotalDays * 10;
                    break;
                case "Room 102":
                    reservation.cost = (reservation.reserveDateEnd - reservation.reserveDateBegin).TotalDays * 20;
                    break;
                case "Room 103":
                    reservation.cost = (reservation.reserveDateEnd - reservation.reserveDateBegin).TotalDays * 30;
                    break;
                default:
                    return BadRequest(jsonInfo["RoomNumber"] + " is not a room that is listed in our hotel.");
            };

            await Create(reservation);

            return Ok(reservation);
        }
    }
}
