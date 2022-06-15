using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleProje.Models;
using System.Collections.Generic;

namespace VehicleProje.Controllers
{
    public class DBVehicles
    {
        private readonly VehicleContext _context; 

        public DBVehicles(VehicleContext context)
        {
            _context = context;
        }

 
        public List<Boat> BoatsByColor(int colorId)
        {
            var vehicleContext = _context.Boats.Where(b => b.ColorId == colorId).Include(b => b.Color);
            return vehicleContext.ToList(); 
        }
        // GET: DBVehicles Buses by color
        public List<Bus> BusesByColor(int colorId)
        {
            var vehicleContext = _context.Buses.Where(b => b.ColorId == colorId).Include(b => b.Color);
            return vehicleContext.ToList(); 
        }
        // GET: DBVehicles Cars by color
        public List<Car> CarsByColor(int colorId)
        {
            var vehicleContext = _context.Cars.Where(c => c.ColorId == colorId).Include(c => c.Color);
            return vehicleContext.ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool ChangeHeadLights(int id, bool headLightsState)
        {
            Car car = _context.Cars.Find(id);

            if (car == null)
            {
                return false;
            }
            try
            {
                car.HeadLights = headLightsState;
                _context.Update(car);
                _context.SaveChanges();
                return true;
            }
            catch
            {

                return false;    
            }
         
        
        }

        // POST: DBVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public bool DeleteCar(int id)
        {
            var car = _context.Cars.Find(id);
            if (car == null)
            {
                return false;
            }
            try
            {
                
                _context.Cars.Remove(car);
                _context.SaveChanges();
                return true;

            }
            catch 
            {

                return false;
            }
                    
        }

    }
}
