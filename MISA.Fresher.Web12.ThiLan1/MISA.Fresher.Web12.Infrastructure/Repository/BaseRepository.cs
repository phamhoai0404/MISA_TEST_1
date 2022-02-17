using Dapper;
using MISA.Fresher.Web12.Core.Interfaces.Infrastructure;
using MISA.Fresher.Web12.Core.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Web12.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity>: IBaseRepository<MISAEntity>
    {
        #region Property
        //1.Khai báo thông tin của CSDL
        protected string ConnectionString = Core.Resourcs.ResourceVN.ConnectionString;

        //2.Khởi tạo kết nối
        protected MySqlConnection SqlConnection;

        //3.Tên của Enity
        private string className = typeof(MISAEntity).Name;
        #endregion

        #region Methods

        #region Methods Base
        /// <summary>
        /// Lấy toàn bộ dữ liệu bảng
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: HoaiPT(24/01/2022)
        public IEnumerable<MISAEntity> GetAll()
        {
            //Thực hiện khởi tạo kết nối và sau khi làm xong là nó tự ngắt kết nối luôn
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                //Thực hiện truy vấn dữ liệu trong database
                var enities = SqlConnection.Query<MISAEntity>($"SELECT * FROM View_All{className};");
                return enities;
            }
        }

        /// <summary>
        /// Lấy dữ liệu theo id 
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: HoaiPT(24/01/2022)
        public MISAEntity GetById(Guid id)
        {
            //Thực hiện khởi tạo kết nối và sau khi làm xong là nó tự ngắt kết nối luôn
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"SELECT * FROM {className} WHERE {className}Id = @EnityId  ";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EnityId", id);

                var entity = SqlConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: parameters);

                return entity;
            }

        }

        /// <summary>
        /// Thực hiện xóa hàng theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// CreatedBy: HoaiPT(24/01/2022)
        public int Delete(Guid id)
        {
            //Thực hiện khởi tạo kết nối và sau khi làm xong là nó tự ngắt kết nối luôn
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                //Thực hiện xóa nhân viên
                var sqlcommand = $"DELETE FROM {className} WHERE {className}Id = @EnityId ";
                var res = SqlConnection.Execute(sqlcommand, param: new { EnityId = id });

                return res;
            }
        }

        /// <summary>
        /// Thực hiện thêm dữ liệu mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: HoaiPT(27/01/2022)
        /// Update: HoaiPT(07/02/2022)
        public int Insert(MISAEntity entity)
        {
            //Thực hiện khởi tạo kết nối và sau khi làm xong là nó tự ngắt kết nối luôn
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlColumNames = new StringBuilder();
                var sqlColumnValues = new StringBuilder();

                DynamicParameters parameters = new DynamicParameters();

                //Duyệt tất cả các property của đối tượng
                var props = typeof(MISAEntity).GetProperties();

                string delimiter = "";
                foreach (var prop in props)
                {
                    //Lấy tên của property
                    var propName = prop.Name;
                    var propValue = prop.GetValue(entity);

                    //Nếu nó là không phải là thuộc tính của sql thì bỏ vòng lặp này rồi tiếp tục vòng for
                    var map = Attribute.IsDefined(prop, typeof(NotMapSQL));
                    if (map == true) continue;

                    //Kiểm tra property có phải là khóa chính hay không 
                    var primaryKey = Attribute.IsDefined(prop, typeof(Primarykey));
                    if (primaryKey == true || propName == $"{className}Id")
                    {
                        if (prop.PropertyType == typeof(Guid?))
                        {
                            propValue = Guid.NewGuid();
                        }
                    }

                    //Lấy tên
                    var paramName = $"@{propName}";
                    sqlColumNames.Append($"{delimiter}{propName}");
                    sqlColumnValues.Append($"{delimiter}{paramName}");
                    delimiter = ",";

                    parameters.Add(paramName, propValue);
                }

                //Viết câu lệnh sql
                var sqlCommand = $"INSERT INTO {className} ({sqlColumNames.ToString()}) VALUES ({sqlColumnValues.ToString()});";

                //Thực hiện thêm mới dữ liệu
                var res = SqlConnection.Execute(sqlCommand, param: parameters);
                return res;

            }
        }

        /// <summary>
        /// Thực hiện cập nhật dữ liệu theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: HoaiPT(27/01/2022)
        /// Update: HoaiPT(07/02/2022)
        public int Update(Guid entityId, MISAEntity entity)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlColum = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                //Duyệt tất cả các property của đối tượng
                var props = typeof(MISAEntity).GetProperties();

                string delimiter = "";
                foreach (var prop in props)
                {
                    //Lấy tên của property
                    var propName = prop.Name;
                    var propValue = prop.GetValue(entity);

                    //Nếu nó là không phải là thuộc tính của sql thì bỏ vòng lặp này rồi tiếp tục vòng for
                    var map = Attribute.IsDefined(prop, typeof(NotMapSQL));
                    if (map == true) continue;

                    //Kiểm tra property có phải là khóa chính hay không 
                    var primaryKey = Attribute.IsDefined(prop, typeof(Primarykey));

                    //không phải là khóa chính
                    if (!(primaryKey == true || propName == $"{className}Id"))
                    {
                        //Nó có giá trị truyền vào
                        if (propValue != null)
                        {
                            var paramName = $"@{propName}";
                            sqlColum.Append($"{delimiter}{propName} = {paramName}");
                            delimiter = ",";

                            parameters.Add(paramName, propValue);
                        }
                    }
                }
                //Viết câu lệnh sql
                var sqlCommand = $"UPDATE {className} SET {sqlColum} WHERE {className}Id = @enityId";
                parameters.Add("@enityId", entityId);

                //Thực hiện update dữ liệu
                var res = SqlConnection.Execute(sqlCommand, param: parameters);
                return res;
            }
        }

        /// <summary>
        /// Thực hiện lấy dữ liệu export excel theo bảng
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: HoaiPT(11/02/2022)
        /// Update: HoaiPT(12/02/2022)
        public IEnumerable<MISAEntity> GetDataExport()
        {
            //Thực hiện khởi tạo kết nối và sau khi làm xong là nó tự ngắt kết nối luôn
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                //Thực hiện truy vấn dữ liệu trong database
                var enities = SqlConnection.Query<MISAEntity>($"SELECT * FROM View_Export{className};");
                return enities;
            }
        }
        #endregion

        #region Methods Validate
        /// <summary>
        /// Thực hiện kiểm tra sự tồn tại của id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// true: id đã tồn tại
        /// false: id chưa tồn tại
        /// </returns>
        /// CreatedBy: HoaiPT(24/01/2022)
        public bool ExistId(Guid id)
        {
            //Thực hiện khởi tạo kết nối và sau khi làm xong là nó tự ngắt kết nối luôn
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                //2. Thực hiện kiểm tra xem id nhân viên đã tồn tại chưa
                var sqlCheck = $"SELECT * FROM {className} WHERE {className}Id = @EnityId";
                var existRow = SqlConnection.QueryFirstOrDefault<MISAEntity>(sqlCheck, param: new { EnityId = id });
                if (existRow != null)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Thực hiện kiểm tra propName với giá trị là propValue đã tồn tại?
        /// </summary>
        /// <param name="isUpate"> True: là update; False: không phải là update</param>
        /// <param name="id">Khóa chính truyền vào</param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns>
        /// true: đã tồn tại
        /// false: chưa tồn tại
        /// </returns>
        /// CreatedBy: HoaiPT(26/01/2022)
        public bool CheckDuplicate_IsUpdate(bool isUpate, Guid? id, string propName, string propValue)
        {
            //Thực hiện khởi tạo kết nối và sau khi làm xong là nó tự ngắt kết nối luôn
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                //2. Thực hiện kiểm tra
                var sqlCheck2 = "";
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@PropValue", propValue);

                if (isUpate)
                {
                    //Đây là thuộc kiểu update
                    sqlCheck2 = $"SELECT * FROM {className} WHERE {propName} = @PropValue and  {className}Id != @EnityId ";
                    dynamic.Add("@EnityId", id);
                }
                else
                {
                    //Đây là thuộc kiểu insert
                    sqlCheck2 = $"SELECT * FROM {className} WHERE {propName} = @PropValue ";
                }

                var existPropName = SqlConnection.QueryFirstOrDefault<MISAEntity>(sqlCheck2, param: dynamic);

                if (existPropName != null)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion


       
        #endregion

    }
}
