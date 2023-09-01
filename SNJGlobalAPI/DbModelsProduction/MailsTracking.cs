using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModels
{
    public class MailsTracking
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public DateTime? SentAt { get; set; }
        public string NotSentReason { get; set; }
    }
}