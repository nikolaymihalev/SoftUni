using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCIdentityDemo.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
