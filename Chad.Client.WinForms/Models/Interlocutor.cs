using System.Collections.Generic;

namespace Chad.Client.WinForms.Models
{
    public class Interlocutor
    {
        public Interlocutor()
        {
            MessageHistory = new List<MessageRecord>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        // TODO: public bool IsConnected { get; set; }
        public List<MessageRecord> MessageHistory { get; set; }

    }
}