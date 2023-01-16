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

        // Akcja GET, przekazania widoku gdy osoba nie jest zalogowana. Do widoku przekazywany jest odpowiedni model.
        public async Task<IActionResult> ContactsLoggedOut()
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            contactViewModel.Contacts = await _context.Contacts.ToListAsync();

            return View(contactViewModel);
        }


        // Akcja GET, przekazania widoku gdy osoba jest zalogowana. Widok z wieksza liczba mozliwosci. Do widoku przekazywany jest odpowiedni model.
        [Authorize]
        public async Task<IActionResult> Index()
        {
               ContactViewModel contactViewModel = new ContactViewModel();
               contactViewModel.Contacts = await _context.Contacts.ToListAsync();
               contactViewModel.Categories = await _categoryDbContext.Categories.ToListAsync();

            return View(contactViewModel);
        }

        // Akcja GET, przekazania widoku szczegolow kontaktu. Widok zmienia sie zaleznie od tego czy osoba jest zalogowana czy nie.
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

        // Akcja GET, ktora przenosi do widoku stworzenia nowego kontaktu. W ViewBagu przekazana jest lista kategorii na podstawie, ktorej tworzona jest dropdown list.
        [Authorize]
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

        // Akcja, POST która jeśli model przeszedl walidacje to dodaje nowy kontakt do bazy danych oraz przechodzi do widoku z lista wszystkich kontaktow.
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

        // Akcja GET, która przenosi do widoku edycji kontaktu. 
        [Authorize]
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

        // Akcja, POST która jeśli model przeszedl walidacje to aktualizuje kontakt oraz przechodzi do widoku z lista wszystkich kontaktow.
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

        // Akcja, Get która zwraca widok szczegolowy z zapytaniem czy na pewno chce sie usunac kontakt.
        [Authorize]
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

        // Akcja POST, która usuwa kontakt z bazy danych oraz przeskakuje na widok listy kontaktów.
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
