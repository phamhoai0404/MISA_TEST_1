using Dapper;
using MISA.Fresher.Web12.Core.Entities;
using MISA.Fresher.Web12.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Web12.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public IEnumerable<Employee> GetDataExport()
        {
            //Thực hiện khởi tạo kết nối và sau khi làm xong là nó tự ngắt kết nối luôn
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Proc_ExportEmployee";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@m_totalRecord", direction: System.Data.ParameterDirection.Output);

                //Thực hiện truy vấn dữ liệu trong database
                var enities = SqlConnection.Query<Employee>(sql, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
              
                //var totalRecord = parameters.Get<Employee>("@m_totalRecord");

                return enities;
               
            }
        }

        public string GetEmployeeCodeNew()
        {
            //var listEmployee = this.GetAll();
            //var hoa = new
            //{
            //    xinh = true,

            //};
            return "hoa";
        }
    }
}
