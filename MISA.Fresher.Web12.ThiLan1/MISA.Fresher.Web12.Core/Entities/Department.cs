using MISA.Fresher.Web12.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Web12.Core.Entities
{
    /// <summary>
    /// Phòng ban
    /// </summary>
    /// CreatedBy: HoaiPT(10/02/2022)
    public class Department
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        [Primarykey]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban 
        /// </summary>
        [NotEmpty]
        [PropertyName("Tên phòng ban")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [PropertyName("Mô tả")]
        public string? Description { get; set; }


        /// <summary>
        /// Ngày tạo phòng ban
        /// </summary>
        [PropertyName("Ngày tạo phòng ban")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo phòng ban
        /// </summary>
        [PropertyName("Người tạo phòng ban")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        [PropertyName("Ngày cập nhật")]
        public string? ModifiedDate { get; set; }

        /// <summary>
        /// Người cập nhật
        /// </summary>
        [PropertyName("Người cập nhật")]
        public string? ModifiedBy { get; set; }


        #endregion
    }
}
