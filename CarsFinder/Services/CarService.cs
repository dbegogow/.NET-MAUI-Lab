using System.Text.Json;
using CarsFinder.Model;

namespace CarsFinder.Services;

class CarService
{
    private const string CarsFilePath = "../../../Input/cars.json";

    public async Task<IEnumerable<Car>> GetCars()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(CarsFilePath);
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();

        var cars = JsonSerializer.Deserialize<IEnumerable<Car>>(contents);

        return cars;
    }
}
