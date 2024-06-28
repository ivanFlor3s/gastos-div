namespace ApiGastos.Entities
{
    public class GroupUserSpent
    {
        public int GroupId { get; set; }
        public string AppUserId { get; set; }
        public int SpentId { get; set; }
        public Group Group { get; set; } = null!;
        public Spent Spent { get; set; } = null!;
        public AppUser AppUser { get; set; }
    }
}
