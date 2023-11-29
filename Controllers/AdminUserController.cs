using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUserController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAllUsers();
            var viewUsersModel = new ViewUsers();
            viewUsersModel.Users = new List<User>();
            foreach (var user in users)
            {
                viewUsersModel.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    UserName = user.UserName,
                    Email = user.Email
                });
            }
            return View(viewUsersModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(ViewUsers request)
        {
            var identityUser = new IdentityUser
            {

                UserName = request.UserName,
                Email = request.Email
            };
            var result = await userManager.CreateAsync(identityUser, request.Password);

            if (result != null)
            {
                if (result.Succeeded)
                {
                    var roles = new List<string> { "User" };
                    if (request.AdminCheckbox)
                    {
                        roles.Add("Admin");
                    }
                    result = await userManager.AddToRolesAsync(identityUser, roles);
                    if (result != null && result.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUser");
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if(user != null)
            {
                var deleteUser = await userManager.DeleteAsync(user);
                if(deleteUser != null && deleteUser.Succeeded)
                {
                    return RedirectToAction("List");
                }
            }
            return View();
        }

    }
}
