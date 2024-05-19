using GestionAdherentsClub.Models;
using GestionAdherentsClub.Models.Repositories;
using GestionAdherentsClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GestionAdherentsClub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IAdherentRepository<Adherent> adherentRepository;
        readonly IClubRepository<Club> clubRepository;


        public HomeController(ILogger<HomeController> logger, IClubRepository<Club> clubRepository , IAdherentRepository<Adherent> adherentRepository)
        {
            this.clubRepository = clubRepository;
            this.adherentRepository = adherentRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Retrieve all clubs
            var clubs = clubRepository.GetAll();

            // Create a list to hold ClubAdherentsViewModel objects
            var clubAdherentsList = new List<ClubAdherentsViewModel>();

            // Iterate through each club
            foreach (var club in clubs)
            {
                // Retrieve adherents for the current club
                var adherents = adherentRepository.GetAdherentsByClubID(club.ClubID);

                // Create a ClubAdherentsViewModel object and populate its properties
                var clubAdherentsViewModel = new ClubAdherentsViewModel
                {
                    Club = club,
                    Adherents = adherents.ToList() // Convert to List if necessary
                };

                // Add the ClubAdherentsViewModel object to the list
                clubAdherentsList.Add(clubAdherentsViewModel);
            }

            // Pass the list of ClubAdherentsViewModel objects to the view
            return View(clubAdherentsList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}