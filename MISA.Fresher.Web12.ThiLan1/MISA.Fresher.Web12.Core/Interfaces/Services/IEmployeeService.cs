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
    }
}
