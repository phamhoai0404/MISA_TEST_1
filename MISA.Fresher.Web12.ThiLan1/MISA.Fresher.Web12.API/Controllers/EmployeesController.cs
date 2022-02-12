using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Web12.Core.Entities;
using MISA.Fresher.Web12.Core.Exceptions;
using MISA.Fresher.Web12.Core.Interfaces.Infrastructure;
using MISA.Fresher.Web12.Core.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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

        [HttpGet("excel")]
        public IActionResult exportExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("DANH SÁCH NHÂN VIÊN");
           
                var title = worksheet.Range("A1:I1");
                title.Value = "DANH SÁCH NHÂN VIÊN";
                title.Merge();
                this.StyleTitle(title, 16, "Arial");

                var distance = worksheet.Range("A2:I2");
                distance.Merge();

                var titleTable = worksheet.Range("A3:I3");
                titleTable.Style.Fill.BackgroundColor = XLColor.Gray;
                this.StyleBorder(titleTable);
                this.StyleTitle(titleTable, 10, "Arial");
                this.SetValueTitle(worksheet, 3);

                var listEmployee =  _employeeRepository.GetDataExport();
                int index = 4; int number = 1;
                foreach (var emloyee in listEmployee)
                {
                    worksheet.Cell(index, 1).Value = number++;
                    worksheet.Cell(index, 2).Value = emloyee.EmployeeCode;
                    worksheet.Cell(index, 3).Value = emloyee.FullName.ToUpper();
                    worksheet.Cell(index, 4).Value = emloyee.GenderName;
                    worksheet.Cell(index, 5).Value = emloyee.DateOfBirth;
                    worksheet.Cell(index, 6).Value = emloyee.PositionName;
                    worksheet.Cell(index, 7).Value = emloyee.DepartmentName;
                    worksheet.Cell(index, 8).Value = $"'{emloyee.BankAccountNumber}";
                    worksheet.Cell(index, 9).Value = emloyee.BankName;

                    index++;
                }
                var rangeData = worksheet.Range($"A4:I{index-1}");
                this.StyleBorder(rangeData);
                rangeData.Style.Font.SetFontName("Times New Roman");

                this.SetColumnWidth(worksheet);

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");

                }
            }

        }
        private void StyleBorder(IXLRange titleTable)
        {
            titleTable.Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);
            titleTable.Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            titleTable.Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
            titleTable.Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        }
        private void StyleTitle(IXLRange titleTable, int fontSize, string fontName)
        {
            titleTable.Style.Font.Bold = true;
            titleTable.Style.Font.FontSize = fontSize;
            titleTable.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            titleTable.Style.Font.SetFontName(fontName);
        }
        private void SetValueTitle(IXLWorksheet worksheet, int first)
        {
            worksheet.Cell(first, 1).Value = "STT";
            worksheet.Cell(first, 2).Value = "Mã nhân viên";
            worksheet.Cell(first, 3).Value = "Tên nhân viên";
            worksheet.Cell(first, 4).Value = "Giới tính";
            worksheet.Cell(first, 5).Value = "Ngày sinh";
            worksheet.Cell(first, 6).Value = "Chức danh";
            worksheet.Cell(first, 7).Value = "Tên đơn vị";
            worksheet.Cell(first, 8).Value = "Số tài khoản";
            worksheet.Cell(first, 9).Value = "Tên ngân hàng";
        }
        private void SetColumnWidth(IXLWorksheet worksheet)
        {
            worksheet.Column("A").Width = 4;
            worksheet.Column("B").Width = 15;
            worksheet.Column("C").Width = 28;
            worksheet.Column("D").Width = 9;
            worksheet.Column("E").Width = 12;
            worksheet.Column("F").Width = 13;
            worksheet.Column("G").Width = 18;
            worksheet.Column("H").Width = 17;
            worksheet.Column("I").Width = 40;
        }



        #endregion






    }
}
