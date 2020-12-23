using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class UserController : ApiController
    {
        UserEntities DB = new UserEntities();
        [Route("InsertUser")]
        [HttpPost]
        public object InsertUser(User user)
        {
            try
            {
                User US = new User();
                if (US.UserId == 0)
                {
                    US.FirstName = user.FirstName;
                    US.LastName = user.LastName;
                    US.EmailAddress = user.EmailAddress;
                    US.Password = user.Password;
                    US.Status = user.Status;
                    DB.Users.Add(US);
                    DB.SaveChanges();
                    return new Response
                    { Status = "Success", Message = "Record SuccessFully Saved." };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return new Response
            { Status = "Error", Message = "Invalid Data." };
        }
        [Route("Login")]
        [HttpPost]
        public Response UserLogin(User login)
        {
            var log = DB.Users.Where(x => x.EmailAddress.Equals(login.EmailAddress) &&
                      x.Password.Equals(login.Password)).FirstOrDefault();

            if (log == null)
            {
                return new Response { Status = "Invalid", Message = "Invalid User." };
            }
            else
                return new Response { Status = "Success", Message = "Login Successfully" };
        }


    }
}
