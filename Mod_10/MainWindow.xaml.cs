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
        private TelegramBot _bot;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                _bot = new TelegramBot(this);
                _bot.StartBot();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _bot.StopBot();
        }

        private void ButtonMessegePush_Click(object sender, RoutedEventArgs e)
        {

        }

        private void logList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            long id = Convert.ToInt64(idBox.Text);
            chatBox.ItemsSource = _bot.GetMessageCollection(id);
        }
    }
}
