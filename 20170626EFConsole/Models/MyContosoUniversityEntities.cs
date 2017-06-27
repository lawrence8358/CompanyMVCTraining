using _20170626EFConsole.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20170626EFConsole.Models
{
    public partial class ContosoUniversityEntities : DbContext
    {
        public override int SaveChanges()
        {
            var entities = this.ChangeTracker.Entries();

            foreach (var entry in entities)
            {
                Console.WriteLine("Entity Name : {0}", entry.Entity.GetType().FullName);
                Console.WriteLine("Status : {0}", entry.State);

                if (entry.Entity is Course)
                {
                    //特定資料表要做的事情
                }

                if (entry.State == EntityState.Modified)
                {
                    //這裡若對應不到屬性，並不會掛掉
                    entry.CurrentValues.SetValues(new { ModifedOn = DateTime.Now });
                }
            }

            return base.SaveChanges();
        }
    }
}
