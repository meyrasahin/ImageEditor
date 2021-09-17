using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageEditor.Controllers
{
    public class CropController : Controller
    {
        public IActionResult Square()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Square(string filename, IFormFile blob)
        {
            try
            {
                using (var image = Image.Load(blob.OpenReadStream()))
                {
                    string fileExtension = filename.Substring(filename.LastIndexOf('.'));

                    image.Mutate(x => x.Resize(180, 180));
                    var newfileName = GenerateFileName("Photo_", fileExtension);
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newfileName}";
                    image.Save(filepath);
                }

                return Json(new { Message = "OK" });
            }
            catch (Exception)
            {
                return Json(new { Message = "ERROR" });
            }
        }


        public IActionResult Rectangle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Rectangle(string filename, IFormFile blob)
        {
            try
            {
                using (var image = Image.Load(blob.OpenReadStream()))
                {
                    string fileExtension = filename.Substring(filename.LastIndexOf('.'));

                    image.Mutate(x => x.Resize(300, 150));
                    var newfileName = GenerateFileName("Photo_", fileExtension);
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newfileName}";
                    image.Save(filepath);

                }

                return Json(new { Message = "OK" });
            }
            catch (Exception)
            {
                return Json(new { Message = "ERROR" });
            }
        }


        public IActionResult Circle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Circle(string filename, IFormFile blob)
        {
            try
            {
                using (var image = Image.Load(blob.OpenReadStream()))
                {
                    string fileExtenstion = filename.Substring(filename.LastIndexOf('.'));

                    image.Mutate(x => x.Resize(180, 180));
                    var newfileName = GenerateFileName("Photo_", fileExtenstion);
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newfileName}";
                    image.Save(filepath);
                }

                return Json(new { Message = "OK" });
            }
            catch (Exception)
            {
                return Json(new { Message = "ERROR" });
            }
        }

        public IActionResult Special()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Special(string filename, IFormFile blob)
        {
            try
            {
                using (var image = Image.Load(blob.OpenReadStream()))
                {
                    string fileExtenstion = filename.Substring(filename.LastIndexOf('.'));

                    image.Mutate(x => x.Resize(image.Width, image.Height));
                    var newfileName = GenerateFileName("Photo_", fileExtenstion);
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newfileName}";
                    image.Save(filepath);
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
