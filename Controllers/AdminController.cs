using GestionAdherentsClub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionAdherentsClub.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<IdentityUser> _userManager;

		public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}



		// Role ID is passed from the URL to the action
		[HttpGet]
		public async Task<IActionResult> EditRole(string id)
		{
			// Find the role by Role ID
			var role = await _roleManager.FindByIdAsync(id);
			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
				return View("NotFound");
			}
			var model = new EditRoleViewModel
			{
				Id = role.Id,
				RoleName = role.Name,
			};
			// Retrieve all the Users
			foreach (var user in _userManager.Users.ToList())
			{
				// If the user is in this role, add the username to
				// Users property of EditRoleViewModel. This model
				// object is then passed to the view for display
				if (await _userManager.IsInRoleAsync(user, role.Name))
				{
					model.Users.Add(user.UserName);
				}
			}
			return View(model);
		}
		// This action responds to HttpPost and receives EditRoleViewModel
		[HttpPost]
		public async Task<IActionResult> EditRole(EditRoleViewModel model)
		{
			var role = await _roleManager.FindByIdAsync(model.Id);
			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
				return View("ListeRoles");
			}
			else
			{
				role.Name = model.RoleName;
				// Update the Role using UpdateAsync
				var result = await _roleManager.UpdateAsync(role);
				if (result.Succeeded)
				{
					return RedirectToAction("ListeRoles");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}

				return View(model);
			}
		}

		/*------------------------------------------------create role -------------------*/

		// GET: AdminController
		public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				IdentityRole identityRole = new IdentityRole { Name = model.RoleName };
				IdentityResult result = await _roleManager.CreateAsync(identityRole);
				if (result.Succeeded)
				{
					return RedirectToAction("ListeRoles");

				}
				foreach (IdentityError error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(model);
		}

		/* -------------------------------------- listeRole ----------------------------*/
		//Get All Roles
		[HttpGet]
		public IActionResult ListeRoles()
		{
			var roles = _roleManager.Roles;
			return View(roles);
		}

		/*--------------------------------------- delete role ---------------------------*/
		[HttpPost]
		public async Task<IActionResult> DeleteRole(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);
			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
				return View("NotFound");
			}
			else
			{
				var result = await _roleManager.DeleteAsync(role);
				if (result.Succeeded)
				{
					return RedirectToAction("ListeRoles");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				return View("ListeRoles");
			}
		}









		/*--------------------------------- edit user in role ---------------------------*/
		[HttpGet]
		public async Task<IActionResult> EditUsersInRole(string roleId)
		{
			ViewBag.roleId = roleId;
			var role = await _roleManager.FindByIdAsync(roleId);


			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
				return View("NotFound");
			}

			var model = new List<UserRoleViewModel>();
			foreach (var user in _userManager.Users.ToList())
			{
				var userRoleViewModel = new UserRoleViewModel
				{
					UserId = user.Id,
					UserName = user.UserName
				};
				if (await _userManager.IsInRoleAsync(user, role.Name))
				{
					userRoleViewModel.IsSelected = true;
				}
				else
				{
					userRoleViewModel.IsSelected = false;
				}
				model.Add(userRoleViewModel);
			}
			return View(model);
		}


		[HttpPost]
		public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);
			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
				return View("NotFound");
			}
			for (int i = 0; i < model.Count; i++)
			{
				var user = await _userManager.FindByIdAsync(model[i].UserId);
				IdentityResult result = null;
				if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
				{
					result = await _userManager.AddToRoleAsync(user, role.Name);
				}
				else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
				{
					result = await _userManager.RemoveFromRoleAsync(user, role.Name);
				}
				else
				{
					continue;
				}
				if (result.Succeeded)
				{
					if (i < (model.Count - 1))
						continue;
					else
						return RedirectToAction("EditRole", new { Id = roleId });
				}
			}
			return RedirectToAction("EditRole", new { Id = roleId });
		}


		/*------------------------------ Access denied --------------------------------*/
		[AllowAnonymous]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
