using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerAPI
{
	public class User
	{
		private int id;
		private string name;
		private string login;
		private string password;
		private string image;

		public User()
		{

		}

		public User(int id, string name, string login, string password)
		{
			this.id = id;
			this.name = name;
			this.login = login;
			this.password = password;
		}

		public bool Login(string login, string password)
		{
			if(login == this.login && password == this.password)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool SetImage(string path)
		{
			bool success = false;



			return success;
		}

		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public string Image
		{
			get
			{
				return this.image;
			}
		}
		
	}
}
