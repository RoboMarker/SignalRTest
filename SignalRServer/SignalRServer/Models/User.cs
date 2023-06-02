namespace SignalRServer.Models
{
    public class User
    {
        public string UserId { get; set; } = null!;
        public string? UserName { get; set; }
        public int PermissionsId { get; set; }


    }
}
