using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.Entities
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string SenderName { get; set; }
        [EmailAddress]
        [Required]
        public string SenderEmail { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
