using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public MessageReader read;
        
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                TelegraBotHelper hlp = new TelegraBotHelper(logList, this);
                //hlp.GetUpdates();
                hlp.StartBot();
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message); 
            }
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
