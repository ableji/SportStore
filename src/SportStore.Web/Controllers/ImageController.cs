using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Model.Domain;
using SportStore.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SportStore.Web.Controllers
{
    
    public class ImageController : AblBaseController
    {
        #region initial
        private IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        #endregion

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UploadImage()
        {
            try
            {
                var image = Request.Form.Files.FirstOrDefault();

                if (image == null) return Json(new { error = "select a file" });
                if (image.ContentType != "image/jpeg" && image.ContentType != "image/png") return Json(new { error = "Invalid Image" });
                if (image.Length > 5000000) return Json(new { error = "Too large image" });


                var img = new Image();
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    img.IMG = memoryStream.ToArray();
                }

                int imageid =await _imageService.save(img);
                return Json(new { imageId = imageid });
            }
            catch (Exception)
            {

                return Json(new { error = "Internal server error" });
            }


        }


        public async Task<IActionResult> GetImage(int imageId)
        {
            var image =await _imageService.GetImageById(imageId);

            return (image?.IMG != null) ?
                new FileContentResult(image.IMG, "image/jpeg")
                : null;
        }
    }
}
