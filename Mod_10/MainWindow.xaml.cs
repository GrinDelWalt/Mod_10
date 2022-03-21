using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace Mod_10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TelegraBotHelper _hlp;

        private long id;
        public MainWindow()
        {
            InitializeComponent();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _hlp = new TelegraBotHelper(this, logList);
            _hlp.GetUpdates();

        }


        private void ButtonMessegePush_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void logList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (idBox.Text != "")
            {
                id = Convert.ToInt64(idBox.Text);
            }
            chatBox.ItemsSource = _hlp.GetMessageCollection(id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text = messageAdminBox.Text;
           
            if (idBox.Text != "")
            {
                _hlp.PushBotMessageAdmin(text, id);
                _hlp.PushCollectionAdmin(text, id);
            }
            
        }
    }
}
