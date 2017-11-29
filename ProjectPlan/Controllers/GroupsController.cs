using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectPlan.Data;
using ProjectPlan.Models;

namespace ProjectPlan.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            return View(await _context.Group.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group group = await _context.Group.SingleOrDefaultAsync(m => m.Id == id);

            if (group == null)
            {
                return NotFound();
            }

            GroupDetailsModel viewModel = await GetGroupDetails(group);

            return View(viewModel);
        }

        public async Task<IActionResult> Details([Bind("Id, FirstName")]
            GroupDetailsModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact();

                contact.Id = viewModel.GroupID;
                contact.FirstName = viewModel.FirstName;
                contact.LastName = viewModel.LastName;

                Group group = await _context.Group.SingleOrDefaultAsync(m => m.Id == viewModel.GroupID);

                if (group == null)
                {
                    return NotFound();
                }

                contact.MyGroup = group;
                _context.Contact.Add(contact);
                await _context.SaveChangesAsync();
            }
            return View(viewModel);
        }

        private async Task<GroupDetailsModel> GetGroupDetails(Group group)
        {
            GroupDetailsModel viewModel = new GroupDetailsModel();

            viewModel.Group = group;

            List<Contact> contacts = await _context.Contact.Where(x => x.MyGroup == group).ToListAsync();

            viewModel.Contacts = contacts;

            return viewModel;
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, Description, Id")] Group @group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group.SingleOrDefaultAsync(m => m.Id == id);
            
            if (@group == null)
            {
                return NotFound();
            }
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title, Description, Id")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
      
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.Group.SingleOrDefaultAsync(m => m.Id == id);
            _context.Group.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.Id == id);
        }
    }
}
