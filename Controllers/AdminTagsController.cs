using Blog.Data;
using Blog.Models.Domain;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class AdminTagsController : Controller
    {
        private BlogDbContext blogDbContext;
        public AdminTagsController(BlogDbContext blogContext) {
            this.blogDbContext = blogContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest request)
        {
            //create model using form data
            var tag = new Tag
            {
                Name = request.Name,
                DisplayName = request.DisplayName
            };
            //include check if Name already exsist

            //add the model instance to the tag table in the DB
            await blogDbContext.Tags.AddAsync(tag);

            //save changes to db
            await blogDbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        //Same as referencing the fn name
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //Use the dbContext to read the tags
            var tags = await blogDbContext.Tags.ToListAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await blogDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
            if(tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest request)
        {
            var tag = new Tag
            {
                Id = request.Id,
                Name = request.Name,
                DisplayName = request.DisplayName
            };

            var exsistingTag = await blogDbContext.Tags.FindAsync(tag.Id);
            
            if(exsistingTag != null)
            {
                //add a check to see if name exsists
                exsistingTag.Name = tag.Name;
                exsistingTag.DisplayName = tag.DisplayName;

                await blogDbContext.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = request.Id });

            }
            return RedirectToAction("Edit", new { id = request.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest request)
        {
            var tag = await blogDbContext.Tags.FindAsync(request.Id);
            if(tag != null)
            {
                blogDbContext.Remove(tag);
                await blogDbContext.SaveChangesAsync();

                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = request.Id });
        }
    }
}
