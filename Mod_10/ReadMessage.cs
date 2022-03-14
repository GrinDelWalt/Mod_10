using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mod_10
{
    public class ReadMessage
    {
        public ObservableCollection<Chats> Chats { get; set; }

        object sender;
        Telegram.Bot.Args.MessageEventArgs e;
        ListBox logList;
        public MainWindow _window;

        public ObservableCollection<Message> messages { get; set; }

        public ReadMessage(ListBox logList, MainWindow window)
        {
            this.logList = logList;
            this._window = window;
            Chats = new ObservableCollection<Chats>();

        }
        /// <summary>
        /// Read Message
        /// </summary>
        /// <param name="messageText">Text</param>
        /// <param name="name">Name</param>
        /// <param name="id">Id</param>
        public void MessageLog(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Debug.WriteLine("----");
            string text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.Chat.Id} {e.Message.Chat.FirstName} {e.Message.Text}";

            Debug.WriteLine(text);

            string date = DateTime.Now.ToLongDateString();

            _window.Dispatcher.Invoke(() =>
            {

                Chats chat = Chats.FirstOrDefault(x => x.Id == e.Message.Chat.Id);
                if (chat != null)
                {
                    chat.MessageCollection.Add(new Message(e.Message.Text, date, e.Message.Chat.FirstName));
                }
                else
                {
                    Chats.Add(new Chats(e.Message.Chat.Id, e.Message.Chat.FirstName, e.Message.Text, date));
                }
                logList.ItemsSource = Chats;
            });
        }

    }
}
