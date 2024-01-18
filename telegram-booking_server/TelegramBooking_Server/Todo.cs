namespace TelegramBooking_Server
{
    public class Todo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class  Admin
    {
        public int Id { get; set; }

        public int Permissions { get; set; }
    }
}
