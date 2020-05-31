using System;

namespace ChaD.server.Interfaces
{
    public interface IMessage
    {
        public int Id { get; set; }
        public object MessageContainer { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}