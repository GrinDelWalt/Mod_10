using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mod_10
{
    public class Chat
    {
        public Chat(long id, string name)
        {
            _unreadMessages = new ObservableCollection<int>();
            _unreadMessages.Add(1);
            this._id = id;
            this._name = name;
            this._messageCollection = new ObservableCollection<Message>();
        }

        public void Write(string text, string date)
        {
            _messageCollection.Add(new Message(text, date, _name));
        }

        public ObservableCollection<int> UnreadMessages { get { return this._unreadMessages; }set { this._unreadMessages = value; } }

        public long Id { get { return this._id; } set { this._id = value; } }

        public string UserName { get { return this._name; } set { this._name = value; } }

        public ObservableCollection<Message> MessageCollection  { get { return this._messageCollection; }set { this._messageCollection = value; } }

        private ObservableCollection<int> _unreadMessages;
        private long _id;
        private string _name;
        private ObservableCollection<Message> _messageCollection;
    }
}
