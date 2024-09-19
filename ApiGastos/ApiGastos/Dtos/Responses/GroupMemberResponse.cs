namespace ApiGastos.Dtos.Responses
{
    public class GroupMemberResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public bool IsAdmin { get; set; }
        public bool IsTemporal { get; set; }

    }
}
