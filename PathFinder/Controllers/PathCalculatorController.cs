using System.Collections.Generic;
using System.Web.Http;
using SaveMyDate.Entities;
using SaveMyDay.Algoritem;

namespace PathFinder.Controllers
{
    public class PathCalculatorController : ApiController
    {
        [HttpPost]
        public void PostPathCalculator(List<CompanyType> companyTypes, City city)
        {
            var companiesForAlgorithem = new ComapanyQueryHandler().GetCompaniesByTypeAndLocation(companyTypes, city);
            var result = new AlgoritemRunner();
            result.Activate(companiesForAlgorithem);
            // Redirect()
        }
    }
}
