using Camelonta.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Camelonta.Backend.Models;
using CamelontaBooking.Models;

namespace CamelontaBooking.Controllers
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