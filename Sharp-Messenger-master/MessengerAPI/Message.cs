using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MessengerAPI
{
	public class Message
	{
		private int id;
		private string text;
		private User sender;
		private User reciever;
		private Contact contact;

		public Message()
		{

		}

		public Message(int id, string text, User sender, User reciever, Contact contact)
		{
			this.id = id;
			this.text = text;
			this.sender = sender;
			this.reciever = reciever;
			this.contact = contact;
		}

		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public string Text
		{
			get
			{
				return this.text;
			}
		}

		public User Sender
		{
			get
			{
				return this.sender;
			}
		}

		public User Reciever
		{
			get
			{
				return this.reciever;
			}
		}

		public Contact Contact
		{
			get
			{
				return this.contact;
			}
		}
	}
}
