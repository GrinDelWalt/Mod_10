using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace Mod_10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TelegraBotHelper _hlp;

        private long _id;
        public MainWindow()
        {
            InitializeComponent();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _hlp = new TelegraBotHelper(this, logList);
            _hlp.GetUpdates();
        }

        private void logList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (idBox.Text != "")
            {
                _id = Convert.ToInt64(idBox.Text);
                chatList.ItemsSource = _hlp.GetMessageCollection(_id);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessagePushChat();
        }

        private void messageAdminBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessagePushChat();
            }
        }
        private void MessagePushChat()
        {
            string text = messageAdminBox.Text;

            if (text != "")
            {
                _hlp.PushBotMessageAdmin(text, _id);
                _hlp.PushCollectionAdmin(text, _id);
            }
            messageAdminBox.Clear();
        }
    }
}
