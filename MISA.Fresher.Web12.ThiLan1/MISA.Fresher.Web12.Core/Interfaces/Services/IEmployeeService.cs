using MISA.Fresher.Web12.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Web12.Core.Interfaces.Services
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Thực hiện lấy ra mã nhân viên mới để phục vụ cho mã tự tăng khi thêm mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: HoaiPT(13/02/2022)
        string GetEmployeeCodeNew();

        /// <summary>
        /// Thực hiện xóa nhiều
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: HoaiPT(14/02/2022)
        int DeleteManyService(List<string> listEmployeeId);

        /// <summary>
        /// Thực hiện lấy dữ liệu và phân trang tìm kiếm
        /// </summary>
        /// <param name="searchText">Nhập vào từ tìm kiếm</param>
        /// <param name="pageSize">Nhập vào kích thước trang</param>
        /// <param name="pageIndex">Nhập vào thứ tự trang</param>
        /// <returns></returns>
        /// CreatedBy: HoaiPT(17/02/2022)
        object GetPagingService(string searchText, int pageSize, int pageIndex);
    }
}
