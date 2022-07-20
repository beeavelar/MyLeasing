using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;
using System;

namespace MyLeasing.Web.Helpers
{
    public class ConverterHelper : IConveterHelper
    {
        public Lessee ToLessee(LesseeViewModel model, Guid imageId, bool isNew)
        {
            return new Lessee
            {
                Id = isNew ? 0 : model.Id, 
                Document = model.Document,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixedPhone = model.FixedPhone,
                CellPhone = model.CellPhone,
                Addrress = model.Addrress,
                PhotoId = imageId,
                User = model.User
            };
        }

        public LesseeViewModel ToLesseeViewModel(Lessee lessee)
        {
            return new LesseeViewModel
            {
                Id = lessee.Id,
                Document = lessee.Document,
                FirstName = lessee.FirstName,
                LastName = lessee.LastName,
                FixedPhone = lessee.FixedPhone,
                CellPhone = lessee.CellPhone,
                Addrress = lessee.Addrress,
                PhotoId = lessee.PhotoId,
                User = lessee.User
            };
        }

        public Owner ToOwner(OwnerViewModel model, Guid imageId, bool isNew)
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
                ImageId = imageId,
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
                ImageId = owner.ImageId,
                User = owner.User
            };
        }
    }
}
