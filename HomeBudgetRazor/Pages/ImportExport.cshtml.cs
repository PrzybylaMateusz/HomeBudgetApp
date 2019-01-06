using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetRazor.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPOI.HSSF.UserModel;
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
                row.CreateCell(3).SetCellValue("Description");

                var i = 1;
                foreach(var expense in expenses)
                {
                    row = excelSheet.CreateRow(i);
                    row.CreateCell(0).SetCellValue(expense.DateOfExpense.ToString("dd/MM/yyyy"));
                    
                    row.CreateCell(1).SetCellValue(expense.Amount.ToString());
                    row.CreateCell(2).SetCellValue(expense.Category);
                    row.CreateCell(3).SetCellValue(expense.Description);
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

        public async Task<IActionResult> OnPostImport()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }

                    

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        var expense = new Expense();
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                        if (row.GetCell(row.FirstCellNum) != null)
                            expense.DateOfExpense = Convert.ToDateTime(row.GetCell(row.FirstCellNum).ToString());
                        if (row.GetCell(row.FirstCellNum+1) != null)
                            expense.Amount = Convert.ToDecimal(row.GetCell(row.FirstCellNum+1).ToString());
                        if (row.GetCell(row.FirstCellNum + 2) != null)
                            expense.Category = row.GetCell(row.FirstCellNum + 2).ToString();
                        if (row.GetCell(row.FirstCellNum + 3) != null)
                            expense.Description= row.GetCell(row.FirstCellNum + 3).ToString();

                        //expense.Id = _context.Expense.Select(x => x.Id).Max() + 1;

                        var currentUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                        expense.OwnerID = currentUser;
                        _context.Expense.Add(expense);
                    }


                    await _context.SaveChangesAsync();                 
                }
            }
            return RedirectToPage("./Index");
        }
    }
}