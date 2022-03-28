using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using Telegram.Bot.Args;

namespace Mod_10
{
    public class MessageReader
    {
        private ListBox _logList;
        private MainWindow _window;
        private SerelaseJson _serelaseJson;


        public ObservableCollection<Chat> Chats { get; set; }

        public MessageReader(MainWindow window, ListBox logList)
        {
            _window = window;
            Chats = new ObservableCollection<Chat>();
            _logList = logList;
            _serelaseJson = new SerelaseJson();
        }
        /// <summary>
        /// Read Message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MessageLog(long id, string name, string textMsg)
        {
            Debug.WriteLine("----");
            string text = $"{DateTime.Now.ToLongTimeString()}: {id} {name} {textMsg}";

            Debug.WriteLine(text);

            string date = Convert.ToString(DateTime.Now);
            
            _window.Dispatcher.Invoke(() =>
            {
                Chat chat = Chats.FirstOrDefault(x => x.Id == id);
                int index = Chats.IndexOf(chat);
                _logList.ItemsSource = Chats;
                if (chat != null)
                {
                    chat.UnreadMessages[0] += 1;
                    chat.MessageCollection.Add(new Message(textMsg, date, name));
                    Chats[index] = chat;
                }
                else
                {
                    Chat newChat = new Chat(id, name);
                    newChat.Write(textMsg, date);
                    Chats.Add(newChat);
                }
                
            });
            _serelaseJson.RecordingJson(Chats);
        }
        /// <summary>
        /// Get message collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ObservableCollection<Message> GetMessageCollection(long id)
        {
            Chat chat = UserSearch(id);
            return chat.MessageCollection;
        }
        public void ClearUnreadMessages(long id)
        {
            Chat chat = UserSearch(id);
            chat.UnreadMessages[0] = 0;
        }
        private Chat UserSearch(long id)
        {
            Chat chat = Chats.FirstOrDefault(x => x.Id == id);
            return chat;
        }
    }
}
