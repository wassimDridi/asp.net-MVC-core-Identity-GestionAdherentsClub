using GestionAdherentsClub.Models.Repositories;
using GestionAdherentsClub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Identity;

namespace GestionAdherentsClub.Controllers
{
	[Authorize(Roles = "Admin,Manager,User")]
	public class AdherentController : Controller
    {
        readonly IAdherentRepository<Adherent> adherentRepository;
        readonly IClubRepository<Club> clubRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly UserManager<IdentityUser> userManager;
        //injection de dépendance
        public AdherentController(UserManager<IdentityUser> userManager , IAdherentRepository<Adherent> adherentRepository, IClubRepository<Club> clubRepository , IWebHostEnvironment hostingEnvironment)
        {
            this.userManager = userManager;
            this.adherentRepository = adherentRepository;
            this.clubRepository = clubRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: AdherentController
        [AllowAnonymous]

        public ActionResult Index()
        {
            ViewBag.ClubID = new SelectList(clubRepository.GetAll(), "ClubID", "ClubName");
            var adherents = adherentRepository.GetAll();
            return View(adherents);
        }
        
        




        // GET: AdherentController/Details/5
        public ActionResult Details(int id)
        {
            var adherent = adherentRepository.GetById(id);
            return View(adherent);
        }

        // GET: AdherentController/Create
        public ActionResult Create()
        {
            ViewBag.ClubID = new SelectList(clubRepository.GetAll(), "ClubID", "ClubName");
            return View();
        }

        // POST: AdherentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Create(Adherent adherent, IFormFile image)
        {
            try
            {
                ViewBag.ClubID = new SelectList(clubRepository.GetAll(), "ClubID", "ClubName");
                var user = await userManager.GetUserAsync(User);
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

                adherent.img = uniqueFileName;
                adherent.UserID = user.Id ;
                adherentRepository.Add(adherent);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdherentController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ClubID = new SelectList(clubRepository.GetAll(), "ClubID", "ClubName");
            var adherent = adherentRepository.GetById(id);
            return View(adherent);
            
        }

        // POST: AdherentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Adherent adherent , IFormFile image)
        {
            try
            {
                ViewBag.ClubID = new SelectList(clubRepository.GetAll(), "ClubID", "ClubName");
                Adherent a = adherentRepository .GetById(id);
                if (image == null || image.Length == 0)
                {
                    adherent.img = a.img;

                }
                else
                {

                    string oldPosterPath = Path.Combine(hostingEnvironment.WebRootPath, "images", a.img);
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

                    adherent.img = uniqueFileName;
                }

                adherentRepository.Edit(adherent);
                return RedirectToAction(nameof(Index));

                
                
                
            }
            catch
            {
                return View();
            }
        }

        // GET: AdherentController/Delete/5
        public ActionResult Delete(int id)
        {
            var adherent = adherentRepository.GetById(id);
            return View(adherent);
        }

        // POST: AdherentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Adherent adherent)
        {
            try
            {
                adherentRepository.Delete(adherent);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string name, int? adherentid)
        {
            var result = adherentRepository.GetAll();
            if (!string.IsNullOrEmpty(name))
                result = adherentRepository.FindByName(name);
            else
            if (adherentid != null)
                result = adherentRepository.GetAdherentsByClubID(adherentid);
            ViewBag.ClubID = new SelectList(clubRepository.GetAll(), "ClubID", "ClubName");
            return View("Index", result);
        }
    }
}
