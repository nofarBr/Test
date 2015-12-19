using System.Linq;
using System.Net;
using System.Net.Http;
using SaveMyDate.Entities;
using System.Web.Http;
using System.Web.OData;
using SaveMayDay.Common;

namespace SaveMyDay.WebApi.Controllers
{
    public class UserController : ODataController
    {
        private readonly MongoCrud<User> _mongoCrud;
        public UserController()
        {
            _mongoCrud = new MongoCrud<User>();
        }

        [EnableQuery]
        public IQueryable<User> GetUser()
        {
            return _mongoCrud.GetAllEntities().AsQueryable();
        }


        [HttpPost]
        public HttpResponseMessage PostUser([FromBody]User user)
        {
            _mongoCrud.SaveOrUpdate(user);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
