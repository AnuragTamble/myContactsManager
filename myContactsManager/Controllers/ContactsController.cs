using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactManagerData;
using ContactModels;
using Microsoft.AspNetCore.Authorization;

namespace myContactsManager.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly MyContactDbContext _context;

        public ContactsController(MyContactDbContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var myContactDbContext = _context.Contacts.Include(c => c.State);
            return View(await myContactDbContext.ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacts == null)
            {
                return NotFound();
            }

            return View(contacts);
        }
        private async Task UpdateCreate(Contacts contact) 
        {
            ModelState.Clear();
            var states = _context.States.SingleOrDefault(x => x.Id == contact.StateId);
            contact.State = states;
            TryValidateModel(contact);
        } 
        // GET: Contacts/Create
        public IActionResult Create()
        {
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Abbrevation");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,email,PhonePrimary,PhoneSecondary,Birthday,StreetAdd1,StreetAdd2,city,StateId,Zip,userId")] Contacts contacts)
        {
            UpdateCreate(contacts);
            if (ModelState.IsValid)
            {
               await _context.Contacts.AddAsync(contacts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Abbrevation", contacts.StateId);
            return View(contacts);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts == null)
            {
                return NotFound();
            }
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Abbrevation", contacts.StateId);
            return View(contacts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FirstName,LastName,email,PhonePrimary,PhoneSecondary,Birthday,StreetAdd1,StreetAdd2,city,StateId,Zip,userId")] Contacts contacts)
        {
            if (id != contacts.Id)
            {
                return NotFound();
            }
             UpdateCreate(contacts);

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Contacts.Update(contacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactsExists(contacts.Id))
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
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Abbrevation", contacts.StateId);
            return View(contacts);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacts == null)
            {
                return NotFound();
            }

            return View(contacts);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts != null)
            {
                _context.Contacts.Remove(contacts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactsExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
