using LengerProje.Helpers;
using LengerProje.Helpers.Constants;
using LengerProje.Helpers.Enums;
using LengerProje.Models;
using LengerProje.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;


namespace LengerProje.Controllers
{   
    
    [Route("api/[controller]/[action]")]
    public class UserController : ApiController
    {
        private readonly UserRepository userRepo;

        public UserController()
        {
            userRepo = new UserRepository();
        }

        #region User Registiration
      

        [AllowAnonymous]
        [Route("api/user/Register")]
        [HttpPost]
        public IHttpActionResult Register([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                bool success = userRepo.AddUser(user);
                if (success)
                {
                    Logger.SendLog("UserController/Register", user, false, LogTypes.Response);
                    return Ok( Messages.Success);

                }
                else
                {
                    Logger.SendLog("UserController/Register", user, true, LogTypes.Response);
                    return Ok(Messages.Error);
                }
                   
            }
            else
            {
                Logger.SendLog("UserController/Register", user, true, LogTypes.Response);
                return Ok(Messages.Check);
            }
        }
        #endregion

        #region Add SuperUser

        [Authorize]
        [Route("api/user/AddSuperUser")]
        [HttpPost]
        public IHttpActionResult AddSuperUser([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                bool success = userRepo.AddSuperUser(user);
                if (success)
                {
                    Logger.SendLog("UserController/AddSuperUser", user, false, LogTypes.Response);
                    return Ok(Messages.Success);

                }
                else
                {
                    Logger.SendLog("UserController/AddSuperUser", user, true, LogTypes.Response);
                    return Ok(Messages.Error);
                }

            }
            else
            {
                Logger.SendLog("UserController/AddSuperUser", user, true, LogTypes.Response);
                return Ok(Messages.Check);
            }
        }
        #endregion
    }
}
