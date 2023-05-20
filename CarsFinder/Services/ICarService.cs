using CarsFinder.Model;

namespace CarsFinder.Services;

public interface ICarService
{
    Task<IEnumerable<Car>> GetCars();
}
