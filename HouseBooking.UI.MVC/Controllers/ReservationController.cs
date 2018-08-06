using System;
using HouseBooking.Backend.Repository;
using System.Web.Mvc;
using HouseBooking.Backend.Models;
using HouseBooking.UI.MVC.Models.ViewModels;

namespace HouseBooking.UI.MVC.Controllers
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
        [Route("Reservation/NewReservation")]
        public ActionResult NewReservation(long? housingId)
        {
            if (housingId == null) return RedirectToAction("Index", "House");

            var housing = _housingRepository.GetHousingById((long)housingId);

            var viewModel = new ReservationViewModel
            {
                HousingId = housing.Id,
                HousingName = housing.Name,
                HousingImage = housing.Image,
                DateFrom = DateTime.Now.ToString("yyyy-MM-dd"),
                DateTo = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd")
            };

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

            return RedirectToAction("Checkout", "Reservation", new { id = reservationId });
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

        [HttpGet]
        [Route("Reservation/GetPrice")]
        public decimal GetPrice(long housingId, DateTime dateFrom, DateTime dateTo)
        {
            return _reservationRepository.GetPrice(housingId, dateFrom, dateTo);
        }
    }
}