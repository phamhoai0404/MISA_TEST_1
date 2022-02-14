
using Dapper;
using MISA.Fresher.Web12.Core.Entities;
using MISA.Fresher.Web12.Core.Exceptions;
using MISA.Fresher.Web12.Core.Interfaces.Infrastructure;
using MISA.Fresher.Web12.Core.Interfaces.Services;
using System.Text;

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


        public string GetEmployeeCodeNew()
        {
            List<string> listEmployeeCode = this._employeeRepository.GetAllEmployeeCode() as List<string>;

            var z = "";
            List<int> listNumberEmployeeCode = new List<int>();
            for (int i = 0; i < listEmployeeCode.Count; i++)
            {
                int u = int.Parse(listEmployeeCode[i].Substring(3));
                listNumberEmployeeCode.Add(u);
            }
            int k = listNumberEmployeeCode.Max();
            z += k+1;
            switch (z.Length)
            {
                case 1:
                    z = "NV-000" + z;
                    break;
                case 2:
                    z = "NV-00" + z;
                    break;
                case 3:
                    z = "NV-0" + z;
                    break;
                default:
                    z = "NV-" + z;
                    break;
            }
            return z;
        }

        public int DeleteManyService(List<string> listEmployeeId)
        {
            
            for (int i = 0; i < listEmployeeId.Count; i++)
                {
                    if (listEmployeeId[i].Length != 36)
                    {
                        listEmployeeId.RemoveAt(i);
                        i--;
                    }
                }
            int res = 0;
            if(listEmployeeId.Count > 0)
            {
                 res = _employeeRepository.DeleteManyId(listEmployeeId);
            }
            return res;
           
        }
        #endregion

    }
}
