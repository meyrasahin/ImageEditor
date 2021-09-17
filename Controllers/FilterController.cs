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
    public class FilterController : Controller
    {
        private static string MyFileName = "";
        public IActionResult BlackWhite()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BlackWhite(string filename, IFormFile blob)
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
                    string newImage = newfileName.Substring(newfileName.LastIndexOf("//") + 1);

                    using (Bitmap bitmap = new Bitmap(filepath))
                    {
                        int rgb;
                        System.Drawing.Color c;

                        for (int y = 0; y < bitmap.Height; y++)
                            for (int x = 0; x < bitmap.Width; x++)
                            {
                                c = bitmap.GetPixel(x, y);
                                rgb = (int)((c.R + c.G + c.B) / 3);
                                bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(rgb, rgb, rgb));
                            }

                        var newfileName2 = GenerateFileName("Photo_", fileExtenstion);
                        var filepath2 = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newfileName2}";
                        //image.Save(filepath2);
                        bitmap.Save(filepath2);

                        image.Dispose();
                        //MyFileName = newfileName;
                    }


                }

                return Json(new { Message = "OK" });
            }
            catch (Exception e)
            {
                return Json(new { Message = "ERROR" });
            }
        }

        //[HttpPost]
        //public IActionResult DeleteOldFile()
        //{
        //    System.IO.File.Delete(MyFileName);
        //    return Json("");
        //}


        public IActionResult Sepia()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sepia(string filename, IFormFile blob)
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
                    string newImage = newfileName.Substring(newfileName.LastIndexOf("//") + 1);

                    using (Bitmap bitmap = new Bitmap(filepath))
                    {
                        int outputRed, outputGreen, outputBlue;
                        System.Drawing.Color c;

                        for (int y = 0; y < bitmap.Height; y++)
                            for (int x = 0; x < bitmap.Width; x++)
                            {
                                c = bitmap.GetPixel(x, y);
                                outputRed =(int) (c.R * 0.393) + (int) (c.G * 0.769) + (int)(c.B * 0.189);
                                outputGreen = (int)(c.R * 0.349) + (int)(c.G * 0.686) + (int)(c.B * 0.168);
                                outputBlue = (int)(c.R * 0.272) + (int)(c.G * 0.534) + (int)(c.B * 0.131);

                                if (outputRed > 255)
                                {
                                    outputRed = 255;
                                }

                                if (outputGreen > 255)
                                {
                                    outputGreen = 255;
                                }

                                if (outputBlue > 255)
                                {
                                    outputBlue = 255;
                                }



                                //rgb = (int)(c.R * 0.180 + c.G * 0.770 + c.B * 0.050);
                                bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(outputRed, outputGreen, outputBlue));
                            }

                        var newfileName2 = GenerateFileName("Photo_", fileExtenstion);
                        var filepath2 = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newfileName2}";
                        //image.Save(filepath2);
                        bitmap.Save(filepath2);

                        image.Dispose();
                        //MyFileName = newfileName;
                    }


                }

                return Json(new { Message = "OK" });
            }
            catch (Exception e)
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
