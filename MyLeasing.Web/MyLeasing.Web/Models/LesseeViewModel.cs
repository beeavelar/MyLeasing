using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Models
{
    public class LesseeViewModel : Lessee
    {
        [Display(Name = "Photo")]
        public IFormFile ImageFile { get; set; }
    }
}
