using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Web12.Core.Entities;
using MISA.Fresher.Web12.Core.Exceptions;
using MISA.Fresher.Web12.Core.Interfaces.Infrastructure;
using MISA.Fresher.Web12.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MISA.Fresher.Web12.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : MISABaseController<Employee>
    {
        #region Fields
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;
        #endregion

        #region Contructor
        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(employeeRepository, employeeService)
        {
            this._employeeRepository = employeeRepository;
            this._employeeService = employeeService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy ra mã thêm mới tiếp theo
        /// </summary>
        /// <returns></returns>
        /// Created: HoaiPT(11/02/2022)
        [HttpGet("CodeNew")]
        public string GetHoa()
        {
            return _employeeRepository.GetEmployeeCodeNew();
        }


        [HttpGet("ExportExcel")]
        public string ExportExcel()
        {
            return "hoa";/*string)_employeeService.exportExcel();*/
        }

        //[HttpGet("exportv2")]
        //public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        //{
        //    // query data from database  
        //    await Task.Yield();
        //    var list = new List<Employee>()
        //    {
        //        new Employee { EmployeeCode = "catcher", Address = "18" },
        //          new Employee { EmployeeCode = "haooo", Address = "sfsf18" },
        //    };
        //    var stream = new MemoryStream();

        //    using (var package = new ExcelPackage(stream))
        //    {
        //        var workSheet = package.Workbook.Worksheets.Add("Sheet1");
        //        workSheet.Cells.LoadFromCollection(list, true);
        //        package.Save();
        //    }
        //    stream.Position = 0;
        //    string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

        //    //return File(stream, "application/octet-stream", excelName);  
        //    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        //}

        //private readonly IHostingEnvironment _hostingEnvironment;

        //public EPPlusController(IHostingEnvironment hostingEnvironment)
        //{
        //    this._hostingEnvironment = hostingEnvironment;
        //}

        //[HttpGet("export")]
        //public async Task<DemoResponse<string>> Export(CancellationToken cancellationToken)
        //{
        //    string folder = _hostingEnvironment.WebRootPath;
        //    string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
        //    string downloadUrl = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, excelName);
        //    FileInfo file = new FileInfo(Path.Combine(folder, excelName));
        //    if (file.Exists)
        //    {
        //        file.Delete();
        //        file = new FileInfo(Path.Combine(folder, excelName));
        //    }

        //    // query data from database  
        //    await Task.Yield();

        //    var list = new List<Employee>()
        //    {
        //        new Employee { EmployeeCode = "catcher", Address = "18" },
        //          new Employee { EmployeeCode = "haooo", Address = "sfsf18" },
        //    };


        //    using (var package = new ExcelPackage(file))
        //    {
        //        var workSheet = package.Workbook.Worksheets.Add("Sheet1");
        //        workSheet.Cells.LoadFromCollection(list, true);
        //        package.Save();
        //    }

        //    return DemoResponse<string>.GetResult(0, "OK", downloadUrl);
        //}


        [HttpGet("excel")]
        public IActionResult exportExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("User");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "1";
                worksheet.Cell(currentRow, 2).Value = "2";
                worksheet.Cell(currentRow, 3).Value = "3";
                worksheet.Cell(currentRow, 4).Value = "4";


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");

                }
            }

        }




        #endregion






    }
}
