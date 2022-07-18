using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyLeasing.Web.Helpers
{
    public interface IImageHelper
    {
        //Método para faezr o upload
        Task<string> UploadImageAsync(IFormFile imageFile, string folder); //Recebe a propria imagem e a pasta onde vou guardar a imagem
    }
}
