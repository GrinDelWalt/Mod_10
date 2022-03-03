using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod_10
{
    public class ReadMessage
    {
        public ObservableCollection<Chats> Chats { get; set; }

        public MainWindow window;

        public ObservableCollection<Message> messages { get; set; }
        
        public ReadMessage(MainWindow window)
        {
            
            this.window = window;
            Chats = new ObservableCollection<Chats>();
            
        }
        /// <summary>
        /// Read Message
        /// </summary>
        /// <param name="messageText">Text</param>
        /// <param name="name">Name</param>
        /// <param name="id">Id</param>
        public void MessageLog(string messageText, string name, long id)
        {
            Debug.WriteLine("----");

            string text = $"{DateTime.Now.ToLongTimeString()}: {id} {name} {messageText}";

            Debug.WriteLine(text);

            DateTime date = DateTime.Now;

            window.Dispatcher.Invoke(()=>
            {
                if (Chats.Count != 0)
                {
                    Chats chat = Chats.FirstOrDefault(x => x.Id == id);
                    if (chat != null)
                    {
                        chat.MessageCollection.Add(new Message(messageText, date));
                    }
                    else
                    {
                        Chats.Add(new Chats(id, name, messageText, DateTime.Now));
                    }
                }
            });
        }

    }
}
