
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrawingsWebApp.Models
{
    public class Drawing
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string FileName { get; set; }
        public string Comment { get; set; } = "";
        [Required]
        public byte[] Image { get; set; }
        public DateTime UploadedDateTime { get; set; } = DateTime.Now;
        [NotMapped]
        [Display(Name = "Name")]
        public IFormFile FormImage { get; set; }
        [NotMapped]
        [Display(Name = "Comment")]
        public string FormComment { get; set; }

    }
}
