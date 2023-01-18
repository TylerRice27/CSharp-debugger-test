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

    }

    internal Car Edit(Car update, string userId)
    {
        Car original = GetById(update.Id);
        if (original.CreatorId != userId)
        {
            throw new Exception("You are not the owner of this car");
        }
        original.Make = update.Make ?? original.Make;
        original.Model = update.Model ?? original.Model;
        original.ImgUrl = update.ImgUrl ?? original.ImgUrl;
        update.Description = update.Description ?? update.Description;
        return _carsRepo.Edit(original);

    }

    internal string Delete(int id, string userId)
    {
        Car original = GetById(id);
        if (original.CreatorId != userId)
        {
            throw new Exception("You are not the owner of this car");
        }
        _carsRepo.Delete(id);
        return $"You deleted {original.Make} {original.Model}";
    }
}