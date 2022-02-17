using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Web12.Core.Entities;
using MISA.Fresher.Web12.Core.Exceptions;
using MISA.Fresher.Web12.Core.Interfaces.Infrastructure;
using MISA.Fresher.Web12.Core.Interfaces.Services;



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

        #region Methods Public
        /// <summary>
        /// Lấy ra mã thêm mới tiếp theo
        /// </summary>
        /// <returns></returns>
        /// Created: HoaiPT(11/02/2022)
        [HttpGet("CodeNew")]
        public IActionResult GetCodeNew()
        {
            try
            {
                var res = _employeeService.GetEmployeeCodeNew();
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return this.AllException(ex, null);
            }
           
        }

        /// <summary>
        /// Thực hiện Xóa nhiều do Delete không truyền qua Body được nên là phải thông qua Post
        /// </summary>
        /// <param name="listEmployeeId">Truyền vào một mảng từ client truyền sang</param>
        /// <returns></returns>
        /// Created: HoaiPT(14/02/2022)
        [HttpPost("DeleteMany")]
        public IActionResult DeleteMany(List<string> listEmployeeId)
        {
            try
            {
                var res = _employeeService.DeleteManyService(listEmployeeId);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return this.AllException(ex, null);
            }
        }

        /// <summary>
        /// Thực hiện phân trang lấy dữ liệu với từ khóa tìm kiếm
        /// </summary>
        /// <returns></returns>
        /// Created: HoaiPT(17/02/2022)
        [HttpGet("GetPaging")]
        public IActionResult GetPaging(int pageIndex, int pageSize, string? searchText)
        {
            try
            {
                var res = _employeeService.GetPagingService(searchText,pageSize,pageIndex);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return this.AllException(ex, null);
            }
        }

        /// <summary>
        /// Thực hiện xuất ra file excel danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        /// Created: HoaiPT(14/02/2022)
        [HttpGet("Excel")]
        public IActionResult exportExcel()
        {
            //Thực hiện tạo ra file excel mới và sau khi làm xong  đóng lại luôn
            using (var workbook = new XLWorkbook())
            {
                //Tạo ra sheet mới trong file excel có tên là DANH SÁCH NHÂN VIÊN
                var worksheet = workbook.Worksheets.Add(Core.Resourcs.EntitiesVN.EmployeeVN.TitleExcelExport);

                //Thực hiện style cho title
                var title = worksheet.Range("A1:I1");
                title.Value = Core.Resourcs.EntitiesVN.EmployeeVN.TitleExcelExport;
                title.Merge();
                this.StyleTitle(title, 16, "Arial");

                //Cách ra một cell và gộp các cell ấy lại
                var distance = worksheet.Range("A2:I2");
                distance.Merge();

                //Thực hiện style cho title của table
                var titleTable = worksheet.Range("A3:I3");
                titleTable.Style.Fill.BackgroundColor = XLColor.Gray;//Thiết lập màu cho background
                this.StyleBorder(titleTable);//Thiết lập các border cho cell của title
                this.StyleTitle(titleTable, 10, "Arial");
                this.SetValueTitle(worksheet, 3);

                //Thực hiện lấy dữ liệu từ database gồm các list Emloyee
                var listEmployee = _employeeRepository.GetDataExport();
                int index = 4;//dòng đầu tiên của dữ liệu
                int number = 1;//Dùng đếm số thứ tự của các dòng
                foreach (var emloyee in listEmployee)
                {
                    worksheet.Cell(index, 1).Value = number++;
                    worksheet.Cell(index, 2).Value = emloyee.EmployeeCode;
                    worksheet.Cell(index, 3).Value = emloyee.FullName.ToUpper();//Thực hiện In hoa tất cả
                    worksheet.Cell(index, 4).Value = emloyee.GenderName;
                    worksheet.Cell(index, 5).Value = emloyee.DateOfBirth;
                    worksheet.Cell(index, 6).Value = emloyee.PositionName;
                    worksheet.Cell(index, 7).Value = emloyee.DepartmentName;
                    worksheet.Cell(index, 8).Value = $"'{emloyee.BankAccountNumber}";//Để excel nhận biết được đây là string chứ không phải số
                    worksheet.Cell(index, 9).Value = emloyee.BankName;

                    index++;//Sau khi nhập dữ liệu của một Employee thì tiếp tục nhập  dòng kế tiếp
                }
                var rangeData = worksheet.Range($"A4:I{index - 1}");//Tất cả dữ 
                this.StyleBorder(rangeData);//Thiết lập các border cho cell của data 
                rangeData.Style.Font.SetFontName("Times New Roman");//Thiết lập font chữ cho từng cell của data là dạng Times New Roman

                //Căn giữa cho dòng excel
                var rangeDateOfBirth = worksheet.Range($"E4:E{index - 1}");
                rangeDateOfBirth.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);


                //Thiết lập độ rộng cho các cột của sheet A, B, C
                this.SetColumnWidth(worksheet);

                using (var stream = new MemoryStream())
                {
                    //Lưu lại file excel
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    //Trả về cho client file excel
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Core.Resourcs.EntitiesVN.EmployeeVN.FileNameExcel);

                }
            }

        }

        #endregion

        #region Method Private For Style Export Excel

        /// <summary>
        /// Thực hiện style cho border của cell excel
        /// </summary>
        /// <param name="titleTable"></param>
        /// Created: HoaiPT(14/02/2022)
        private void StyleBorder(IXLRange titleTable)
        {
            titleTable.Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);
            titleTable.Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            titleTable.Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
            titleTable.Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        }

        /// <summary>
        /// Thực hiện style cho tiêu đề với cỡ chữ, kiểu chữ
        /// </summary>
        /// <param name="titleTable"></param>
        /// <param name="fontSize">Cỡ chữ</param>
        /// <param name="fontName">Kiểu chữ</param>
        /// Created: HoaiPT(14/02/2022)
        private void StyleTitle(IXLRange titleTable, int fontSize, string fontName)
        {
            titleTable.Style.Font.Bold = true;
            titleTable.Style.Font.FontSize = fontSize;
            titleTable.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            titleTable.Style.Font.SetFontName(fontName);
        }

        /// <summary>
        /// Thiết lập giá trị cho title table
        /// </summary>
        /// <param name="worksheet">Sheet thiết lập</param>
        /// <param name="first">Dòng bắt đầu</param>
        /// Created: HoaiPT(14/02/2022)
        private void SetValueTitle(IXLWorksheet worksheet, int first)
        {
            worksheet.Cell(first, 1).Value = Core.Resourcs.EntitiesVN.EmployeeVN.Number;
            worksheet.Cell(first, 2).Value = Core.Resourcs.EntitiesVN.EmployeeVN.EmployeeCode;
            worksheet.Cell(first, 3).Value = Core.Resourcs.EntitiesVN.EmployeeVN.FullName;
            worksheet.Cell(first, 4).Value = Core.Resourcs.EntitiesVN.EmployeeVN.GenderName;
            worksheet.Cell(first, 5).Value = Core.Resourcs.EntitiesVN.EmployeeVN.DateOfBirth;
            worksheet.Cell(first, 6).Value = Core.Resourcs.EntitiesVN.EmployeeVN.PositionName;
            worksheet.Cell(first, 7).Value = Core.Resourcs.EntitiesVN.EmployeeVN.DepartmentName;
            worksheet.Cell(first, 8).Value = Core.Resourcs.EntitiesVN.EmployeeVN.BankAccountNumber;
            worksheet.Cell(first, 9).Value = Core.Resourcs.EntitiesVN.EmployeeVN.BankName;
        }

        /// <summary>
        /// Thiết lập độ rộng của từng cột cell A, B, C
        /// </summary>
        /// <param name="worksheet"></param>
        /// Created: HoaiPT(14/02/2022)
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

        /// <summary>
        /// Thiết ngoại lệ exception sẽ vào đây
        /// </summary>
        /// <param name="worksheet"></param>
        /// Created: HoaiPT(07/02/2022)
        private IActionResult AllException(Exception ex, Object? enity)
        {
            if (typeof(MISAValidateException) == ex.GetType())
            {
                var respo = new
                {
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    data = enity,
                };
                return StatusCode(400, respo);
            }
            else
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Resourcs.ResourceVN.ErrorException
                };

                return StatusCode(500, response);
            }

        }
        #endregion
    }
}