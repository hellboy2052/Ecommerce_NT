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
    ////[Authorize("Bearer")]
    public class ImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;
        public ImageController(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult> PostImage(IFormFile file, ImageVm imageVm)
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
