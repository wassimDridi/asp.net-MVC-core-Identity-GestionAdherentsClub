using GestionAdherentsClub.Models.Repositories;
using GestionAdherentsClub.Models;
using GestionAdherentsClub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionAdherentsClub.Controllers
{
    public class AccountController : Controller
    {
		// GET: AccountController
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
        private readonly IClubEventRepository<ClubEvent> clubEventRepository;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager , IClubEventRepository<ClubEvent> clubEventRepository)
		{
			this.clubEventRepository = clubEventRepository;

            this.userManager = userManager;
			this.signInManager = signInManager;
		}

        [HttpGet]
        public async Task<IActionResult> ListEvents()
        {
			var events = clubEventRepository.GetAll();
            return View(events);
        }




        [HttpGet]
		public IActionResult Register()
		{

			return View();
		}

		[HttpPost]	
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Copy data from RegisterViewModel to IdentityUser
				var user = new IdentityUser()
				{
					UserName = model.Email,
					Email = model.Email
				};
				// Store user data in AspNetUsers database table
				var result = await userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user, isPersistent: false);
					await userManager.AddToRoleAsync(user, "User");
					//var allUsers = await userManager.
					return RedirectToAction("index", "home");
				}
				// If there are any errors, add them to the ModelState object
				// which will be displayed by the validation summary tag helper
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("index", "Home");
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
		{
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(returnUrl))
					{
						return LocalRedirect(returnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
			}
			return View(model);
		}

		[AllowAnonymous]
		public IActionResult AccessDenied()
		{
			return View();
		}

	}
}
