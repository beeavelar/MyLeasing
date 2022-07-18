using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Models
{
    public class OwnerViewModel : Owner //Herda da class Owner pq precisa das outras propriedades do Owner
    {
        [Display(Name = "Photo")]
        public IFormFile ImageFile { get; set; }
    }
}
