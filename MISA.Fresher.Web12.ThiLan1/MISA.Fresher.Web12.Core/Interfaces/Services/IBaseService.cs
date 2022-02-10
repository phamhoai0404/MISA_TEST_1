using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Web12.Core.Interfaces.Services
{
    public interface IBaseService<MISAEnity>
    {

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng thêm mới</param>
        /// <returns></returns>
        /// Created: HoaiPT(27/01/2022)
        /// Update: HoaiPT(07/02/2022)
        int InsertService(MISAEnity entity);

        /// <summary>
        /// Sửa dữ liệu
        /// </summary>
        /// <param name="entity">Cả đối tượng </param>
        /// <param name="entityId">Id của đối tượng cần thực thực hiện sửa dữ liệu</param>
        /// <returns></returns>
        /// Created: HoaiPT(27/01/2022)
        /// Update: HoaiPT(07/02/2022)
        int UpdateService(Guid entityId, MISAEnity entity);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="entityId">Id của đối tượng cần thực hiện xóa</param>
        /// <returns></returns>
        /// Created: HoaiPT(27/01/2022)
        /// Update: HoaiPT(07/02/2022)
        int DeleteService(Guid entityId);
    }
}
