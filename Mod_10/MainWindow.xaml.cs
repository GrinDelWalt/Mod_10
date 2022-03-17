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
        //private TelegramBot _bot;

        private TelegraBotHelper _hlp;
        public MainWindow()
        {
            InitializeComponent();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _hlp = new TelegraBotHelper(this, logList);
            _hlp.GetUpdates();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //_bot.StopBot();
        }

        private void ButtonMessegePush_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void logList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            long id = Convert.ToInt64(idBox.Text);
            chatBox.ItemsSource = _hlp.GetMessageCollection(id);
        }
    }
}
