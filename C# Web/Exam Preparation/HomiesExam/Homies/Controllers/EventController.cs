using Homies.Data;
using Homies.Data.DataModels;
using Homies.Models.EventViewModels;
using Homies.Models.TypeView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Globalization;
using System.Security.Claims;
using static Homies.Data.Constants.DataConstants;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext context;
        public EventController(HomiesDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<EventViewModel> models = await context.Events
                .AsNoTracking()
                .Select(e => new EventViewModel(
                    e.Id,
                    e.Name,
                    e.Start,
                    e.Type.Name,
                    e.Organiser.UserName
                    ))
                .ToListAsync();

            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            Event? model = await context.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            string userId = GetUser();

            if (!model.EventsParticipants.Any(ep => ep.HelperId == userId))
            {
                model.EventsParticipants.Add(new EventParticipant()
                {
                    EventId = id,
                    HelperId = userId
                });
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUser();

            var models = await context.EventsParticipants
                .Where(ep => ep.HelperId == userId)
                .AsNoTracking()
                .Select(ep => new EventViewModel(
                    ep.EventId,
                    ep.Event.Name,
                    ep.Event.Start,
                    ep.Event.Type.Name,
                    ep.Event.Organiser.UserName
                    ))
                .ToListAsync();

            return View(models);
        }

        public async Task<IActionResult> Leave(int id)
        {
            //string userId = GetUser();

            //var model = await context.Events
            //    .Where(e => e.Id == id
            //    && e.EventsParticipants.Any(ep => ep.HelperId == userId))
            //    .FirstOrDefaultAsync();

            //if(model == null)
            //{
            //    return BadRequest();
            //}

            //var epToRemove = context.EventsParticipants
            //    .FirstOrDefault(ep => ep.EventId == model.Id);

            //context.EventsParticipants.Remove(epToRemove);
            //context.SaveChangesAsync();

            var model = await context.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            string userId = GetUser();

            var ep = model.EventsParticipants
                .FirstOrDefault(ep => ep.HelperId == userId);

            if (ep == null)
            {
                return BadRequest();
            }

            model.EventsParticipants.Remove(ep);
            await context.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            EventAddModel model = new EventAddModel();

            model.Types = await context.Types
                .Select(t => new TypeViewModel()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventAddModel model)
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.Start,
                DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(model.Start, $"Invalid date! Format must be: {DateTimeFormat}");
            }
            if (!DateTime.TryParseExact(
                model.End,
                DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out end))
            {
                ModelState.AddModelError(model.End, $"Invalid date! Format must be: {DateTimeFormat}");
            }
            string userId = GetUser();

            if (!ModelState.IsValid)
            {
                model.Types = await context.Types
                    .Select(t => new TypeViewModel()
                    {
                        Id = t.Id,
                        Name = t.Name
                    })
                    .ToListAsync();

                return View(model);
            }

            var modelToCreate = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                OrganiserId = userId,
                CreatedOn = DateTime.Now,
                Start = start,
                End = end,
                TypeId = model.TypeId
            };

            await context.Events.AddAsync(modelToCreate);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var e = context.Events
                .Find(id);

            if(e == null)
            {
                return BadRequest();
            }

            string userId = GetUser();

            if(e.OrganiserId != userId)
            {
                return Unauthorized();
            }

            EventAddModel model = new EventAddModel()
            {
                Name = e.Name,
                Description = e.Description,
                Start = e.Start.ToString(DateTimeFormat),
                End = e.End.ToString(DateTimeFormat),
                TypeId = e.TypeId,
            };
            
            model.Types = await context.Types
                .Select(t => new TypeViewModel()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventAddModel model, int id)
        {
            var e = context.Events
                .Find(id);

            if (e == null)
            {
                return BadRequest();
            }
            string userId = GetUser();

            if (e.OrganiserId != userId)
            {
                return Unauthorized();
            }

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.Start,
                DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(model.Start, $"Invalid date! Format must be: {DateTimeFormat}");
            }
            if (!DateTime.TryParseExact(
                model.End,
                DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out end))
            {
                ModelState.AddModelError(model.End, $"Invalid date! Format must be: {DateTimeFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Types = await context.Types
                .Select(t => new TypeViewModel()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
            }

            e.Start = start;
            e.End = end;
            e.Description = model.Description;
            e.Name = model.Name;
            e.TypeId = model.TypeId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            EventDetailsModel? model = await context.Events
                .Where(e => e.Id == id)
                .AsNoTracking()
                .Select(e => new EventDetailsModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Start = e.Start.ToString(DateTimeFormat),
                    End = e.End.ToString(DateTimeFormat),
                    Organiser = e.Organiser.UserName,
                    CreatedOn = e.CreatedOn.ToString(DateTimeFormat),
                    Type = e.Type.Name
                })
                .FirstOrDefaultAsync();

            if(model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        private string GetUser()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

    }
}
