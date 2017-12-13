using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPlan.Data;
using ProjectPlan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace ProjectPlan.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public int counter = 0;

        /// <summary>
        /// Initializes the Controller class assigning it to the relevant userManager and Database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public GroupsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// Displays the index for the view
        /// </summary>
        /// <returns>
        /// The Post context including the assigned Comment system
        /// </returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Group.Include(g => g.Comment).ToListAsync());
        }


        [HttpGet("Details/{id}")]
        [Authorize]
        /// <summary>
        /// Displays the details for the view and increments the post views counter, saves the post in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Post context including the assigned Comment system
        /// </returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group group = await _context.Group.SingleOrDefaultAsync(m => m.Id == id);

            if (@group == null)
            {
                return NotFound();
            }

            GroupDetailsModel viewModel = await GetGroupDetails(group);

            group.viewed++;
            _context.Update(group);
            await _context.SaveChangesAsync();

            return View(await _context.Group.Include(g => g.Comment).ToListAsync());
        }

        [Authorize]
        /// <summary>
        /// Assigns the viewModel to a contact and assigns a group to the contact
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>
        /// The viewModel 
        /// </returns>
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

        /// <summary>
        /// Creates a viewModel for the Posts and adds the posts to the contact list
        /// </summary>
        /// <param name="group"></param>
        /// <returns>
        /// The viewModel
        /// </returns>
        private async Task<GroupDetailsModel> GetGroupDetails(Group group)
        {
            GroupDetailsModel viewModel = new GroupDetailsModel();

            viewModel.Group = group;

            List<Contact> contacts = await _context.Contact.Where(x => x.MyGroup == group).ToListAsync();

            viewModel.Contacts = contacts;

            return viewModel;
        }

        
        [Authorize]
        /// <summary>
        /// Returns the view when creating a post
        /// </summary>
        /// <returns>
        /// Returns the view
        /// </returns>
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        /// <summary>
        /// Stores the currently logged in user and assigns it to the post's author. 
        /// Initializes view count and stores in database.
        /// </summary>
        /// <param name="@group"></param>
        /// <returns>
        /// View(@group)
        /// </returns>
        public async Task<IActionResult> Create([Bind("Title, Description, Id")] Group @group)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);

                group.ApplicationUser = await _userManager.FindByIdAsync(userId);
                group.viewed = 0;

                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        /// <summary>
        /// Stores the commentId as the GroupId passed in. Adds the comment to the database and saves
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns>
        /// View(comment)
        /// </returns>
        public async Task<IActionResult> Comment(int? id, [Bind("Body")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var post = await _context.Group.SingleOrDefaultAsync(m => m.Id == id);
                comment.MyGroup = post;

                _context.Add(comment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        /// <summary>
        /// Returns the view when commenting on a post
        /// </summary>
        /// <returns>
        /// Returns the view
        /// </returns>
        public IActionResult Comment()
        {
            return View();
        }

        [Authorize]
        /// <summary>
        /// Holds the post information
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// View(@group)
        /// </returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group.SingleOrDefaultAsync(m => m.Id == id);
            var groups = await _context.Group.Include(g => g.ApplicationUser).SingleOrDefaultAsync(m => m.Id == id);
            var userId = _userManager.GetUserId(HttpContext.User);

            if (@group == null)
            {
                return NotFound();
            }

            if (userId == groups.ApplicationUser.Id)
            {
                return View(@group);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
                
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        /// <summary>
        /// Edits the post details and updates the database accordingly
        /// </summary>
        /// <param name="id"></param>
        /// <param name="group"></param>
        /// <returns>
        /// RedirectToAction(nameof(Index)) or NotFound() error or View(group)
        /// </returns>
        public async Task<IActionResult> Edit(int id, [Bind("Title, Description, Id, ApplicationUser")] Group group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                _context.Update(group);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        [Authorize]
        /// <summary>
        /// Stores in the data for a post including the author. 
        /// Post will be deleted if the author matches the logged in user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// View(group)
        /// </returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .Include(g => g.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);

            var userId = _userManager.GetUserId(HttpContext.User);

            if (@group == null)
            {
                return NotFound();
            }

            if (userId == group.ApplicationUser.Id)
            {
                return View(@group);
            }

            return View(group);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        /// <summary>
        /// The post information will be stored including the author. 
        /// The comment information will be stored when the comment is the same as the passed in postId. 
        /// Comments which match are removed and if the logged in User matches author then post is removed. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// RedirectToAction(nameof(Index))
        /// </returns>
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var group = await _context.Group.Include(g => g.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);

            var userId = _userManager.GetUserId(HttpContext.User);
            var comment = _context.Comment.Where(a => a.MyGroup.Id == id);
            
            _context.Comment.RemoveRange(comment);
            _context.SaveChanges();

            if(userId == group.ApplicationUser.Id)
            {
                _context.Group.Remove(group);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Returns all of the posts which are valid
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// _context.Group.Any(e => e.Id == id)
        /// </returns>
        private bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.Id == id);
        }
    }
}