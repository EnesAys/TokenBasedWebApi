using LengerProje.DATA;
using LengerProje.Helpers;
using LengerProje.Helpers.Constants;
using LengerProje.Helpers.Enums;
using LengerProje.Models;
using LengerProje.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace LengerProje.Controllers
{

    [Route("api/[controller]/[action]")]
    public class TextController : ApiController
    {

        private readonly TextRepository textRepo;
        private readonly UserRepository userRepo;

        public TextController()
        {
            textRepo = new TextRepository();
            userRepo = new UserRepository();
        }

        #region Get Texts List

        [AllowAnonymous]
        [Route("api/text/GetTexts")]
        [HttpGet]
        public IEnumerable<Texts> GetTexts()
        {
            try
            {
                var list = textRepo.GetTexts();
                if (list == null)
                {
                    Logger.SendLog("TextController/GetTexts", Messages.NullData, false, LogTypes.Response);
                    throw new Exception(Messages.NullData);
                }
                else
                {
                    Logger.SendLog("TextController/GetTexts", Messages.Success, false, LogTypes.Response);
                    try
                    {
                        return list;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception();
                    }
                  
                }


            }
            catch (Exception ex)
            {
                Logger.SendLog("TextController/GetTexts", Messages.Error, true, LogTypes.Response);
                throw new Exception(ex.ToString());
            }
        }
        #endregion

        #region Add Text 

        [Authorize]
        [Route("api/text/AddText")]
        [HttpPost]
        public IHttpActionResult AddText([FromBody]Text text)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var user = userRepo.FindUser(identity.Name);
                text.UserID = user.ID;

                if (ModelState.IsValid)
                {
                    bool success = textRepo.AddText(text);
                    if (success)
                    {
                        Logger.SendLog("TextController/AddText", text, false, LogTypes.Response);
                        return Ok(Messages.Success);

                    }
                    else
                    {
                        Logger.SendLog("TextController/AddText", text, true, LogTypes.Response);
                        return Ok(Messages.Error);
                    }

                }
                else
                {
                    Logger.SendLog("TextController/AddText", text, true, LogTypes.Response);
                    return Ok(Messages.Check);
                }
            }
            catch (Exception ex)
            {
                Logger.SendLog("TextController/AddText", text+" "+Messages.UserNotFind, true, LogTypes.Response);
                throw new Exception(ex.ToString()+" "+Messages.UserNotFind);
            }
         
        }
        #endregion

        #region Bonus - DeleteText
        [Authorize(Roles=RoleType.SuperUser)]
        [Route("api/text/DeleteText")]
        [HttpDelete]
        public IHttpActionResult DeleteText(int id)
        {
            bool isSucceed;
            try
            {
                isSucceed = textRepo.DeleteText(id);
                if (isSucceed)
                {
                    Logger.SendLog("TextController/DeleteText", id+" "+Messages.Success, false, LogTypes.Response);
                    return Ok(Messages.Success);
                }
                else
                {
                    Logger.SendLog("TextController/DeleteText", id + " " + Messages.Error, true, LogTypes.Response);
                    return Ok(Messages.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.SendLog("TextController/DeleteText", Messages.Error+" "+ex.ToString(), true, LogTypes.Response);
                throw new Exception(ex.ToString());
            }

        }
        #endregion


    }
}
