using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Web12.Core.Entities;
using MISA.Fresher.Web12.Core.Interfaces.Infrastructure;
using MISA.Fresher.Web12.Core.Interfaces.Services;

namespace MISA.Fresher.Web12.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : MISABaseController<Department>
    {
        public DepartmentsController(IBaseService<Department> baseService, IBaseRepository<Department> baseRepository) : base(baseRepository, baseService)
        {

        }
    }
}