using HouseBooking.Backend.Repository;
using System.Web.Mvc;

namespace HouseBooking.UI.MVC.Controllers
{
    public class HouseController : Controller
    {
        private readonly IHousingRepository _repository;

        public HouseController(IHousingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        [Route("House/")]
        [Route("House/Index")]
        public ActionResult Index()
        {
            return View(_repository.GetHousings());
        }

    }
}