using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EPAXSteamsContacts.Models;
using EPAXSteamsContacts.Enums;
using ClosedXML.Excel;

namespace EPAXSteamsContacts.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactDbContext _context;

        public ContactsController(ContactDbContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index(string searchContact)
        {
            if (!string.IsNullOrEmpty(searchContact))
            {
                var contacts = from contact in _context.Contacts select contact;

                if (string.Equals(searchContact, "subscriber", StringComparison.OrdinalIgnoreCase))
                {
                    contacts = from c in contacts where c.LifecycleStage.Contains("0") select c;
                }
                else if (string.Equals(searchContact, "lead", StringComparison.OrdinalIgnoreCase))
                {
                    contacts = from c in contacts where c.LifecycleStage.Contains("1") select c;
                }
                else if (string.Equals(searchContact, "opportunity", StringComparison.OrdinalIgnoreCase))
                {
                    contacts = from c in contacts where c.LifecycleStage.Contains("2") select c;
                }
                else if (string.Equals(searchContact, "customer", StringComparison.OrdinalIgnoreCase))
                {
                    contacts = from c in contacts where c.LifecycleStage.Contains("3") select c;
                }
                else if (!String.IsNullOrEmpty(searchContact))
                {

                    contacts = contacts.Where(c => c.FirstName.Contains(searchContact) || c.LastName.Contains(searchContact) || c.EmailAddress.Contains(searchContact)
                                                   || c.PhoneNumber.Contains(searchContact) || c.JobTitle.Contains(searchContact));
                }

                return View(await contacts.ToListAsync());
            }
            else
            {
                return _context.Contacts != null ?
                        View(await _context.Contacts.ToListAsync()) :
                        Problem("Entity set 'ContactDbContext.Contacts'  is null.");
            }            
        }

        // GET: Contacts/Create
        public IActionResult AddorEdit(int id=0)
        {
            // create new instance of Contact model
            if (id == 0)
            {
                return View(new Contact());
            }
            else
            {
                return View(_context.Contacts.Find(id));
            }
        }

        // POST: Contacts/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("ContactID,FirstName,LastName,EmailAddress,PhoneNumber,JobTitle,LifecycleStage")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (contact.ContactID == 0)
                {
                    _context.Add(contact);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
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


        // Export table to excel
        public IActionResult ExportToExcel()
        {
            List<Contact> contacts = (from contact in _context.Contacts select contact).ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("contacts");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ContactID";
                worksheet.Cell(currentRow, 2).Value = "Contact Name";
                worksheet.Cell(currentRow, 3).Value = "Phone Number";
                worksheet.Cell(currentRow, 4).Value = "Job Title";
                worksheet.Cell(currentRow, 5).Value = "Lifecycle Stage";

                foreach (var contact in contacts)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = contact.ContactID;
                    worksheet.Cell(currentRow, 2).Value = contact.FullName;
                    worksheet.Cell(currentRow, 3).Value = contact.PhoneNumber;
                    worksheet.Cell(currentRow, 4).Value = contact.JobTitle;
                    worksheet.Cell(currentRow, 5).Value = contact.LifecycleStage;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "contacts.xlsx"
                        );
                }
            }
        }


         
    }
}
