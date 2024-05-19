using GestionAdherentsClub.Models;
using GestionAdherentsClub.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionAdherentsClub.Controllers
{
	[Authorize(Roles = "Admin,Manager")]
	public class ClubeventController : Controller
	{
		readonly IClubEventRepository<ClubEvent> clubEventRepository;
		readonly IClubRepository<Club> clubRepository;
		private readonly IWebHostEnvironment hostingEnvironment;

		public ClubeventController(IClubEventRepository<ClubEvent> clubEventRepository , IClubRepository<Club> clubRepository , IWebHostEnvironment hostingEnvironment)
		{
			this.clubEventRepository = clubEventRepository;
			this.hostingEnvironment = hostingEnvironment;
			this.clubRepository = clubRepository;
		}

		// GET: ClubEvent
		[AllowAnonymous]
		public ActionResult Index()
		{
			ViewBag.ClubID = new SelectList(clubRepository.GetAll(), "ClubID", "ClubName");
			var events = clubEventRepository.GetAll();
			Console.WriteLine("---------------------------------------");
           
            return View(events);
		}

		// GET: ClubEvent/Details/5
		public ActionResult Details(int id)
		{
			var clubEvent = clubEventRepository.GetById(id);
			return View(clubEvent);
		}

		// GET: ClubEvent/Create
		public ActionResult Create()
		{
            ViewBag.ClubID = new SelectList(clubRepository.GetAll(), "ClubID", "ClubName");
            return View();
		}

		// POST: ClubEvent/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(ClubEvent clubEvent, IFormFile image)
		{
			try
			{
                ViewBag.ClubID = new SelectList(clubRepository.GetAll(), "ClubID", "ClubName");
                if (image == null || image.Length == 0)
				{
					return RedirectToAction(nameof(Index));
				}

				string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
				string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await image.CopyToAsync(fileStream);
				}

				clubEvent.img = uniqueFileName;

				clubEventRepository.Add(clubEvent);
                Console.WriteLine("---------------------------------------");
                return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: ClubEvent/Edit/5
		public ActionResult Edit(int id)
		{
            ViewBag.ClubID = new SelectList(clubRepository.GetAll(), "ClubID", "ClubName");
            var clubEvent = clubEventRepository.GetById(id);
			return View(clubEvent);
		}

		// POST: ClubEvent/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, ClubEvent clubEvent , IFormFile image)
		{
			try
			{

                ViewBag.ClubID = new SelectList(clubRepository.GetAll(), "ClubID", "ClubName");
                ClubEvent ce = clubEventRepository.GetById(id);
                if (image == null || image.Length == 0)
                {
                    clubEvent.img = ce.img;

                }
                else
                {

                    string oldPosterPath = Path.Combine(hostingEnvironment.WebRootPath, "images", ce.img);
                    if (System.IO.File.Exists(oldPosterPath))
                    {
                        System.IO.File.Delete(oldPosterPath);
                    }
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    clubEvent.img = uniqueFileName;
                }

                clubEventRepository.Edit(clubEvent);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: ClubEvent/Delete/5
		public ActionResult Delete(int id)
		{
			var clubEvent = clubEventRepository.GetById(id);
			return View(clubEvent);
		}

		// POST: ClubEvent/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, ClubEvent clubEvent)
		{
			try
			{
				clubEventRepository.Delete(clubEvent);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
