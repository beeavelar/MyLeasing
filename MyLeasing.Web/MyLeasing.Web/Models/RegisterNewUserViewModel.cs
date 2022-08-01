using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Models
{
    public class RegisterNewUserViewModel
    {
        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more then {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)] //Obriga a colocar email
        public string Username { get; set; }

        [Required]
        [MinLength(6)] //Pede no mínimo 6 caracteres, se quisermos mais exigências --> Ir no Startup --> No método ConfigureServices podemos mudar
        public string Password { get; set; }

        [Required]
        [Compare("Password")] //Vai comparar o campo Password com o campo ConfirmPassword
        public string ConfirmPassword { get; set; }
    }
}
