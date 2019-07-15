using System;

namespace uichat
{
    [Serializable]
    public class Message
    {
        public int Sender;
        public string Body;

        public DateTime TimeStamp;

        public Message(int sender, string body, DateTime timeStamp)
        {
            this.Sender = sender;
            this.Body = body;
            this.TimeStamp = timeStamp;
        }
    }
}