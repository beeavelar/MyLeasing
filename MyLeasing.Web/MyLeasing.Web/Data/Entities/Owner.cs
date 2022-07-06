using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "The field {0} can contain {1} numbers.")]
        public int Document { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characteres length.")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? FixedPhone { get; set; }

        [Required]
        [MaxLength(9, ErrorMessage = "The field {0} can contain {1} numbers.")]
        public int CellPhone { get; set; }

        public string? Addrress { get; set; }

    }
}
