using _20170626EFConsole.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20170626EFConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ContosoUniversityEntities())
            {
                //0. 輸出EF產生的SQL Log
                //db.Database.Log = (log) => { Console.WriteLine(log); };

                //查詢資料(db);
                //新增資料(db); 
                //更新資料(db); 
                //刪除資料(db);

                //查詢資料(db); 

                //關閉延遲載入查詢(db);

                //新增關聯資料表資料(db);

                查看Entry狀態(db);
            }
        }

        private static void 查看Entry狀態(ContosoUniversityEntities db)
        {
            var item = db.Course.Find(1);

            var ce = db.Entry(item);
            Console.WriteLine("修改前 : " + ce.State);

            item.Credits++;
            var ce2 = db.Entry(item);   //這邊很怪，若再取一次資料，會影響ce原本的資料
            Console.WriteLine("修改後1 : " + ce.State);
            Console.WriteLine(System.Object.ReferenceEquals(ce, ce2));

            if (ce.State == System.Data.Entity.EntityState.Modified)
            {
                Console.WriteLine("本次執行修改資料");
            }
            db.SaveChanges();

            var ce3 = db.Entry(item);
            Console.WriteLine("儲存完畢 : " + ce.State);
        }

        private static void 新增關聯資料表資料(ContosoUniversityEntities db)
        {
            var course = db.Course.Find(1);
            //course.Person.Add(db.Person.Find(3)); //將原本的學生新增進去mapping table

            //底下新增一個學生和mapping table
            course.Person.Add(new Person { FirstName = "Lawrence", LastName = "Shen", Discriminator = "Hello" });
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //這裡才真的有辦法抓到資料驗證錯誤的訊息。
                throw ex;
            }

            // 若要刪除一樣要在關聯表找出來後再刪除，如下刪除
            //course.Person.Remove(db.Person.Find(32));
            //db.SaveChanges(); 
        }

        private static void 關閉延遲載入查詢(ContosoUniversityEntities db)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var depts = db.Department.Include("Course").ToList();
            foreach (var dept in depts)
            {
                Console.WriteLine("部門 : " + dept.Name);

                foreach (var course in dept.Course) //若關閉延遲載入，這裡的資料要自己抓取Include
                {
                    Console.WriteLine("\t 課程(" + course.Credits.ToString() + ") : " + course.Title + "\t " + course.CreateOn);
                }
            }
            Console.WriteLine("");
        }

        private static void 刪除資料(ContosoUniversityEntities db)
        {
            foreach (var item in db.Course.Where(p => p.CourseID >= 10 && p.CourseID <= 11))
            {
                db.Course.Remove(item);
            }
            db.SaveChanges();
            //使用底下的SQL指令來執行命令時，不須SaveChange，但是這樣一來就沒辦法Rollback了
            //db.Database.ExecuteSqlCommand("delete Course where CourseID>=@p0 and CourseID<=@p1", 10, 14);
        }

        private static void 更新資料(ContosoUniversityEntities db)
        {
            foreach (var item in db.Course.ToList())
            {
                item.Credits += 1;
            }
            db.SaveChanges();
        }

        private static void 新增資料(ContosoUniversityEntities db)
        {
            db.Course.Add(new Course
            {
                Title = "Hello " + DateTime.Now.ToString("yyyyMMddHHmmss"),
                Credits = 5,
                // 若資料庫有預設日期，但程式端沒有給，會出現錯誤，可透過EDM資料模型內的StoreGeneratedPattern = Computed，來解決
                // 但若設定該屬性後，該資料欄位將無法重程式端去修改
                // The conversion of a datetime2 data type to a datetime data type resulted in an out-of-range value.

                //DepartmentID = 5,  //與底下這個相同意思
                Department = db.Department.Find(5)
            });
            db.SaveChanges();
        }

        private static void 查詢資料(ContosoUniversityEntities db)
        {
            //1. Where 條件示範
            var data = db.Course.Where(o => o.Title.Contains("Git")).ToList();
            foreach (var item in data) Console.WriteLine(item.Title);
            Console.WriteLine("");

            //2. 導覽屬性示範
            var depts = db.Department.ToList();
            foreach (var dept in depts)
            {
                Console.WriteLine("部門 : " + dept.Name);

                foreach (var course in dept.Course)
                {
                    Console.WriteLine("\t 課程(" + course.Credits.ToString() + ") : " + course.Title + "\t " + course.CreateOn);
                }
            }
            Console.WriteLine("");
        }
    }
}
