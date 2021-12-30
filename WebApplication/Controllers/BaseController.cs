using Microsoft.AspNetCore.Mvc;
using Services.IConfiguration;

namespace WebApplication.Controllers
{
    public class BaseController : ControllerBase
    {
        public IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}