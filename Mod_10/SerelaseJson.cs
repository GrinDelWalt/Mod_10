using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod_10
{
    class SerelaseJson
    {
        public void RecordingJson(ObservableCollection<Chat> listChats, ObservableCollection<Message> listMessages)
        {
            JObject messenger = new JObject();

            JArray users = new JArray();
            JArray messages = new JArray();

            messenger["Messenger"] = true;
            for (int userNumber = 0; userNumber < listChats.Count; userNumber++)
            {
                var chat = listChats[userNumber];
                JObject user = new JObject
                {
                    ["User_Name"] = chat.UserName,
                    ["User_Id"] = chat.Id,
                    ["Number_messages"] = chat.MessageCollection.Count,
                };

                for (int numbeMessagesr = 0; numbeMessagesr < chat.MessageCollection.Count; numbeMessagesr++)
                {
                    var messegesUser = listMessages.FirstOrDefault(x => x.Name == chat.UserName);
                    JObject message = new JObject
                    {
                        ["Name"] = messegesUser.Name,
                        ["Message_Text"] = messegesUser.Text,
                        ["Data_Message"] = messegesUser.Date,
                    };
                    messages.Add(message);
                }
                user["Message_Collection"] = messages;
                users.Add(user);
            }
            messenger["Messenger"] = users;

            string json = messenger.ToString();

            File.WriteAllText("Messages_User.json", json);
        }
    }
}

