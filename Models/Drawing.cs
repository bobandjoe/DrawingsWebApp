
using System.ComponentModel.DataAnnotations;

namespace DrawingsWebApp.Models
{
    public class Drawing
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }
        public DateTime UploadedDateTime { get; set; } = DateTime.Now;

    }
}
