using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod_10
{
    public class Chats
    {
        public Chats(long id, string name, string text, string date)
        {
            this.id = id;
            this.name = name;
            this.messageCollection = new List<Message>();
            Write(text, date);
        }
        public void Write(string text, string date)
        {
            messageCollection.Add(new Message(text, date));
        }
        public long Id { get { return this.id; } set { this.id = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public List<Message> MessageCollection  { get { return this.messageCollection; }set { this.messageCollection = value; } }

        private long id;
        private string name;
        private List<Message> messageCollection;
    }
    public class Message
    {
        public Message(string text, string date)
        {
            this.text = text;
            this.date = date;
        }

        public string Text { get { return this.text; } set { this.text = value; } }
        public string Date { get { return this.date; } set { this.date = value; } }

        private string text;
        private string date;
    }
}
