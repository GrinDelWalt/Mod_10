using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MessengerApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Messenger app;

		public MainWindow()
		{
			InitializeComponent();
			app = new Messenger();

			ContactsList.ItemsSource = app.Contacts;
			MessagesList.ItemsSource = app.Chat;
		}

		//Обработчик нажатия на кнопку Login
		private void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			//Пока будут использоваться тестовые данные
			if(LoginBox.Text == "admin" && PasswordBox.Password == "12345")
			{
				//Если логин и пароль верные, то переходим на другой экран
				Open(ContactsScreen);
			}
			else
			{
				//Иначе выводим сообщение об ошибке авторизации
				LoginMessageBlock.Text = "Wrong login or password!";
				LoginMessageBlock.Visibility = Visibility.Visible;
			}
		}

		//Метод для окрытия другого экрана
		private void Open(Border screen)
		{
			//Делаем все экраны невидимыми
			LoginScreen.Visibility = Visibility.Hidden;
			ContactsScreen.Visibility = Visibility.Hidden;
			ChatScreen.Visibility = Visibility.Hidden;

			//Делаем видимым необходиый экран
			screen.Visibility = Visibility.Visible;
		}

private void ContactsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
	//Метод вызывается, когда меняется индекс выделенного элемента
	//При выделении элемент списка будет подсвечиваться
	//Чтобы убрать это, мы будем менять индекс на -1
	//Чтобы метод не срабатывал повторно, мы будем проверять, чтобы индекс был больше или равен 0
	if(ContactsList.SelectedIndex >= 0)
	{
		//Этот код будет написан позже
		//app.ChangeContact(ContactsList.SelectedIndex);
		ContactsList.SelectedIndex = -1;

		//app.LoadChat();

		MessagesList.ItemsSource = app.Chat;
		ChatName.Text = app.CurrentContact.Name;

		Open(ChatScreen);
	}
	
}

		private void SendButton_Click(object sender, RoutedEventArgs e)
		{

			string text = "";

			if(!string.IsNullOrEmpty(MessageBox.Text))
			{
				
				text = MessageBox.Text.Trim();
			}

			if(!string.IsNullOrEmpty(text))
			{
				User reciever;

				if (app.CurrentContact.Users[0].ID == app.User.ID)
				{
					reciever = app.CurrentContact.Users[0];
				}
				else
				{
					reciever = app.CurrentContact.Users[1];
				}

				Message message = new Message(0, text, app.User, reciever, app.CurrentContact, app.User.ID);

				bool result = app.Send(message);

				if(result)
				{
					MessageBox.Text = "";
					app.Update();
				}
				
			}
			else
			{

			}

			
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			Open(ContactsScreen);
		}
	}
}
