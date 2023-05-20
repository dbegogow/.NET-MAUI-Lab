using System.Collections.ObjectModel;
using System.Diagnostics;
using CarsFinder.Model;
using CarsFinder.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarsFinder.ViewModel;

public partial class CarsViewModel : BaseViewModel
{
    public ObservableCollection<Car> Cars { get; } = new();

    ICarService carService;
    IConnectivity connectivity;
    IGeolocation geolocation;

    public CarsViewModel(ICarService carService, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "Cars Finder";

        this.carService = carService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async Task GetCarsAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    "Please check internet and try again.", "OK");

                return;
            }

            IsBusy = true;

            var cars = await carService.GetCars();

            if (Cars.Count != 0)
            {
                Cars.Clear();
            }

            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get cars: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }
}
