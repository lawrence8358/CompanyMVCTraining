using System;
using System.Linq;
using System.Collections.Generic;
	
namespace _20170703MVC.Models
{   
	public  class OrderLineRepository : EFRepository<OrderLine>, IOrderLineRepository
	{

	}

	public  interface IOrderLineRepository : IRepository<OrderLine>
	{

	}
}