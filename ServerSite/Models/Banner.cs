namespace ServerSite.Models
{
    public class Banner
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public string ImagePath { get; set; }
    }
}
