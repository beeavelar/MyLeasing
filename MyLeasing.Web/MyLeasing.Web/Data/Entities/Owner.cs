using System;
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
        //public string ImageUrl { get; set; }
        public Guid ImageId { get; set; }

        public User User { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty //Se o ImageId estiver vazio, 
            ? $"https://myleasing.azurewebsites.net/images/noimage.jpg" //vai buscar a imagem "noimage"
            : $"https://myleasingcet69.blob.core.windows.net/owners/{ImageId}"; //caso exista imagem, buscar o Id que esta dentro do container que foi criado

    }
}
