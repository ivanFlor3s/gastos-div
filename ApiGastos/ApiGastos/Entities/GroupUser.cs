namespace ApiGastos.Entities
{
    public class GroupUser : SoftDeleteEntity

    {
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsAdmin { get; set; }
    }
}
    