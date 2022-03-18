using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessengerAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		// GET: api/User
		[HttpGet]
		public IEnumerable<User> Get()
		{
			return new User[] 
			{ 
				new User(1, "Evgenii", "admin", "12345"),
				new User(2, "Victoria", "vika", "12345"),
				new User(3, "Igor", "igor", "12345"),
				new User(4, "Boris", "boris", "12345"),
			};
		}

		// GET: api/User/5
		[HttpGet("{id}", Name = "Get")]
		public string Get(int id)
		{
			return "value";
		}

		// POST: api/User
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT: api/User/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
