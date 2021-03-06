using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telegram.Bot;

namespace Mod_10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ReadMessage read;
        

        public MainWindow()
        {
            
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                TelegraBotHelper hlp = new TelegraBotHelper(this);
                hlp.GetUpdates();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            
        }
        private void ButtonMessegePush_Click(object sender, RoutedEventArgs e)
        {

        }

        private void logList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            long id = Convert.ToInt64(idBox.Text);

            Chats chat = read.Chats.FirstOrDefault(x => x.Id == id);
            chatBox.ItemsSource = chat.MessageCollection;
        }
    }
}
