using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyLeasing.Web.Helpers
{
    public class ImageHelper : IImageHelper //Essa classe implementa o inteface
    {
        public async Task<string> UploadImageAsync(IFormFile imageFile, string folder)
        {
            string guid = Guid.NewGuid().ToString(); //Guid gera uma chave aleatória
            string file = $"{guid}.jpg";

            //Caminho do ficheiro
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\images\\{folder}",
                file);

            //gravar a imagem
            using (FileStream stream = new FileStream(path, FileMode.Create)) //gravar no servidor, passando dois parametros
                                                                       //Um é o path (o caminho do ficheiro) e o segundo é criar um ficheiro novo
            {
                //Guardar 
                await imageFile.CopyToAsync(stream);
            }

            return $"~/images/{folder}/{file}";
        }
    }
}
