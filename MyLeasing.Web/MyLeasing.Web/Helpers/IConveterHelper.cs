using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Helpers
{
    public interface IConveterHelper
    {
        Owner ToOwner(OwnerViewModel model, string path, bool isNew); //Tem que passar o modelo, a pasta e se é novo

        OwnerViewModel ToOwnerViewModel(Owner owner);

    }
}
