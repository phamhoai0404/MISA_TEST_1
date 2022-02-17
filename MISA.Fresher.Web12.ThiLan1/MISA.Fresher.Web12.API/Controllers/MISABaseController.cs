using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Web12.Core.Exceptions;
using MISA.Fresher.Web12.Core.Interfaces.Infrastructure;
using MISA.Fresher.Web12.Core.Interfaces.Services;

namespace MISA.Fresher.Web12.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MISABaseController<MISAEnity> : ControllerBase
    {
        #region Fields
        IBaseService<MISAEnity> _baseService;
        IBaseRepository<MISAEnity> _baseRepository;
        #endregion

        #region Contructor
        public MISABaseController(IBaseRepository<MISAEnity> baseRepository, IBaseService<MISAEnity> baseService)
        {
            this._baseRepository = baseRepository;
            this._baseService = baseService;
        }
        #endregion

        #region Methods

        #region Methods Basic

        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <returns></returns>
        /// Created: HoaiPT(07/02/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var entites = _baseRepository.GetAll();
                return Ok(entites);
            }
            catch (Exception ex)
            {
                return this.AllException(ex, null);
            }


        }

        /// <summary>
        /// Lấy entity theo mã
        /// </summary>
        /// <param name="enityId"></param>
        /// <returns></returns>
        /// Created: HoaiPT(07/02/2022)
        [HttpGet("{enityId}")]
        public IActionResult GetById(Guid enityId)
        {
            try
            {
                var enity = _baseRepository.GetById(enityId);
                return Ok(enity);
            }
            catch (Exception ex)
            {
                return this.AllException(ex, null);
            }

        }
        /// <summary>
        /// Thực hiện thêm mới
        /// </summary>
        /// <param name="enity"></param>
        /// <returns></returns>
        /// Created: HoaiPT(07/02/2022)
        [HttpPost]
        public IActionResult Post(MISAEnity enity)
        {
            try
            {
                //Validate dữ liệu
                var res = _baseService.InsertService(enity);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return this.AllException(ex, null);
            }
        }

        /// <summary>
        /// Thực hiện sửa theo mã
        /// </summary>
        /// <param name="enityId"></param>
        /// <param name="enity"></param>
        /// <returns></returns>
        /// Created: HoaiPT(07/02/2022)
        [HttpPut("{enityId}")]
        public IActionResult Put(Guid enityId, MISAEnity enity)
        {
            try
            {
                //Validate dữ liệu
                var res = _baseService.UpdateService(enityId, enity);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return this.AllException(ex, enity);
            }

        }

        /// <summary>
        /// Thực hiện xóa enity theo mã
        /// </summary>
        /// <param name="enityId"></param>
        /// <returns></returns>
        /// Created: HoaiPT(07/02/2022)
        [HttpDelete("{enityId}")]
        public IActionResult Delete(Guid enityId)
        {
            try
            {
                //Validate dữ liệu
                var res = _baseService.DeleteService(enityId);

                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return this.AllException(ex, null);
            }

        }
        #endregion

        #region Methods Exception

        /// <summary>
        /// Khi có lỗi sẽ nhảy vào exception này
        /// </summary>
        /// <param name="ex">Error</param>
        /// <returns></returns>
        /// Created: HoaiPT(07/02/2022)
        private IActionResult AllException(Exception ex, Object? enity)
        {
            if (typeof(MISAValidateException) == ex.GetType())
            {
                var respo = new
                {
                    devMsg = ex.Message,
                    userMsg = ex.Message,
                    data = enity,
                };
                return StatusCode(400,respo);
            }
            else
            {
                var response = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Resourcs.ResourceVN.ErrorException
                };

                return StatusCode(500, response);
            }
            
        }
        #endregion


        #endregion
    }
}