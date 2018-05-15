namespace Camelonta.Backend.Models
{
    public class Housing
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long BaseDayFee { get; set; }
        public decimal DayPriceFactor { get; set; }
        public string Image { get; set; }
    }
}
