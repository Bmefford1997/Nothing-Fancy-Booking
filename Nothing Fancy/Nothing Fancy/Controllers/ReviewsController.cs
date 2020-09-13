using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nothing_Fancy.Models;
using Nothing_Fancy.Models.DataContext;

namespace Nothing_Fancy.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ReviewDbContext _context;

        public ReviewsController(ReviewDbContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            var reviews = from r in _context.Review
                          select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                reviews = reviews.Where(s => (s.reviewerName.Contains(searchString) ||
                                              s.Title.Contains(searchString)));
            }

            switch (sortOrder)
            {
                case "Date_A":
                    reviews = _context.Review.OrderBy(r => r.reviewDate);
                    break;
                case "Date_D":
                    reviews = _context.Review.OrderByDescending(r => r.reviewDate);
                    break;
                case "Name_A":
                    reviews = _context.Review.OrderBy(r => r.reviewerName);
                    break;
                case "Name_D":
                    reviews = _context.Review.OrderByDescending(r => r.reviewerName);
                    break;
                case "Title_A":
                    reviews = _context.Review.OrderBy(r => r.Title);
                    break;
                case "Title_D":
                    reviews = _context.Review.OrderByDescending(r => r.Title);
                    break;
                case "Rating_A":
                    reviews = _context.Review.OrderBy(r => r.reviewRate);
                    break;
                case "Rating_D":
                    reviews = _context.Review.OrderByDescending(r => r.reviewRate);
                    break;
            }

            return View(await reviews.ToListAsync());

        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,reviewerName,reviewRate,reviewDate,reviewContent")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,reviewerName,reviewRate,reviewDate,reviewContent")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Review.FindAsync(id);
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.Id == id);
        }

        // API GET
        [HttpGet]
        [Route("api/v1/reviews")]
        public IActionResult GetReviews([FromQuery] string ID, [FromQuery] string Title, [FromQuery] string Name,
                                        [FromQuery] string Rate)
        {
            var reviews = from r in _context.Review
                          select r;

            if (!String.IsNullOrEmpty(ID))
            {
                reviews = reviews.Where(r => (r.Id == Int16.Parse(ID)));
            }

            if (!String.IsNullOrEmpty(Title))
            {
                reviews = reviews.Where(r => (r.Title == Title));
            }

            if (!String.IsNullOrEmpty(Name))
            {
                reviews = reviews.Where(r => (r.reviewerName.Contains(Name)));
            }

            if (!String.IsNullOrEmpty(Rate))
            {
                reviews = reviews.Where(r => (r.Id == Int16.Parse(Rate)));
            }

            return Ok(reviews);
        }

        // API POST
        [HttpPost]
        [Route("api/v1/reviews")]
        public async Task<IActionResult> PostReview([FromBody] Dictionary<string, string> jsonInfo)
        {
            if (!jsonInfo.ContainsKey("Title"))
            {
                return BadRequest("Did not have Title key.");
            }

            else if (!jsonInfo.ContainsKey("Name"))
            {
                return BadRequest("Did not have Name key.");
            }

            else if (!jsonInfo.ContainsKey("Rate"))
            {
                return BadRequest("Did not have Rate key.");
            }

            else if (!jsonInfo.ContainsKey("Content"))
            {
                return BadRequest("Did not have Content key.");
            }

            var review = new Review();
            review.Title = jsonInfo["Title"];
            review.reviewerName = jsonInfo["Name"];
            review.reviewRate = double.Parse(jsonInfo["Rate"]);
            review.reviewDate = DateTime.Today;
            review.reviewContent = jsonInfo["Content"];

            await Create(review);

            return Ok(review);
        }
    }
}
