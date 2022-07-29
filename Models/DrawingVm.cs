
using System.ComponentModel.DataAnnotations;

namespace DrawingsWebApp.Models
{
    public class DrawingVm
    {
        public string FileName { get; set; }
        public IFormFile Image { get; set; }

    }
}
