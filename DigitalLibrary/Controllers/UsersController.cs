using DigitalLibrary.Models;
using DigitalLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DigitalLibrary.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public UsersController(IUserRepository userRepository,
            UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAll();
            return View(users);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User appUser)
        {
            if (!ModelState.IsValid) return View(appUser);

            try
            {
                var user = new IdentityUser
                {
                    UserName = appUser.Email,
                    Email = appUser.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, appUser.Password);

                if (!result.Succeeded)
                {
                    return View(appUser);
                }

                if (appUser.Administrator)
                {
                    var newUserIdentity = await _userManager.FindByEmailAsync(appUser.Email);
                    result = await _userManager.AddToRoleAsync(newUserIdentity, "Administrator");
                }

                await _userRepository.Add(appUser);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }           
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var appUser = await _userRepository.GetById(id);

            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User appUser)
        {
            if (!ModelState.IsValid) return View(appUser);

            try
            {

                var oldAppUser = await _userRepository.FirstOrDefault(x => x.Id == appUser.Id);
                
                var user = await _userManager.FindByEmailAsync(oldAppUser.Email);
                var roles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, roles);

                user.Email = appUser.Email;
                user.UserName = appUser.Email;
                user.NormalizedEmail = appUser.Email.ToUpper();
                user.NormalizedUserName = appUser.Email.ToUpper();

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                await _userManager.ResetPasswordAsync(user, token, appUser.Password);
                await _userManager.AddToRoleAsync(user, "Administrator");

                await _userManager.UpdateAsync(user);
                await _userRepository.Update(appUser);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public async Task<IActionResult> Delete(Guid id)
        {

            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _userRepository.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
