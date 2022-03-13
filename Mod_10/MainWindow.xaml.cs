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
        ReadMessage read;


        public MainWindow()
        {
            InitializeComponent();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                TelegraBotHelper hlp = new TelegraBotHelper(logList, this);
                hlp.GetUpdates();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            
        }
        private void ButtonMessegePush_Click(object sender, RoutedEventArgs e)
        {

        }

        private void logList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var log = logList.SelectedItems;
            List<string> itemList = new List<string>();
            foreach (var item in log)
            {
                itemList.Add(Convert.ToString(item));
            }
            //var logAp = log.ToString
            //List<string> items = new List<string>();

            //while (logAp.MoveNext())
            //{
            //    string item = Convert.ToString(logAp.Current);
            //    items.Add(item);
            //}
        }
        
        
    }
}
