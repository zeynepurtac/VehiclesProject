using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleProje.Models
{

    public class Color
    {
        public int ColorId { get; set; }
        
        [Required]
        public string ColorName { get; set; }    
    }
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        
        [Required]
        public int ColorId { get; set; }
        public Color? Color { get; set; }    

    }
}
