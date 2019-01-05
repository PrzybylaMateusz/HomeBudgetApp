using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace HomeBudgetRazor.Pages
{
    public class ImportExportModel : PageModel
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly Models.HomeBudgetRazorContext _context;
        public ImportExportModel(IHostingEnvironment hostingEnvironment, Models.HomeBudgetRazorContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostExport()
        {
            var currentUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var expenses = from e in _context.Expense
                           where e.OwnerID == currentUser
                           select e;

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"demo.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Demo");

                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("Date of expense");
                row.CreateCell(1).SetCellValue("Amount");
                row.CreateCell(2).SetCellValue("Category");
                row.CreateCell(2).SetCellValue("Description");

                var i = 1;
                foreach(var expense in expenses)
                {
                    row = excelSheet.CreateRow(i);
                    row.CreateCell(0).SetCellValue(expense.DateOfExpense);
                    row.CreateCell(1).SetCellValue(expense.Amount.ToString());
                    row.CreateCell(2).SetCellValue(expense.Category);
                    row.CreateCell(2).SetCellValue(expense.Description);
                    i++;
                }

                workbook.Write(fs);
            }

            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }
    }
}