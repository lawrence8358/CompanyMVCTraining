using System;
using System.Linq;
using System.Collections.Generic;
	
namespace _20170703MVC.Models
{   
	public  class ClientRepository : EFRepository<Client>, IClientRepository
	{

	}

	public  interface IClientRepository : IRepository<Client>
	{

	}
}