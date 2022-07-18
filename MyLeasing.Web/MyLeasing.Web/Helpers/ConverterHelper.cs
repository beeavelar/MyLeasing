using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Helpers
{
    public class ConverterHelper : IConveterHelper
    {
        public Owner ToOwner(OwnerViewModel model, string path, bool isNew)
        {
            //Fazer a comversão do OwnerViewModel em um Owner
            return new Owner
            {
                Id = isNew? 0 : model.Id, //Se o valor não vier preenchido, colocar 0, senão buscar o id
                Document = model.Document,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixedPhone = model.FixedPhone,
                CellPhone = model.CellPhone,
                Addrress = model.Addrress,
                ImageUrl = path,
                User = model.User
            };
        }

        public OwnerViewModel ToOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel
            {
                Id = owner.Id,
                Document = owner.Document,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                FixedPhone = owner.FixedPhone,
                CellPhone = owner.CellPhone,
                Addrress = owner.Addrress,
                ImageUrl = owner.ImageUrl,
                User = owner.User
            };
        }
    }
}
