namespace ApiGastos.Dtos
{
    public class GroupMembersUpdateDto
    {
        public List<string> AddedUsers { get; set; } 
        public List<string> DeletedUsers { get; set; }
    }
}
