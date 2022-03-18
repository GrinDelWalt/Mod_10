using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerAPI
{
	public class Contact
	{
		private int id;
		private User[] users;

		public Contact()
		{

		}

		public Contact(int id, User u1, User u2)
		{
			this.id = id;
			this.users = new User[] { u1, u2 };
		}

		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public User[] Users
		{
			get
			{
				return this.users;
			}
		}
	}
}
