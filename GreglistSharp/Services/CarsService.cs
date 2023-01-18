namespace GreglistSharp.Services;

public class CarsService
{
    // REPOSITORY PATTERN
    // will be used to control communication with the DB

    private readonly CarsRepository _carsRepo;

    public CarsService(CarsRepository carsRepo)
    {
        _carsRepo = carsRepo;
    }

    public List<Car> GetCars()
    {
        return _carsRepo.GetCars();
    }

    public Car CreateCar(Car carData)
    {
        return _carsRepo.CreateCar(carData);
    }

    internal Car GetById(int id)
    {
        Car car = _carsRepo.GetById(id);
        if (car == null)
        {
            throw new Exception("No car by that id");
        }
        return car;
    }
}