using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VehicleProje.Models
{
    public class Car : Vehicle  
    {
        public int Wheels { get; set; }
        
        [Required]
        public bool HeadLights { get; set; } 
    }
}
