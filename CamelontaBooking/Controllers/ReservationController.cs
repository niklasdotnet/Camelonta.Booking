using System;
using Camelonta.Backend.Repository;
using System.Web.Mvc;
using Camelonta.Backend.Models;
using CamelontaBooking.Models.ViewModels;

namespace CamelontaBooking.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IHousingRepository _housingRepository;

        public ReservationController(IReservationRepository reservationRepository, IHousingRepository housingRepository)
        {
            _reservationRepository = reservationRepository;
            _housingRepository = housingRepository;
        }
        
        [HttpGet]
        [Route("NewReservation")]
        public ActionResult NewReservation(int housingId)
        {
            var housing = _housingRepository.GetHousingById(housingId);

            var viewModel = new ReservationViewModel
            {
                HousingId = housing.Id,
                HousingName = housing.Name,
                HousingImage = housing.Image,
                DateFrom = DateTime.Now.ToString("yyyy-MM-dd"),
                DateTo = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd")
            };

            // Auto fill for convenience

            return View(viewModel);
        }
        
        [HttpPost]
        [Route("CreateReservation")]
        public ActionResult CreateReservation(ReservationViewModel model)
        {
            var newReservation = new Reservation()
            {
                HousingId = model.HousingId,
                PersonalNumber = model.PersonalNumber,
                DateFrom = DateTime.Parse(model.DateFrom),
                DateTo = DateTime.Parse(model.DateTo)
            };
            var reservationId = _reservationRepository.CreateReservation(newReservation);
            
            return RedirectToAction("Checkout", "Reservation", new { id = reservationId});
            //return View("ViewReservation", reservation);
        }


        [HttpGet]
        [Route("Checkout/{id}")]
        public ActionResult Checkout(long id)
        //public ActionResult ViewReservation(Reservation model)
        {
            var model = _reservationRepository.GetReservation(id);
            return View(model);
        }
        
    }
}