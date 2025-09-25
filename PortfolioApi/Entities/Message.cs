namespace PortfolioApi.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string senderName { get; set; }
        public string senderEmail { get; set; }
        public string content { get; set; }
    }
}
