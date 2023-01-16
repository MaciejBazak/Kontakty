using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Contacts_Assignment.Models;
using Microsoft.AspNetCore.Authorization;

namespace Contacts_Assignment.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactDbContext _context;
        private readonly CategoryDbContext _categoryDbContext;

        public ContactController(ContactDbContext context, CategoryDbContext categoryContext)
        {
            _context = context;
            _categoryDbContext = categoryContext;
        }

        public async Task<IActionResult> ContactsLoggedOut()
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            contactViewModel.Contacts = await _context.Contacts.ToListAsync();

            return View(contactViewModel);
        }


        // GET: Contact
        [Authorize]
        public async Task<IActionResult> Index()
        {
               ContactViewModel contactViewModel = new ContactViewModel();
               contactViewModel.Contacts = await _context.Contacts.ToListAsync();
               contactViewModel.Categories = await _categoryDbContext.Categories.ToListAsync();

            return View(contactViewModel);
        }

        // GET: Contact/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contact/Create
        public async Task<IActionResult> CreateAsync()
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            foreach (var c in await _categoryDbContext.Categories.ToListAsync())
            {
                categories.Add(new SelectListItem { Value = c.Name, Text = c.Name });
            }

            ViewBag.categories = categories;

            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,FirstName,LastName,Email,Password,Category,Subcategory,TelephoneNumber,DoB")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Contact/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            List<SelectListItem> categories = new List<SelectListItem>();

            foreach (var c in await _categoryDbContext.Categories.ToListAsync()) 
            {
                categories.Add(new SelectListItem { Value = c.Name, Text = c.Name });
            }

            ViewBag.categories = categories;


            return View(contact);
        }

        // POST: Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,FirstName,LastName,Email,Password,Category,Subcategory,TelephoneNumber,DoB")] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId))
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
            return View(contact);
        }

        // GET: Contact/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set 'ContactDbContext.Contacts'  is null.");
            }
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
          return _context.Contacts.Any(e => e.ContactId == id);
        }
    }
}
