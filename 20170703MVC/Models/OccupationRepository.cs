using System;
using System.Linq;
using System.Collections.Generic;
	
namespace _20170703MVC.Models
{   
	public  class OccupationRepository : EFRepository<Occupation>, IOccupationRepository
	{

	}

	public  interface IOccupationRepository : IRepository<Occupation>
	{

	}
}