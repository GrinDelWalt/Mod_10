using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Telegram.Bot;

namespace Mod_10
{
    class TelegramBot
    {
        public MainWindow _window;
        public MessageReader _messageReader;

        public TelegramBotClient _client;
        private string _token;

        public TelegramBot(MainWindow window)
        {
            _token = File.ReadAllText(Environment.CurrentDirectory + @"\Token_bot.txt");
            _window = window;
            _messageReader = new MessageReader(_window);
        }
        public async void StartBot()
        {
            _client = new TelegramBotClient(_token);
            _client.OnMessage += _messageReader.MessageLog;
            _client.StartReceiving();

        }
        public void StopBot()
        {
            _client.StopReceiving();
        }
        public List<Message> GetMessageCollection(long id)
        {
            return _messageReader.GetMessageCollection(id);
        }
    }
}
