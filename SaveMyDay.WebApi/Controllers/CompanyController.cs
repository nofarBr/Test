using SaveMyDate.Entities;
using System.Web.Http;

namespace SaveMyDay.WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        public Company GetCompanyByKindAndLocation(string Type, [FromUri]City city)
        {
            return new Company { Code = 1, Type = CompanyType.MedicalClinic, Location = new Location { X = 0.6494, Y = 65946.614 } };
        }
    }
}
