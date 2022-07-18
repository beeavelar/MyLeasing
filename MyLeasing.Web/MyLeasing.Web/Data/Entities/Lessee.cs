using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Lessee : IEntity
    {
        public int Id { get; set; }

        public int Document { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characteres length.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characteres length.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Fixed phone")]
        public int? FixedPhone { get; set; }

        [Display(Name = "Cellphone")]
        public int? CellPhone { get; set; }

        [Display(Name = "Address")]
        public string Addrress { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        public User User { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        //public string PhotoFullPath
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(Photo)) //Se tiver vazio
        //        {
        //            return null; //É nulo
        //        }

        //        return $"https://localhost:44355{Photo.Substring(1)}";
        //    }
        //}
    }
}
