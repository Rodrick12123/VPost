using Blog.Data;
using Blog.Models.Domain;
using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;
        public AdminTagsController(ITagRepository tagRepository) {
            this.tagRepository = tagRepository;
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
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            //create model using form data
            var tag = new Tag
            {
                Name = request.Name,
                DisplayName = request.DisplayName
            };
            //include check if Name already exsist

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        //Same as referencing the fn name
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //Use the dbContext to read the tags

            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);
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

            var exsistingTag = await tagRepository.UpdateAsync(tag);
            if(exsistingTag != null)
            {

            }
            else
            {

            }
            
            return RedirectToAction("Edit", new { id = request.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest request)
        {
            var tag = await tagRepository.DeleteAsync(request.Id);
            
            if(tag != null)
            {

                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = request.Id });
        }
    }
}
