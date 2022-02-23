using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod_10
{
    class ReadMessage
    {
        public ObservableCollection<Chats> chats;

        public MainWindow window;

        public Telegram.Bot.Types.Update e;

        private void MessageLog()
        {
            chats = new ObservableCollection<Chats>();
            Debug.WriteLine("----");

            string messageText = e.Message.Text;
            string name = e.Message.Chat.FirstName;
            long id = e.Message.Chat.Id;

            string text = $"{DateTime.Now.ToLongTimeString()}: {id} {name} {messageText}";

            List<string> messages = new List<string>();
            messages.Add(messageText);

            List<DateTime> data = new List<DateTime>();
            data.Add(DateTime.Now);

            Debug.WriteLine(text);
            
            window.Dispatcher.Invoke(() =>
            {
                if (chats.Count != 0)
                {
                    Chats chat = chats.FirstOrDefault(x => x.Id == "id поиска");
                    if (chat != null)
                    {

                    }
                    else
                    {
                        chats.Add(new Chats(id, name, messageText, data));
                    }
                }
                
                if (chats.Id.)
                {

                }
                
                
            });

        }

    }
}
