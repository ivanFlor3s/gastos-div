namespace ApiGastos.Entities
{
    public class GroupUser

    {
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
    