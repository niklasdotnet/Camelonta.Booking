using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Camelonta.Backend.Models;

namespace CamelontaBooking.Models.ViewModels
{
    public class ReservationViewModel
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string PersonalNumber { get; set; }
        public long HousingId { get; set; }
        public string HousingImage { get; set; }
        public string HousingName { get; set; }

    }
}