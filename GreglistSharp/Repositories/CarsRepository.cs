namespace GreglistSharp.Repositories;

public class CarsRepository
{
    private readonly IDbConnection _db;

    public CarsRepository(IDbConnection db)
    {
        _db = db;
    }


    public List<Car> GetCars()
    {
        var sql = "SELECT * FROM cars";
        return _db.Query<Car>(sql).ToList();
    }

    public Car CreateCar(Car carData)
    {
        var sql = @"
    INSERT INTO cars(
      make, model, year, price, description, imgUrl, creatorId
    )
    VALUES(
      @Make, @Model, @Year, @Price, @Description, @ImgUrl, @CreatorId
    );
    SELECT LAST_INSERT_ID();
    ";

        carData.Id = _db.ExecuteScalar<int>(sql, carData);
        return carData;

    }

    internal Car GetById(int id)
    {
        string sql = @"
    SELECT
    c.*,
    a.*
    FROM cars c
    JOIN accounts a ON a.id = c.creatorId
    WHERE c.id =@id
    ";
        return _db.Query<Car, Account, Car>(sql, (car, account) =>
        {
            car.Creator = account;
            return car;
        }, new { id }).FirstOrDefault();
    }
}