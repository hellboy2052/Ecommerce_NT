using Microsoft.AspNetCore.Http;

namespace SharedVm
{
    public class ImageVm
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
