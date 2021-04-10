namespace ServerSite.Models
{
    public class Cart
    {
        public int Id { get; set; }
        //public List<CartProductVm> cartProducts { get; set; }
        public double Total { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
