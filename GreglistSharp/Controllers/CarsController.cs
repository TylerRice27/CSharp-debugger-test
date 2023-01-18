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

    [HttpGet("{id}")]

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

}
