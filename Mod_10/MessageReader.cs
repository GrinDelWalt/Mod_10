﻿using System;
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

        private TelegraBotHelper _hlp;

        public ObservableCollection<Chat> Chats { get; set; }

        public ObservableCollection<Message> messages { get; set; }

        public MessageReader(MainWindow window, ListBox logList)
        {
            _window = window;
            Chats = new ObservableCollection<Chat>();
            _logList = logList;
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

            string date = DateTime.Now.ToLongDateString();
            
            _window.Dispatcher.Invoke(() =>
            {
                Chat chat = Chats.FirstOrDefault(x => x.Id == id);
                int index = Chats.IndexOf(chat);

                if (chat != null)
                {
                    chat.MessageCollection.Add(new Message(textMsg, date, name));
                    Chats[index] = chat;
                }
                else
                {
                    Chat newChat = new Chat(id, name);
                    newChat.Write(textMsg, date);
                    Chats.Add(newChat);
                }

                _logList.ItemsSource = Chats;
            });
        }

        /// <summary>
        /// Get message collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Message> GetMessageCollection(long id)
        {
            Chat chat = Chats.FirstOrDefault(x => x.Id == id);
            return chat.MessageCollection;
        }
    }
}
