namespace SignalRServer.Models
{
    public class ChatMessage
    {
        public string? Id { get; set; }

        public string UserId { get; set; }

        public string RoomId { get; set; }

        public string Message { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
