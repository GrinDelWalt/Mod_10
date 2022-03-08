using System;
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
        ReadMessage read;


        public MainWindow()
        {
            InitializeComponent();
            read = new ReadMessage(this);
            logList.ItemsSource = read.Chats;
            read.StartBot();
        }
        private void ButtonMessegePush_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
