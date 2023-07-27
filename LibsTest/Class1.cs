using MiniExcelLibs;
using System.IO;

namespace LibsTest
{
    public class ExcelHelper
    {


        public static MemoryStream ExportExcel()
        {
            var memoryStream = new MemoryStream();


            var ms = new MemoryStream();
            var projects = new[]
            {
         new {Name = "MiniExcel",Link="https://github.com/shps951023/MiniExcel",Star=146, CreateTime=new DateTime(2021,03,01)},
         new {Name = "HtmlTableHelper",Link="https://github.com/shps951023/HtmlTableHelper",Star=16, CreateTime=new DateTime(2020,02,01)},
         new {Name = "PocoClassGenerator",Link="https://github.com/shps951023/PocoClassGenerator",Star=16, CreateTime=new DateTime(2019,03,17)}
            };
            var value = new
            {
                User = "ITWeiHan",
                Projects = projects,
                TotalStar = projects.Sum(s => s.Star)
            };
            memoryStream.SaveAs(projects);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
    }
}