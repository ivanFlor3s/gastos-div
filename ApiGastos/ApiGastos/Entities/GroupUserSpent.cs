namespace ApiGastos.Entities
{
    public class GroupUserSpent
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int SpendId { get; set; }
        public Group Group { get; set; } = null!;
        public Spent Spent { get; set; } = null!;
        public AppUser AppUser { get; set; }
    }
}
