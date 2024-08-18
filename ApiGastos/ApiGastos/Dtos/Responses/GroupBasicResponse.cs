namespace ApiGastos.Dtos.Responses
{
    public class GroupBasicResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<AppUserResponse> Users { get; set; }
    }
}
