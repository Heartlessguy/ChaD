using System;
using ChaD.server.Interfaces;

namespace ChaD.server.Models
{
    public class TextMessage : IMessage
    {
        private int _id;
        private string _textMessage;

        public TextMessage(string message)
        {
            _textMessage = message;
        }
        public int Id { get; set; }
        public object MessageContainer
        {
            get => _textMessage;
            set => _textMessage = (string)value;
        }
        public DateTime TimeStamp { get; set; }
    }
}