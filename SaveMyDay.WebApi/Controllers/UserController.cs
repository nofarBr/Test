using SaveMyDate.Entities;
using System.Web.Http;

namespace SaveMyDay.WebApi.Controllers
{
    public class UserController : ApiController
    {
        public User GetUserByName(string name)
        {
            return new User { Name = "iahel", Password = "222" };
        }
    }
}
