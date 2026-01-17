using System.ComponentModel.DataAnnotations;

namespace GoTrip.Areas.AdminPanel.ViewModels
{
    public class UpdateVM
    {
        [Required]
        public string title { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public decimal rating { get; set; }
        public string? imageURL { get; set; }
        public IFormFile? imageFile { get; set; }
        public int id_ { get; set; }
    }
}
