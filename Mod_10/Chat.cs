using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mod_10
{
    public class Chat
    {
        public Chat(long id, string name)
        {
            this.id = id;
            this.name = name;
            this.messageCollection = new ObservableCollection<Message>();
        }

        public void Write(string text, string date)
        {
            messageCollection.Add(new Message(text, date, name));
        }

        public long Id { get { return this.id; } set { this.id = value; } }

        public string UserName { get { return this.name; } set { this.name = value; } }

        public ObservableCollection<Message> MessageCollection  { get { return this.messageCollection; }set { this.messageCollection = value; } }

        private long id;
        private string name;
        private ObservableCollection<Message> messageCollection;
    }
}
