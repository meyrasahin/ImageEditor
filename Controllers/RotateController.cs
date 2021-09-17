using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ImageEditor.Controllers
{
    public class RotateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string filename, IFormFile blob)
        {
            try
            {
                using (var image = SixLabors.ImageSharp.Image.Load(blob.OpenReadStream()))
                {
                    string fileExtenstion = filename.Substring(filename.LastIndexOf('.'));

                    image.Mutate(x => x.Resize(image.Width, image.Height));
                    var newfileName = GenerateFileName("Photo_", fileExtenstion);
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newfileName}";
                    image.Save(filepath);


                    //using (Bitmap bitmap = new Bitmap(filepath))
                    //{
                    //    bitmap.RotateFlip(RotateFlipType.Rotate180FlipY);
                    //    var newfileName2 = GenerateFileName("Photo_", fileExtenstion);
                    //    var filepath2 = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newfileName2}";
                    //    //image.Save(filepath2);
                    //    bitmap.Save(filepath2);

                    //    image.Dispose();
                    //    //MyFileName = newfileName;
                    //}


                }

                return Json(new { Message = "OK" });
            }
            catch (Exception)
            {
                return Json(new { Message = "ERROR" });
            }
        }

        public string GenerateFileName(string fileTypeName, string fileextenstion)
        {
            if (fileTypeName == null)
                throw new ArgumentNullException(nameof(fileTypeName));
            if (fileextenstion == null)
                throw new ArgumentNullException(nameof(fileextenstion));
            return $"{fileTypeName}_{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{fileextenstion}";
        }
    }
}
