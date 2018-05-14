using Camelonta.Backend.Models;
using System.Collections.Generic;

namespace Camelonta.Backend.Repository
{
    public interface IHousingRepository
    {
        IEnumerable<Housing> GetHousings();
        Housing GetHousingById(long id);
    }
}
