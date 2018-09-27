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
    public class MessagesController : Controller
    {
        private readonly DataContext _context;

        public MessagesController(DataContext context)
        {
            _context = context;
        }
        // Index page
        [Route("Messages/Index")]
        [EnableCors("AllowAll")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // Get all messages with Ajax
        [Route("Messages/GetMessages")]
        [EnableCors("AllowAll")]
        [HttpGet]
        public IActionResult AjaxGetMessages(int FromID, int ToID, int numPage = 0)
        {
            ViewData["Friend"] = ToID;
            return View(_context.Message.Where(el => (el.FromID == ToID && el.ToID == ToID) || (el.FromID == ToID && el.ToID == FromID)).OrderBy(el => el.TimeSent).ToList());
        }

        //Create message
        [Route("Messages/Create")]
        [EnableCors("AllowAll")]
        [HttpPost]
        public async Task<IActionResult> Create(string text, int FromID, int ToID)
        {
            DateTime TimeSent = DateTime.Now;
            Message message = new Message(text, TimeSent, FromID, ToID);
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        // Deletes user from contacts       
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
