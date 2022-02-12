using ClosedXML.Excel;
using MISA.Fresher.Web12.Core.Entities;
using MISA.Fresher.Web12.Core.Exceptions;
using MISA.Fresher.Web12.Core.Interfaces.Infrastructure;
using MISA.Fresher.Web12.Core.Interfaces.Services;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Web12.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Property
        IEmployeeRepository _employeeRepository;
        #endregion


        #region Contructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion


        #region Methods
        
        protected override void ValidateInsertCustomer(Employee employee)
        {
            //2. Ngày sinh không được lớn hơn ngày hiện tại
            this.ValidateDateOfBirth(employee.DateOfBirth);
        }
        protected override void ValidateUpdateCustomer(Guid employeeId, Employee employee)
        {
            //1. Ngày sinh không được lớn hơn ngày hiện tại
            this.ValidateDateOfBirth(employee.DateOfBirth);
        }

        /// <summary>
        /// Thực hiện Validate ngày sinh không được lớn hơn thời gian hiện tại
        /// </summary>
        /// <param name="dateTime">Ngày tháng năm sinh</param>
        /// <exception cref="MISAValidateException"></exception>
        private void ValidateDateOfBirth(DateTime? dateTime)
        {
            if (dateTime > DateTime.Now)
            {
                throw new MISAValidateException(String.Format(Core.Resourcs.ResourceVN.ErrorDateOfBirth));
            }
        }
        #endregion

    }
}
