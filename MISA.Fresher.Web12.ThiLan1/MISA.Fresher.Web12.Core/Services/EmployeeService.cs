
using Dapper;
using MISA.Fresher.Web12.Core.Entities;
using MISA.Fresher.Web12.Core.Exceptions;
using MISA.Fresher.Web12.Core.Interfaces.Infrastructure;
using MISA.Fresher.Web12.Core.Interfaces.Services;
using System.Text;
using System.Text.RegularExpressions;

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

            //3. Phải có định dạng mẫu employee NV-[]
            this.ValidateEmployeeCode(employee.EmployeeCode);
        }
        protected override void ValidateUpdateCustomer(Guid employeeId, Employee employee)
        {
            //2. Ngày sinh không được lớn hơn ngày hiện tại
            this.ValidateDateOfBirth(employee.DateOfBirth);

            //3. Phải có định dạng mẫu employee NV-[]
            this.ValidateEmployeeCode(employee.EmployeeCode);
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

        /// <summary>
        /// Thực hiện validate Mã phải có định dạng NV-
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: HoaiPT(17/02/2022)
        private void ValidateEmployeeCode(string employeeCode)
        {
            string strRegex = @Core.Resourcs.EntitiesVN.EmployeeVN.RegexEmployeeCode;

            Regex regexCode = new Regex(strRegex);
            if (!regexCode.IsMatch(employeeCode))
            {
                throw new MISAValidateException(String.Format(Core.Resourcs.ResourceVN.ErrorFormatCode));
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

        public object GetPagingService(string searchText, int pageSize, int pageIndex)
        {
            //Nếu mà không tồn tại searchText thì gán giá trị bằng rỗng
            if (searchText == null)
                searchText = "";

            //Nếu không tồn tại thứ tự trang hoặc <1 thì mặc định bằng 1
            if(pageIndex == null || pageIndex < 1)
            {
                pageIndex = 1;
            }

            return _employeeRepository.GetPaging(pageIndex, pageSize, searchText); ;
        }
        #endregion

    }
}
