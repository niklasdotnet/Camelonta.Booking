using System;

namespace Camelonta.Backend.Models
{
    public class Reservation
    {
        public long Id { get; set; }
        public long HousingId { get; set; }
        public string ReservationNumber { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal Price { get; set; }
    }
}
