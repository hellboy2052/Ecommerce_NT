using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerSite.Data;
using ServerSite.Models;
using ServerSite.Services;
using SharedVm;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ServerSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Bearer")]
    public class ImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageController(ApplicationDbContext context, IWebHostEnvironment webhostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webhostEnvironment;
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult> Post([FromForm] ImageVm image)
        {
            var image1 = new Image();
            string path = _webHostEnvironment.WebRootPath + "\\images\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fileStream=System.IO.File.Create(path+image.ImageFile.FileName))
            {
                image.ImageFile.CopyTo(fileStream);
                fileStream.Flush();
                image1.ImagePath = "/images/" + image.ImageFile.FileName.ToString();
            }
            image1.ProductId = image.ProductId;
            _context.Images.Add(image1);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
