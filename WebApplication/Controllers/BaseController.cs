using Microsoft.AspNetCore.Mvc;
using Services.IConfiguration;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class BaseController : ControllerBase
    {
        public IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}