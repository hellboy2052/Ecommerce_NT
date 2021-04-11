using System.Collections;

namespace ServerSite.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public ICollection Product { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
