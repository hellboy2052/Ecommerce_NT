using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ServerSite.Data;
using ServerSite.Models;
using SharedVm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ServerSite.Services;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace ServerSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Bearer")]
    public class ImageController: ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;
        public ImageController( IWebHostEnvironment hostEnvironment, ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
            _storageService = storageService;
        }
       
        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult> Post(IFormFile file,ImageVm imageVm)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            imageVm.ImagePath = fileName;
            var image = new Image
            {
                Id = imageVm.Id,
                ProductId = imageVm.ProductId,
                ImagePath = imageVm.ImagePath
            };
            _context.Images.Add(image);
            return Accepted();
        }

    }
}
