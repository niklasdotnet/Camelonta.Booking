using HouseBooking.Backend.Models;
using System.Collections.Generic;

namespace HouseBooking.Backend.Repository
{
    public interface IHousingRepository
    {
        IEnumerable<Housing> GetHousings();
        Housing GetHousingById(long id);
    }
}
