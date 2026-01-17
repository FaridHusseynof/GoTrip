using System.ComponentModel.DataAnnotations;

namespace GoTrip.Models
{
    public class Tour:BaseModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Rating { get; set; }
        public string ImageURL { get; set; }
    }
}
