using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Owner : IEntity //Implementar o interface IEntity
    {
        public int Id { get; set; }

        public int? Document { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characteres length.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Fixed phone")]
        public int? FixedPhone { get; set; }

        [Display(Name = "Cellphone")]
        public int? CellPhone { get; set; }

        [Display(Name = "Address")]
        public string Addrress { get; set; }

        [Display(Name = "Photo")]
        public string ImageUrl { get; set; }

        public User User { get; set; }

        public string ImageFullPath
        {
            get
            {
                if(string.IsNullOrEmpty(ImageUrl)) //Se tiver vazio
                {
                    return null; //É nulo
                }

                return $"https://localhost:44355{ImageUrl.Substring(1)}";
            }
        }

    }
}
