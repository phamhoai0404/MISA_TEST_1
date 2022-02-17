using MISA.Fresher.Web12.Core.Enum;
using MISA.Fresher.Web12.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MISA.Fresher.Web12.Core.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    /// CreatedBy: HoaiPT(10/02/2022)
    public class Employee
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Primarykey]
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [NotEmpty]
        [NotDuplicate]
        [PropertyName("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ tên đầy đủ
        /// </summary>
        [NotEmpty]
        [PropertyName("Họ tên đầy đủ")]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [PropertyName("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [PropertyName("Giới tính")]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        [PropertyName("Số CMND")]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CMND
        /// </summary>
        [PropertyName("Ngày cấp CMND")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CMND
        /// </summary>
        [PropertyName("Nơi cấp CMND")]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        [PropertyName("Chức vụ")]
        public string? PositionName { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        [PropertyName("Số điện thoại di động")]
        public string? MobilephoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [PropertyName("Địa chỉ")]
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại bàn
        /// </summary>
        [PropertyName("Số điện thoại bàn")]
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ Email
        /// </summary>
        [PropertyName("Email")]
        public string? Email { get; set; }

        /// <summary>
        ///Số tài khoản ngân hàng
        /// </summary>
        [PropertyName("Số tài khoản ngân hàng")]
        public string? BankAccountNumber { get; set; }

        /// <summary>
        ///Tên chi nhánh ngân hàng
        /// </summary>
        [PropertyName("Tên chi nhánh ngân hàng")]
        public string? BankBranchName { get; set; }


        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [PropertyName("Tên ngân hàng")]
        public string? BankName { get; set; }


        /// <summary>
        /// Nhà cung cấp
        /// </summary>
        [PropertyName("Nhà cung cấp")]
        public bool? IsSupplier { get; set; }

        /// <summary>
        /// Khách hàng
        /// </summary>
        [PropertyName("Khách hàng")]
        public bool? IsCustomer { get; set; }

        /// <summary>
        /// Khách hàng
        /// </summary>
        [NotEmpty]
        [PropertyName("Phòng ban")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Ngày tạo nhân viên
        /// </summary>
        [PropertyName("Ngày tạo nhân viên")]
        public DateTime? CreatedDate { get; set; }


        /// <summary>
        /// Người tạo nhân viên
        /// </summary>
        [PropertyName("Người tạo nhân viên")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        [PropertyName("Ngày cập nhật")]
        public DateTime? ModifiedDate { get; set; }
        

        /// <summary>
        /// Người cập nhật
        /// </summary>
        [PropertyName("Người cập nhật")]
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Tên giới tính
        /// (Propertity bỏ qua khi thực hiện post hay put
        /// (Vì không có trong cơ sử dữ liệu))
        /// </summary>
        [NotMapSQL]
        public string? GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Enum.Gender.Male:
                        return Core.Resourcs.ResourceVN.Gender_Male;
                    case Enum.Gender.Female:
                        return Core.Resourcs.ResourceVN.Gender_Female;
                    case Enum.Gender.Other:
                        return Core.Resourcs.ResourceVN.Gender_Other;
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Tên phòng ban
        /// (Propertity bỏ qua khi thực hiện post hay put
        /// (Vì không có trong cơ sử dữ liệu))
        /// </summary>
        [NotMapSQL]
        public string? DepartmentName { get; set; }
        #endregion
    }
}
