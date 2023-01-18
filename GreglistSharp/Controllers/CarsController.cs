namespace GreglistSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly CarsService _carsService;

    private readonly Auth0Provider _auth0Provider;

    public CarsController(CarsService carsService, Auth0Provider auth0Provider)
    {
        _carsService = carsService;
        _auth0Provider = auth0Provider;
    }

    [HttpGet]
    public ActionResult<List<Car>> Get()
    {
        try
        {
            List<Car> cars = _carsService.GetCars();
            return Ok(cars);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]

    public ActionResult<Car> GetById(int id)
    {
        try
        {
            Car car = _carsService.GetById(id);
            return Ok(car);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Car>> Create([FromBody] Car carData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            carData.CreatorId = userInfo.Id;
            Car car = _carsService.CreateCar(carData);
            return Ok(car);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]

    public async Task<ActionResult<Car>> Edit(int id, Car update)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            update.Id = id;
            Car car = _carsService.Edit(update, userInfo.Id);
            return Ok(car);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]

    public Task<ActionResult<string>> Delete(int id)
    {

        try
        {
            string message = _carsService.Delete(id, userInfo.Id);
            return Task.FromResult(Ok(message));
        }
        catch (Exception e)
        {
            return Task.FromResult(BadRequest(e.Message));
        }
    }
}
