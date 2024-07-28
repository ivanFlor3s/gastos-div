namespace ApiGastos.Entities
{
    public class SpentParticipant
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int SpentId { get; set; }
        public Spent Spent { get; set; }
    }
}
