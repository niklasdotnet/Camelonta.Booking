using Camelonta.Backend.Models;

namespace Camelonta.Backend.Repository
{
    public interface IReservationRepository
    {
        int CreateReservation(Reservation model);
        Reservation GetReservation(long id);
    }
}
