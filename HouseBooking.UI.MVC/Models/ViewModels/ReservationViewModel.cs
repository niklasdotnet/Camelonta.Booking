namespace HouseBooking.UI.MVC.Models.ViewModels
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