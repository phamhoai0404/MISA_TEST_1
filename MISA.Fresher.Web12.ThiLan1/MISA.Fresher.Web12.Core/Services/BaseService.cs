using MISA.Fresher.Web12.Core.Exceptions;
using MISA.Fresher.Web12.Core.Interfaces.Infrastructure;
using MISA.Fresher.Web12.Core.Interfaces.Services;
using MISA.Fresher.Web12.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Web12.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        #region Property
        IBaseRepository<MISAEntity> _baseRepository;

        //1.Tên của Enity
        private string className = typeof(MISAEntity).Name;
        #endregion

        #region Contructor
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods

        #region Methods Basic
        public int DeleteService(Guid entityId)
        {
            //1.XỬ LÝ VALIDATE DỮ LIỆU
            //1.1 Validate xử lý chung 
            this.ValidateExistId(entityId);

            //1.2 Valiate xử lý riêng
            this.ValidateDeleteCustomer(entityId);

            var res = _baseRepository.Delete(entityId);
            return res;
        }
        public int InsertService(MISAEntity entity)
        {
            //1.XỬ LÝ VALIDATE DỮ LIỆU
            //1.1 Validate xử lý chung 
            this.ValidateNotEmpty(entity);//Kiểm tra các trường không được để trống
            this.ValidateNotDuplicate_IsUpate(false,null, entity);//Kiểm tra các trường có giá trị không được trùng lặp

            //1.2 Validate riêng
            this.ValidateInsertCustomer(entity);
            
            //2.Thực hiện thêm mới vào data base
            var res = _baseRepository.Insert(entity);
            return res;
        }
        public int UpdateService(Guid entityId, MISAEntity entity)
        {
            //1.XỬ LÝ VALIDATE DỮ LIỆU
            //1.1 Validate xử lý chung 
            this.ValidateExistId(entityId);//Kiểm tra sự id này đã tồn tại id
            this.ValidateNotEmpty(entity);//Kiểm tra các trường không được để trống
            this.ValidateNotDuplicate_IsUpate(true, entityId, entity);//Kiểm tra các trường có giá trị không được trùng lặp

            //1.2 Validate riêng
            this.ValidateUpdateCustomer(entityId, entity);

            //2.Thực hiện update dữ liệu
            var res = _baseRepository.Update(entityId,entity);
            return res;
        }
        #endregion

        #region Methods Private
        /// <summary>
        /// Thực hiện kiểm tra dữ liệu không được để trống 
        /// </summary>
        /// <param name="entity">Cả đối tượng truyền vào</param>
        /// <exception cref="MISAValidateException"></exception>
        /// CreatedBy: HoaiPT(07/02/2022)
        private void ValidateNotEmpty(MISAEntity entity)
        {
            //Lấy ra tất cả các porperty được đánh dấu là NotEmpty không được để trống 
            var propNotEmpties = entity.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(NotEmpty)));

            var lan = 1;
            foreach (var prop in propNotEmpties)
            {
                var propValue = prop.GetValue(entity);
                var propName = prop.Name;
                var nameDisplay = string.Empty;

                //Lấy ra tên của property vì anh bảo là có thể một thuộc tính có nhiều cái PropertyName nên nó có thể là một mảng
                var propertyNames = prop.GetCustomAttributes(typeof(PropertyName), true);
                 if (propertyNames.Length > 0)
                {
                    nameDisplay = (propertyNames[0] as PropertyName).Name;
                }
               // ((propValue == String.Format(Core.Resourcs.ResourceVN.NotGuid)) && (prop.PropertyType == typeof(Guid)))
                if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                {
                    //Kiểm tra xem nó có tồn tại cái propertyName không
                    //nếu tồn tại thì lấy còn nếu không thì lấy chính cái tên property có sẵn
                    nameDisplay = (nameDisplay != string.Empty) ? nameDisplay : propName;

                    throw new MISAValidateException(String.Format(Core.Resourcs.ResourceVN.InforNotEmpty, nameDisplay));
                }
            }

        }

        /// <summary>
        /// Thực hiện kiểm tra sự tồn tại của Id
        /// </summary>
        /// <param name="entityId">id truyền vào để kiểm tra</param>
        /// <exception cref="MISAValidateException"></exception>
        /// CreatedBy: HoaiPT(07/02/2022)
        private void ValidateExistId(Guid entityId)
        {
            if (!_baseRepository.ExistId(entityId))
            {
                throw new MISAValidateException(String.Format(Core.Resourcs.ResourceVN.InforNotExistId, className));
            }
        }

        /// <summary>
        /// Thực hiện kiểm tra xem thuộc tính có được lặp lại
        /// </summary>
        /// <param name="isUpdate"> true: update, false: không phải là update</param>
        /// <param name="id">khóa chính</param>
        /// <param name="entity">cả đối tượng</param>
        /// <exception cref="MISAValidateException"></exception>
        /// CreatedBy: HoaiPT(07/02/2022)
        private void ValidateNotDuplicate_IsUpate(bool isUpdate, Guid? id, MISAEntity entity)
        {
            //Lấy ra tất cả các porperty được đánh dấu là NotDuplicate không được để trống 
            var propNotEmpties = entity.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(NotDuplicate)));

            foreach (var prop in propNotEmpties)
            {
                var propValue = prop.GetValue(entity);//Giá trị
                var propName = prop.Name;//Lấy tên
                var nameDisplay = string.Empty;

                //Lấy ra tên của property vì anh bảo là có thể một thuộc tính có nhiều cái PropertyName nên nó có thể là một mảng
                var propertyNames = prop.GetCustomAttributes(typeof(PropertyName), true);
                if (propertyNames.Length > 0)
                {
                    nameDisplay = (propertyNames[0] as PropertyName).Name;
                }

                if (_baseRepository.CheckDuplicate_IsUpdate(isUpdate, id, propName, (propValue + "")))
                {
                    //Kiểm tra xem nó có tồn tại cái propertyName không
                    //nếu tồn tại thì lấy còn nếu không thì lấy chính cái tên property có sẵn
                    nameDisplay = (nameDisplay != string.Empty) ? nameDisplay : propName;

                    throw new MISAValidateException(String.Format(Core.Resourcs.ResourceVN.InforDuplicate,nameDisplay));
                }
            }
        }
        #endregion

        #region Override
        /// <summary>
        ///Validate dữ liệu riêng của từng class khác nhau khi thực hiện thêm mới
        /// </summary>
        /// <param name="entity">Cả đối tượng enity</param>
        /// CreatedBy: HoaiPT(07/02/2022)
        protected virtual void ValidateInsertCustomer(MISAEntity entity)
        {

        }

        /// <summary>
        /// Validate dữ liệu riêng của từng class khác nhau khi thực hiện cập nhật
        /// </summary>
        /// <param name="entityId">Khóa của enity</param>
        /// <param name="entity">Cả đối tượng enity</param>
        /// CreatedBy: HoaiPT(07/02/2022)
        protected virtual void ValidateUpdateCustomer(Guid entityId, MISAEntity entity)
        {

        }

        /// <summary>
        ///Validate dữ liệu riêng của từng class khác nhau khi thực hiện xóa
        /// </summary>
        /// <param name="entityId">Khóa của enity</param>
        protected virtual void ValidateDeleteCustomer(Guid entityId) { }
        #endregion

        #endregion
    }
}
