using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;
using System;

namespace MyLeasing.Web.Helpers
{
    public interface IConveterHelper
    {
        Owner ToOwner(OwnerViewModel model, Guid imageId, bool isNew); //Tem que passar o modelo, a pasta e se é novo

        OwnerViewModel ToOwnerViewModel(Owner owner);

        Lessee ToLessee(LesseeViewModel model, Guid imageId, bool isNew); //Tem que passar o modelo, a pasta e se é novo

        LesseeViewModel ToLesseeViewModel(Lessee lessee);

    }
}
