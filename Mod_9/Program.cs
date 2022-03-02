using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

namespace Mod_9
{
    public class Program
    {
        
        public static void Main()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                TelegraBotHelper hlp = new TelegraBotHelper();
                hlp.GetUpdates();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
 