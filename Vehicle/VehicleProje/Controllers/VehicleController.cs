using Microsoft.AspNetCore.Mvc;
using VehicleProje.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleContext _context;
        private DBVehicles dbVehiclesController; 
        public VehicleController(VehicleContext context)
        {
            _context = context;
            dbVehiclesController = new DBVehicles(_context);
        }

        [HttpGet]
  
        public IEnumerable<Vehicle> Get(int colorId, int vehicleType) 
        {
            switch (vehicleType) 
            {
                case 1:
                    return dbVehiclesController.BoatsByColor(colorId);
                case 2:
                    return dbVehiclesController.BusesByColor(colorId);
                case 3:
                    return dbVehiclesController.CarsByColor(colorId);            
            }
            return null;
                  
        }

        // POST api/<VehicleController>
        [HttpPost]
        public bool Post(int carId, bool headLightsState) 
        {
           return dbVehiclesController.ChangeHeadLights(carId, headLightsState);
        }

        // DELETE api/<VehicleController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
           return dbVehiclesController.DeleteCar(id);
        }
    }
}
