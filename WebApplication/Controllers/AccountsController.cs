using Microsoft.AspNetCore.Mvc;
using Services.IConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class AccountsController : BaseController
    {
        public AccountsController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}