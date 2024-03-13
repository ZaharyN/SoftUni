using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.DataModels;
using SeminarHub.Models;
using System.Globalization;
using System.Security.Claims;
using static SeminarHub.Data.Constants.DataConstants;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext context;

        public SeminarController(SeminarHubDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await context.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            SeminarAddViewModel model = new SeminarAddViewModel()
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await context.Categories
                    .Select(c => new CategoryViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name,
                    })
                    .ToListAsync();

                return View(model);
            }

            if (!DateTime.TryParseExact(model.DateAndTime,
                SeminarDateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime dateTime))
            {
                throw new InvalidOperationException("Invalid date or time format");
            }

            Seminar seminar = new()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                OrganizerId = GetUser(),
                DateAndTime = dateTime,
                Duration = model.Duration.Value,
                Categoryid = model.CategoryId,
            };

            await context.Seminars.AddAsync(seminar);
            await context.SaveChangesAsync();

            return RedirectToAction("All", "Seminar");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<SeminarAllViewModel> seminars = await context.Seminars
                .Select(s => new SeminarAllViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Category = s.Category.Name,
                    DateAndTime = s.DateAndTime.ToString(SeminarDateTimeFormat),
                    Organizer = s.Organizer.UserName
                })
                .ToListAsync();

            return View(seminars);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int Id)
        {
            Seminar? seminar = await context.Seminars
                .Where(s => s.Id == Id)
                .FirstOrDefaultAsync();

            if (seminar == null)
            {
                return RedirectToAction(nameof(All));
            }

            string userId = GetUser();

            if (await context.SeminarsParticipants
                .AnyAsync(sp => sp.ParticipantId == userId
                        && sp.SeminarId == Id))
            {
                return RedirectToAction(nameof(All));
            }

            await context.SeminarsParticipants.AddAsync(new SeminarParticipant()
            {
                SeminarId = Id,
                ParticipantId = userId
            });

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUser();

            IEnumerable<SeminarJoinedViewModel> seminars = await context.SeminarsParticipants
                .Where(sp => sp.ParticipantId == userId)
                .AsNoTracking()
                .Select(sp => new SeminarJoinedViewModel()
                {
                    Id = sp.SeminarId,
                    Topic = sp.Seminar.Topic,
                    Lecturer = sp.Seminar.Lecturer,
                    DateAndTime = sp.Seminar.DateAndTime.ToString(SeminarDateTimeFormat),
                    Organizer = sp.Seminar.Organizer.UserName,
                    Category = sp.Seminar.Category.Name
                })
                .ToListAsync();

            return View(seminars);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categories = await context.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            SeminarEditFormViewModel? seminar = await context.Seminars
                .Where(s => s.Id == id)
                .Select(s => new SeminarEditFormViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Details = s.Details,
                    DateAndTime = s.DateAndTime.ToString(SeminarDateTimeFormat),
                    Duration = s.Duration,
                    CategoryId = s.Category.Id,
                    Categories = categories
                })
                .FirstOrDefaultAsync();

            if (seminar == null)
            {
                return BadRequest();
            }


            return View(seminar);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeminarEditFormViewModel editModel)
        {
            Seminar? seminar = await context.Seminars
                .FindAsync(editModel.Id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (!DateTime.TryParseExact(editModel.DateAndTime,
                SeminarDateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime dateTime))
            {
                throw new InvalidOperationException("Invalid date or time format");
            }

            if (!ModelState.IsValid)
            {
                editModel.Categories = await context.Categories
                    .Select(c => new CategoryViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name,
                    })
                    .ToListAsync();

                return View(editModel);
            }

            seminar.Topic = editModel.Topic;
            seminar.Lecturer = editModel.Lecturer;
            seminar.Details = editModel.Details;
            seminar.DateAndTime = dateTime;
            seminar.Duration = editModel.Duration;
            seminar.Categoryid = editModel.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            Seminar? seminarToDelete = await context.Seminars
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (seminarToDelete == null)
            {
                return BadRequest();
            }

            string userId = GetUser();

            SeminarParticipant? seminarParticipant = await context.SeminarsParticipants
                .Where(sp => sp.SeminarId == id && sp.ParticipantId == userId)
                .FirstOrDefaultAsync();

            if(seminarParticipant == null)
            {
                return BadRequest();
            }

            context.SeminarsParticipants.Remove(seminarParticipant);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            SeminarDetailsViewModel? seminarToView = await context.Seminars
                .Where(s => s.Id == id)
                .AsNoTracking()
                .Select(s => new SeminarDetailsViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    DateAndTime = s.DateAndTime.ToString(),
                    Duration = s.Duration,
                    Lecturer = s.Lecturer,
                    Category = s.Category.Name,
                    Details = s.Details,
                    Organizer = s.Organizer.UserName,
                    OrganizerId = s.OrganizerId,
                })
                .FirstOrDefaultAsync();

            if(seminarToView == null)
            {
                return RedirectToAction("All", "Seminar");
            }

            return View(seminarToView);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = GetUser();

            Seminar? seminarToDelete = context.Seminars
                .FirstOrDefault(s => s.Id == id);

            if(seminarToDelete.OrganizerId != userId)
            {
                return Unauthorized();
            }

            SeminarDeleteViewModel? model = await context.Seminars
                .Where(s => s.Id == id)
                .Select(s => new SeminarDeleteViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    DateAndTime = s.DateAndTime.ToString()
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(SeminarDeleteViewModel model)
        {
            if(model == null)
            {
                return RedirectToAction(nameof(All));
            }

            Seminar? seminarToDelete = await context.Seminars.FindAsync(model.Id);

            context.Seminars.Remove(seminarToDelete);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private string GetUser()
        {
            string userId = string.Empty;

            if (User != null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return userId;
        }
    }
}
