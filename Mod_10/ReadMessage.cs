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
        Mod_9.TelegraBotHelper bot;
        

        public ObservableCollection<Chats> chats { get; set; }

        public MainWindow window;
        public Mod_9.Program program;

        
        public ReadMessage(MainWindow window)
        {
            bot = new Mod_9.TelegraBotHelper();
            this.window = window;
            chats = new ObservableCollection<Chats>();
            
        }
        public void MessageLog()
        {

            program.Dispatcher.Invoke(() =>
            {

                chats = new ObservableCollection<Chats>();
                Debug.WriteLine("----");

                string messageText = bot.e.Message.Text;
                string name = bot.e.Message.Chat.FirstName;
                long id = bot.e.Message.Chat.Id;

                string text = $"{DateTime.Now.ToLongTimeString()}: {id} {name} {messageText}";

                Debug.WriteLine(text);

                if (chats.Count != 0)
                {
                    Chats chat = chats.FirstOrDefault(x => x.Id == bot.e.Message.Chat.Id);
                    if (chat != null)
                    {

                    }
                    else
                    {
                        chats.Add(new Chats(id, name, messageText, DateTime.Now));
                    }
                }
                
                //if (chats.Id.)
                //{

                //}
                
                
            });

        }

    }
}
