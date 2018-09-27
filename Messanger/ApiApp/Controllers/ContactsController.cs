using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiApp.Models;
using ApiApp.Data;
using Newtonsoft.Json.Linq;

namespace ApiApp.Controllers
{
    public class ContactsController : Controller {
        private readonly DataContext _context;

        public ContactsController(DataContext context)
        {
            _context = context;
        }


        // Index page
        [Route("Contacts/Index")]
        [EnableCors("AllowAll")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// Get all contacts with Ajax
        [Route("Contacts/GetContacts")]
        [EnableCors("AllowAll")]
        [HttpGet]
        public IActionResult GetContacts(string User)
        {
            var model = _context.UserContacts.Where(el => (el.User == User)).SingleOrDefault();
            if (model == null)
            {
                Contact userContacts = new Contact(User, new JObject());
                _context.Add(userContacts);
                _context.SaveChanges();
                model = _context.UserContacts.SingleOrDefault(el => (el.User == User));
            }
            return View(model);
        }

        //create contact
        [Route("Contacts/Create")]
        [EnableCors("AllowAll")]
        [HttpPost]
        public async Task<IActionResult> Create(string User)
        {
            Contact userContacts = new Contact(User, new JObject());
            if (ModelState.IsValid)
            {
                _context.Add(userContacts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        //add contact
        [Route("Contacts/Add")]
        [EnableCors("AllowAll")]
        [HttpPost]
        public async Task<IActionResult> Add(string User, string username)
        {
            Contact userContacts = _context.UserContacts.SingleOrDefault(el => (el.User == User));

            JObject currentContacts = userContacts.contacts;
            if (currentContacts["username"] == null)
            {
                currentContacts.Add(username, username);
            }

            userContacts.contacts = currentContacts;

            _context.Attach(userContacts);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //delete user from contacts
        [Route("Contacts/Delete")]
        [EnableCors("AllowAll")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string User, string username)
        {
            Contact userContacts = await _context.UserContacts.SingleOrDefaultAsync(m => m.User == User);

            JObject currentContacts = userContacts.contacts;
            currentContacts.Remove(username);

            userContacts.contacts = currentContacts;


            _context.Attach(userContacts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactsExists(int id)
        {
            return _context.Message.Any(e => e.ID == id);
        }
    }
}
