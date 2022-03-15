using System.Collections.Generic;

namespace Mod_10
{
    public class Chat
    {
        public Chat(long id, string name)
        {
            this.id = id;
            this.name = name;
            this.messageCollection = new List<Message>();
        }

        public void Write(string text, string date)
        {
            messageCollection.Add(new Message(text, date, name));
        }

        public long Id { get { return this.id; } set { this.id = value; } }

        public string Name { get { return this.name; } set { this.name = value; } }

        public List<Message> MessageCollection  { get { return this.messageCollection; }set { this.messageCollection = value; } }

        private long id;
        private string name;
        private List<Message> messageCollection;
    }
}
