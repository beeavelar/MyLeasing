using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyLeasing.Web.Helpers
{
    public interface IBlobHelper
    {
        //Vamos ter 3 maneiras diferentes de receber imagens:
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName); 
        //recebe um ficheiro de um formulario

        Task<Guid> UploadBlobAsync(byte[] file, string containerName); 
        //recebe um ficheiro de butes que corresponde a imagem --> imagens enviadas por telemovel --> usado no projeto mobile

        Task<Guid> UploadBlobAsync(string image, string containerName); 
        //recebe imagem por endereço urel
    }
}
