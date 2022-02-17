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

        public IEnumerable<string> GetAllEmployeeCode()
        {
            //Thực hiện khởi tạo kết nối và sau khi làm xong là nó tự ngắt kết nối luôn
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                //Thực hiện truy vấn dữ liệu trong database
                var enities = SqlConnection.Query<string>($"SELECT * FROM View_AllEmployeeCode;");
                return enities;
            }

        }
        public int DeleteManyId(List<string> listEmployeeId)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();

                var sqlValue = new StringBuilder();
                string delimiter = "";
                for (int i = 0; i < listEmployeeId.Count; i++)
                {
                    var employeeIdName = $"@EmployeeId{i}";
                    parameters.Add(employeeIdName, listEmployeeId[i]);
                    sqlValue.Append($"{delimiter}EmployeeId = {employeeIdName}");
                    delimiter = " || ";
                }

                //Viết câu lệnh sql
                var sqlCommand = $"DELETE FROM Employee WHERE {sqlValue.ToString()}";

                //Thực hiện xóa hàng loạt
                var res = SqlConnection.Execute(sqlCommand, param: parameters);
                return res;
            }
        }
        public object GetPaging(int pageIndex, int pageSize, string searchText)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sql = $"Proc_GetEmployeePaging";
                var parameters = new DynamicParameters();
                parameters.Add("@m_SearchText", searchText);
                parameters.Add("@m_PageIndex", pageIndex);
                parameters.Add("@m_PageSize", pageSize);

                parameters.Add("@m_TotalRecord", direction: System.Data.ParameterDirection.Output);
                parameters.Add("@m_TotalPage", direction: System.Data.ParameterDirection.Output);

                var entites = SqlConnection.Query(sql, param:parameters, commandType:System.Data.CommandType.StoredProcedure);//Thực hiện lấy các bản ghi
                var totalRecord = parameters.Get<int>("@m_TotalRecord");//Lấy tổng số bản ghi
                var totalPage = parameters.Get<int>("@m_TotalPage");//Lấy tổng số trang

                return new
                {
                    Data = entites,//Trả về bảng
                    TotalRecord = totalRecord,//Trả về tổng số bản ghi
                    TotalPage = totalPage//Trả về tổng số trang
                };

            }
        }
    }
}
