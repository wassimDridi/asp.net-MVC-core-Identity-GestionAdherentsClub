using GestionAdherentsClub.Models.Repositories;
using GestionAdherentsClub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GestionAdherentsClub.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ClubController : Controller
    {
        readonly IClubRepository<Club> clubRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        //injection de dépendance
        public ClubController(IClubRepository<Club> clubRepository , IWebHostEnvironment hostingEnvironment)
        {
            this.clubRepository = clubRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        // GET: ClubController
        [AllowAnonymous]
        public ActionResult Index()
        {
            var clubs = clubRepository.GetAll();
            return View(clubs);
        }

        // GET: ClubController/Details/5
        public ActionResult Details(int id)
        {
            var club = clubRepository.GetById(id);
            return View(club);
        }

        // GET: ClubController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClubController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Club club, IFormFile image)
        {
            try
            {
                if (image == null || image.Length == 0)
                {
                    return RedirectToAction("home");
                }

                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                club.img = uniqueFileName;

                clubRepository.Add(club);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClubController/Edit/5
        public ActionResult Edit(int id)
        {
            var club = clubRepository.GetById(id);
            return View(club);
        }

        // POST: ClubController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Club club , IFormFile image)
        {
            try
            {
                Club c = clubRepository.GetById(id);
                if (image == null || image.Length == 0)
                {
                    club.img = c.img;

                }
                else
                {

                    string oldPosterPath = Path.Combine(hostingEnvironment.WebRootPath, "images", c.img);
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

                    club.img = uniqueFileName;
                }

                clubRepository.Edit(club);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClubController/Delete/5
        public ActionResult Delete(int id)
        {
            var club = clubRepository.GetById(id);
            return View(club);
        }

        // POST: ClubController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Club club)
        {
            try
            {
                clubRepository.Delete(club);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
